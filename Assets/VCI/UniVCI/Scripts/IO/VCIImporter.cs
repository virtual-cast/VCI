using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Effekseer;
using TMPro;
using UnityEngine;
using UniGLTF;
using VRMShaders;

namespace VCI
{
    public sealed class VCIImporter : ImporterContext
    {
        public RuntimeVciInstance RuntimeVciInstance { get; private set; }

        private AudioClipFactory AudioClipFactory { get; }
        private PhysicMaterialFactory PhysicMaterialFactory { get; }
        private ColliderMeshFactory ColliderMeshFactory { get; }

        private readonly VciData _data;
        private readonly bool _isLocation;
        private readonly IFontProvider _fontProvider;
        private readonly IVciDefaultLayerProvider _vciDefaultLayerProvider;
        private readonly IVciColliderLayerProvider _vciColliderLayerProvider;
        private readonly ISpringBoneImporter _springBoneImporter;

        public VCIImporter(
            VciData data,
            IReadOnlyDictionary<SubAssetKey, UnityEngine.Object> externalObjectMap = null,
            ITextureDeserializer textureDeserializer = null,
            IMaterialDescriptorGenerator materialDescriptorGenerator = null,
            bool isLocation = false,
            IVciDefaultLayerProvider vciDefaultLayerProvider = null,
            IVciColliderLayerProvider vciColliderLayerProvider = null,
            IFontProvider fontProvider = null,
            IMp3FileDecoder mp3FileDecoder = null,
            ISpringBoneImporter springBoneImporter = null
        ) : base(data.GltfData, externalObjectMap, textureDeserializer)
        {
            _data = data;
            _isLocation = isLocation;
            _vciDefaultLayerProvider = vciDefaultLayerProvider ?? new VciDefaultLayerSettings();
            _vciColliderLayerProvider = vciColliderLayerProvider ?? new VciDefaultLayerSettings();
            _fontProvider = fontProvider;
            var mp3DecodeStrategy = new Mp3DecodeStrategyAdapter(new NAudioMp3DecoderImpl(), mp3FileDecoder);
            _springBoneImporter = springBoneImporter ?? new SpringBoneImporter();
            AudioClipFactory = new AudioClipFactory(ExternalObjectMap
                .Where(x => x.Value is AudioClip)
                .ToDictionary(x => x.Key, x => (AudioClip)x.Value),
                mp3DecodeStrategy);
            PhysicMaterialFactory = new PhysicMaterialFactory(ExternalObjectMap
                .Where(x => x.Value is PhysicMaterial)
                .ToDictionary(x => x.Key, x => (PhysicMaterial)x.Value));
            ColliderMeshFactory = new ColliderMeshFactory(ExternalObjectMap
                .Where(x => x.Value is Mesh)
                .ToDictionary(x => x.Key, x => (Mesh)x.Value));

            // VCI Specification
            InvertAxis = Axes.Z;

            if (_data.Meta == null)
            {
                throw new Exception($"This file has no {nameof(glTF_VCAST_vci_meta)} extension.");
            }

            // Version Check
            if (!ImportableVersionCheck(_data.VciMigrationFlags))
            {
                var (major, minor) = (_data.VciMigrationFlags.FileVciMajorVersion, _data.VciMigrationFlags.FileVciMinorVersion);
                throw new Exception($"The current UniVCI cannot read this VCI version. ({major}.{minor})");
            }

            MaterialDescriptorGenerator = materialDescriptorGenerator ?? new VciMaterialDescriptorGenerator(_data.UnityMaterials, _data.VciMigrationFlags.IsPbrBaseColorSrgb);
            TextureDescriptorGenerator = new VciTextureDescriptorGenerator(_data.GltfData, _data.UnityMaterials, _data.Meta);
        }

        public override void TransferOwnership(TakeResponsibilityForDestroyObjectFunc take)
        {
            base.TransferOwnership(take);

            AudioClipFactory.TransferOwnership(take);
            PhysicMaterialFactory.TransferOwnership(take);
            ColliderMeshFactory.TransferOwnership(take);
        }

        private const string MATERIAL_EXTENSION_NAME = "VCAST_vci_material_unity";

        private const string MATERIAL_PATH = "/extensions/" + MATERIAL_EXTENSION_NAME + "/materials";

        private bool ImportableVersionCheck(VciMigrationFlags flags)
        {
            if (flags.FileVciMajorVersion > VciMigrationFlags.RuntimeVciMajorVersion) return false;
            if (flags.FileVciMajorVersion == VciMigrationFlags.RuntimeVciMajorVersion &&
                flags.FileVciMinorVersion > VciMigrationFlags.RuntimeVciMinorVersion) return false;

            return true;
        }

        public override async Task<RuntimeGltfInstance> LoadAsync(IAwaitCaller awaitCaller = null, Func<string, IDisposable> measureTime = null)
        {
            awaitCaller = awaitCaller ?? new ImmediateCaller();
            measureTime = measureTime ?? (x => null);

            RuntimeGltfInstance runtimeGltfInstance;
            using (measureTime("==== Load VCI Base GLTF ===="))
            {
                runtimeGltfInstance = await base.LoadAsync(awaitCaller, measureTime);
            }
            await awaitCaller.NextFrame();

            // Load VCI Extensions
            using (measureTime("===== Load VCI Extensions ===="))
            {
                RuntimeVciInstance = await SetupVciExtensionsAsync(runtimeGltfInstance, awaitCaller, measureTime);
                RuntimeVciInstance.EnablePhysicalBehaviour(false);
            }

            return runtimeGltfInstance;
        }

        private async Task<RuntimeVciInstance> SetupVciExtensionsAsync(RuntimeGltfInstance runtimeGltfInstance, IAwaitCaller awaitCaller, Func<string, IDisposable> measureTime)
        {
            var vciObject = Root.AddComponent<VCIObject>();
            vciObject.Meta = await LoadVciMetaAsync(awaitCaller);

            if (GLTF.extensions == null)
            {
                return null;
            }

            // Script
            using (measureTime("Script"))
            {
                await ScriptImporter.LoadAsync(_data, vciObject, awaitCaller);
            }

            // Audio
            using (measureTime("Audio"))
            {
                // NOTE: LoadAsyncはCancellationTokenに対応しているが、そもそもVCIロードがキャンセル未対応
                // そのため、現時点ではCancellationTokenを渡さない
                await AudioImporter.LoadAsync(_data, Nodes, Root, AudioClipFactory, awaitCaller);
            }

            // Animation
            // Animation 自体は GLTF 側の LoadAsync からロード済み（ロード手法は override している）
            var rootAnimation = Root.GetComponent<Animation>();
            if (rootAnimation != null)
            {
                rootAnimation.playAutomatically = false;
            }

            // SubItem
            using (measureTime("SubItem"))
            {
                SubItemImporter.Load(_data, Nodes);
            }

            // DefaultLayer
            using (measureTime("Default Layer"))
            {
                LayerImporter.Load(GLTF, Nodes, _vciDefaultLayerProvider, _isLocation);
            }
            await awaitCaller.NextFrame();

            // Effekseer
            List<EffekseerEmitter> effekseerEmitterComponents;
            using (measureTime("Effekseer"))
            {
                effekseerEmitterComponents =
                    await EffekseerImporter.LoadAsync(_data, Nodes, TextureFactory, awaitCaller);
            }

            // Text
            List<TextMeshPro> texts;
            using (measureTime("Text"))
            {
                texts = await TextMeshProImporter.LoadAsync(_data, Nodes, _fontProvider, awaitCaller);
            }

            // Physics
            List<Collider> colliders;
            Dictionary<Rigidbody, RigidbodySetting> rigidbodySettings;
            using (measureTime("Physics"))
            {
                colliders = await PhysicsColliderImporter.LoadAsync(
                    _data,
                    Nodes,
                    _vciColliderLayerProvider,
                    InvertAxis.Create(),
                    PhysicMaterialFactory,
                    ColliderMeshFactory,
                    awaitCaller);
                rigidbodySettings = await PhysicsRigidbodyImporter.LoadAsync(_data, Nodes, awaitCaller);
                await PhysicsJointImporter.LoadAsync(_data, Nodes, awaitCaller);
            }

            // Attachable
            using (measureTime("Attachable"))
            {
                AttachableImporter.Load(_data, Nodes);
            }

            // SpringBone
            using (measureTime("SpringBone"))
            {
                _springBoneImporter.Load(_data, Nodes, Root);
            }

            // PlayerSpawnPoint
            using (measureTime("PlayerSpawnPoint"))
            {
                PlayerSpawnPointImporter.Load(_data, Nodes);
            }

            // LocationBounds
            using (measureTime("LocationBounds"))
            {
                LocationBoundsImporter.Load(_data, Root);
            }

            using (measureTime("Lightmap"))
            {
                await LightmapImporter.LoadAsync(_data, Nodes, Root, TextureFactory, _isLocation, awaitCaller);
            }

            // NOTE: VCI ロード時に生成したリソースを移譲する.
            var runtimeVciResources = new List<(SubAssetKey, UnityEngine.Object)>();
            TransferOwnership((key, obj) => runtimeVciResources.Add((key, obj)));

            return new RuntimeVciInstance(
                runtimeGltfInstance,
                runtimeVciResources,
                Nodes,
                colliders,
                rigidbodySettings,
                texts,
                effekseerEmitterComponents
            );
        }

        public override async Task LoadAnimationAsync(IAwaitCaller awaitCaller)
        {
            await AnimationImporter.LoadAsync(_data, AnimationClipFactory, InvertAxis.Create(), awaitCaller);
        }

        protected override async Task SetupAnimationsAsync(IAwaitCaller awaitCaller)
        {
            await AnimationImporter.SetupAsync(_data, Nodes, Root, AnimationClipFactory, awaitCaller);
        }

        public async Task<VCIMeta> LoadVciMetaAsync(IAwaitCaller awaitCaller = null)
        {
            awaitCaller = awaitCaller ?? new ImmediateCaller();

            return await MetaImporter.LoadMetaAsync(Data, _data.Meta, TextureFactory, awaitCaller);
        }
    }
}
