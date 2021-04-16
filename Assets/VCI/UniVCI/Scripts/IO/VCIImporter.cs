using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UniGLTF;
using UniJSON;
using UniGLTF.Legacy;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VCI
{
    public class VCIImporter : LegacyImporterContext
    {
        private VCI.glTF_VCAST_vci_meta VciMeta { get; set; }

        public Dictionary<string, string> AudioAssetPathList = new Dictionary<string, string>();

        public List<Effekseer.EffekseerEmitter> EffekseerEmitterComponents = new List<Effekseer.EffekseerEmitter>();
        public List<Effekseer.EffekseerEffectAsset> EffekseerEffectAssets = new List<Effekseer.EffekseerEffectAsset>();

        public VCIObject VCIObject { get; private set; }

        private IFontProvider _fontProvider;

        private List<Collider> _colliderComponents = new List<Collider>();

        public class RigidbodySetting
        {
            public RigidbodyConstraints Constraints { get; set; }
            public bool IsKinematic { get; private set; }
            public bool UseGravity { get; private set; }

            public RigidbodySetting(Rigidbody rigidbody)
            {
                Constraints = rigidbody.constraints;
                IsKinematic = rigidbody.isKinematic;
                UseGravity = rigidbody.useGravity;
            }
        }

        private readonly Dictionary<Rigidbody, RigidbodySetting> _rigidBodySettings =
            new Dictionary<Rigidbody, RigidbodySetting>();

        public Dictionary<Rigidbody, RigidbodySetting> RigidBodySettings => _rigidBodySettings;

        public bool IsLocation { get; private set; }

        private readonly IVciDefaultLayerProvider _vciDefaultLayerProvider;
        private readonly IVciColliderLayerProvider _vciColliderLayerProvider;

        public VCIImporter(
            bool isLocation = false,
            IVciDefaultLayerProvider vciDefaultLayerProvider = null,
            IVciColliderLayerProvider vciColliderLayerProvider = null,
            IFontProvider fontProvider = null)
        {
            IsLocation = isLocation;
            _vciDefaultLayerProvider = vciDefaultLayerProvider ?? new VciColldierEditorSetting();
            _vciColliderLayerProvider = vciColliderLayerProvider ?? new VciColldierEditorSetting();
            _fontProvider = fontProvider;
        }

        public override void Parse(string path, byte[] bytes)
        {
            var ext = Path.GetExtension(path).ToLower();
            switch (ext)
            {
                case ".vci":
                    ParseGlb(bytes);
                    break;

                default:
                    base.Parse(path, bytes);
                    break;
            }
        }

        private const string MATERIAL_EXTENSION_NAME = "VCAST_vci_material_unity";

        private const string MATERIAL_PATH = "/extensions/" + MATERIAL_EXTENSION_NAME + "/materials";

        private string GetVersionValue(string exportedVciVersion)
        {
            if (string.IsNullOrEmpty(exportedVciVersion))
            {
                throw new Exception("exportedVciVersion is empty.");
            }

            System.Text.RegularExpressions.Regex r =
                new System.Text.RegularExpressions.Regex(
                    @"UniVCI-(?<version>[+-]?[0-9]+[.]?[0-9]([eE][+-])?[0-9])",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase
                    | System.Text.RegularExpressions.RegexOptions.Singleline);
            var match = r.Match(exportedVciVersion);
            var version = match.Groups["version"].Value;
            return version;
        }

        private bool ImportableVersionCheck(string exportedVciVersion)
        {
            var version = GetVersionValue(exportedVciVersion);
            if (string.IsNullOrEmpty(version))
            {
                return false;
            }

            int itemExportedVersion = (int)Math.Round(float.Parse(version, CultureInfo.InvariantCulture) * 100);
            int uniVciVersion = (int)Math.Round(float.Parse(VCIVersion.VERSION, CultureInfo.InvariantCulture) * 100);
            if (itemExportedVersion > uniVciVersion)
            {
                return false;
            }

            return true;
        }

        public override void ParseJson(string json, IStorage storage)
        {
            // Deserialize
            base.ParseJson(json, storage);

            if (GLTF.extensions == null)
            {
                return;
            }

            glTF_VCAST_vci_meta meta;
            if (!GLTF.extensions.TryDeserializeExtensions(glTF_VCAST_vci_meta.ExtensionName, glTF_VCAST_vci_meta_Deserializer.Deserialize, out meta))
            {
                return;
            }

            VciMeta = meta;

            // VersionCheck
            if (VciMeta != null)
            {
                var version = VciMeta.exporterVCIVersion;

                if (!ImportableVersionCheck(version))
                {
                    throw new Exception("The current UniVCI cannot read this VCI version. " + version);
                }
            }

            // Set MaterialImporter
            glTF_VCAST_vci_material_unity material;
            if (GLTF.extensions.TryDeserializeExtensions(glTF_VCAST_vci_material_unity.ExtensionName, glTF_VCAST_vci_material_unity_Deserializer.Deserialize, out material))
            {
                // UniVCI v0.27以下のバージョンの場合は、baseColorをsrgbからlinearに変換した後にCreateMaterialを実行する
                bool srgbToLinear = false;
                if (material != null)
                {
                    var version = VciMeta.exporterVCIVersion;
                    int exportedVciVersion = (int)Math.Round(float.Parse(GetVersionValue(version), CultureInfo.InvariantCulture) * 100);
                    if (exportedVciVersion <= 27)
                    {
                        srgbToLinear = true;
                    }
                }

                SetMaterialImporter(new VCIMaterialImporter(this, material.materials, srgbToLinear));
            }

            // Set AnimationImporter
            SetAnimationImporter(new EachAnimationImporter());
        }

        protected override IEnumerator OnLoadModel()
        {
            if (VciMeta == null)
            {
                base.OnLoadModel();
            }

            yield break;
        }

        protected override IEnumerable<UnityEngine.Object> ObjectsForSubAsset()
        {
            HashSet<Texture2D> textures = new HashSet<Texture2D>();
            foreach (var x in GetTextures().SelectMany(y => y.GetTexturesForSaveAssets()))
            {
                if (!textures.Contains(x))
                {
                    textures.Add(x);
                }
            }
            foreach (var x in textures) { yield return x; }
            foreach (var x in GetMaterials()) { yield return x; }
            foreach (var x in Meshes) { yield return x.Mesh; }
            // Animationは独自に管理される
        }

        public IEnumerator SetupCoroutine()
        {
            VCIObject = Root.AddComponent<VCIObject>();
            yield return ToUnity(meta => VCIObject.Meta = meta);

            if (GLTF.extensions == null)
            {
                yield break;
            }

            if (VciMeta == null)
            {
                glTF_VCAST_vci_meta meta;
                if (!GLTF.extensions.TryDeserializeExtensions(glTF_VCAST_vci_meta.ExtensionName, glTF_VCAST_vci_meta_Deserializer.Deserialize, out meta))
                {
                    yield break;
                }
                else
                {
                    VciMeta = meta;
                }
            }


            // Script
            glTF_VCAST_vci_embedded_script scriptExtension;
            if (GLTF.extensions.TryDeserializeExtensions(glTF_VCAST_vci_embedded_script.ExtensionName, glTF_VCAST_vci_embedded_script_Deserializer.Deserialize, out scriptExtension))
            {
                VCIObject.Scripts.AddRange(scriptExtension.scripts.Select(x =>
                {
                    var source = "";
                    try
                    {
                        var bytes = GLTF.GetViewBytes(x.source);
                        source = Utf8String.Encoding.GetString(bytes.Array, bytes.Offset, bytes.Count);
                    }
                    catch (Exception)
                    {
                        // 握りつぶし
                    }

                    return new VCIObject.Script
                    {
                        name = x.name,
                        mimeType = x.mimeType,
                        targetEngine = x.targetEngine,
                        source = source
                    };
                }));
            }

            var exportedVciVersion = (int)Math.Round(float.Parse(GetVersionValue(VCIObject.Meta.exporterVersion),
                CultureInfo.InvariantCulture) * 100);

            // Audio
            if (GLTF.extensions.TryDeserializeExtensions(glTF_VCAST_vci_audios.ExtensionName, glTF_VCAST_vci_audios_Deserializer.Deserialize, out glTF_VCAST_vci_audios audioExtension))
            {
                // collect AudioClips from vci_audios
                var audioClips = new List<AudioClip>();
                foreach (var audio in audioExtension.audios)
                {
                    AudioClip audioClip = null;

#if ((NET_4_6 || NET_STANDARD_2_0) && UNITY_2017_1_OR_NEWER)
                    if (Application.isPlaying)
                    {
                        var bytes = GLTF.GetViewBytes(audio.bufferView);
                        yield return AudioImporter.Import(audio, bytes, clip => audioClip = clip);
                    }
                    else
#endif
                    {
#if UNITY_EDITOR
                        audioClip =
                            AssetDatabase.LoadAssetAtPath(AudioAssetPathList[audio.name], typeof(AudioClip)) as
                                AudioClip;
#endif
                    }

                    if (audioClip == null)
                    {
                        Debug.LogWarning($"Failed to import audio file: {audio.name}");
                    }
                    else
                    {
                        audioClips.Add(audioClip);
                    }
                }

                // * ver 0.32でAudioSource拡張が追加
                // - それ以前のバージョンで出力されている場合、
                // - すべてのAudioClip, AudioSourceはRootにアタッチされる
                if (exportedVciVersion < 32)
                {
                    foreach (var audioClip in audioClips)
                    {
                        var audioSource = Root.AddComponent<AudioSource>();
                        audioSource.clip = audioClip;
                        audioSource.playOnAwake = false;
                        audioSource.loop = false;
                        audioSource.spatialBlend = 0;
                        audioSource.dopplerLevel = 0;
                    }
                }
                else
                {
                    for (var i = 0; i < GLTF.nodes.Count; i++)
                    {
                        var node = GLTF.nodes[i];

                        if (node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_audio_sources.ExtensionName, glTF_VCAST_vci_audio_sources_Deserializer.Deserialize, out glTF_VCAST_vci_audio_sources audioSourceExtensions))
                        {
                            foreach (var audioSourceExtension in audioSourceExtensions.audioSources)
                            {
                                var audioClip = audioClips[audioSourceExtension.audio];

                                if (audioClip != null)
                                {
                                    var audioSource = Nodes[i].gameObject.AddComponent<AudioSource>();
                                    audioSource.clip = audioClip;
                                    audioSource.playOnAwake = false;
                                    audioSource.loop = false;
                                    audioSource.spatialBlend = audioSourceExtension.spatialBlend;
                                    audioSource.dopplerLevel = 0;
                                }
                                else
                                {
                                    Debug.LogWarning($"Audio file at index {audioSourceExtension.audio} was not found.");
                                }
                            }
                        }
                    }
                }
            }

            // Animation
            var rootAnimation = Root.GetComponent<Animation>();
            if (rootAnimation != null)
            {
                rootAnimation.playAutomatically = false;
            }

            // SubItem
            for (var i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                if (node.extensions != null)
                {
                    if (node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_item.ExtensionName, glTF_VCAST_vci_item_Deserializer.Deserialize, out glTF_VCAST_vci_item itemExtension))
                    {
                        var go = Nodes[i].gameObject;
                        var item = go.AddComponent<VCISubItem>();
                        item.NodeIndex = i;
                        item.Grabbable = itemExtension.grabbable;
                        item.Scalable = itemExtension.scalable;
                        item.UniformScaling = itemExtension.uniformScaling;
                        // UniVCI0.30で追加したフラグ。それ以前のVCIではGrabbable=trueならすべてTrueに
                        item.Attractable = itemExtension.grabbable &&
                                           (exportedVciVersion < 30 || itemExtension.attractable);
                        item.GroupId = itemExtension.groupId;
                    }
                }
            }

            // DefaultLayer
            for (var i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                var go = Nodes[i].gameObject;

                if (IsLocation)
                {
                    go.layer = _vciDefaultLayerProvider.Location;
                }
                else
                {
                    go.layer = _vciDefaultLayerProvider.Item;
                }
            }

            // Effekseer
            SetupEffekseer();

            // Text
            SetupText();

            // Physics
            SetupPhysics(_vciColliderLayerProvider);
            // Fix physics
            EnablePhysicalBehaviour(false);

            // Attachable
            SetupAttachable();

            // SpringBone
            SetupSpringBone();

            // PlayerSpawnPoint
            SetupPlayerSpawnPoint();

            // LocationBounds
            SetupLocationBounds();


            if (Application.isPlaying && IsLocation)
            {
                // Lightmap
                yield return SetupLightmapCoroutine();
            }
        }

        /// <summary>
        /// 物理関係のインポート
        /// </summary>
        public void SetupPhysics(IVciColliderLayerProvider vciColliderLayer = null)
        {
            // Collider and Rigidbody
            for (var i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                var go = Nodes[i].gameObject;

                if (node.extensions != null)
                {
                    glTF_VCAST_vci_colliders colliderExtension;
                    if (node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_colliders.ExtensionName, glTF_VCAST_vci_colliders_Deserializer.Deserialize, out colliderExtension))
                    {
                        foreach (var vciCollider in colliderExtension.colliders)
                        {
                            var collider = glTF_VCAST_vci_Collider.AddColliderComponent(go, vciCollider);
                            _colliderComponents.Add(collider);

                            // set layer
                            if (vciColliderLayer != null)
                            {
                                var layerNumber =
                                    VciColliderSetting.GetLayerNumber(VciColliderSetting.VciColliderLayerTypes.Default,
                                        vciColliderLayer);
                                go.layer = (layerNumber != -1) ? layerNumber : 0;
                            }

                            if (!string.IsNullOrEmpty(vciCollider.layer) && vciColliderLayer != null)
                            {
                                var layer = VciColliderSetting.VciColliderLayerLabel.FirstOrDefault(x =>
                                    x.Value == vciCollider.layer);
                                if (!string.IsNullOrEmpty(layer.Value))
                                {
                                    go.layer = VciColliderSetting.GetLayerNumber(layer.Key, vciColliderLayer);
                                }
                            }
                        }
                    }

                    glTF_VCAST_vci_rigidbody rigidbodyExtension;
                    glTF_VCAST_vci_item itemExtension;
                    if (node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_rigidbody.ExtensionName, glTF_VCAST_vci_rigidbody_Deserializer.Deserialize, out rigidbodyExtension))
                    {
                        foreach (var rigidbody in rigidbodyExtension.rigidbodies)
                        {
                            var rb = glTF_VCAST_vci_Rigidbody.AddRigidbodyComponent(go, rigidbody);
                            _rigidBodySettings.Add(rb, new RigidbodySetting(rb));
                        }
                    }
                    else if (node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_item.ExtensionName, glTF_VCAST_vci_item_Deserializer.Deserialize, out itemExtension))
                    {
                        // SubItemだがVCAST_vci_rigidbody拡張が無い場合、ここで追加する
                        var rb = go.GetOrAddComponent<Rigidbody>();
                        rb.isKinematic = true;
                        rb.useGravity = false;
                        _rigidBodySettings.Add(rb, new RigidbodySetting(rb));
                    }
                }
            }

            // Joint
            for (var i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                var go = Nodes[i].gameObject;

                if (node.extensions != null)
                {
                    glTF_VCAST_vci_joints jointExtension;
                    if (node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_joints.ExtensionName, glTF_VCAST_vci_joints_Deserializer.Deserialize, out jointExtension))
                    {
                        foreach (var joint in jointExtension.joints)
                        {
                            glTF_VCAST_vci_joint.AddJointComponent(go, joint, Nodes);
                        }
                    }
                }
            }
        }

        // Attachable
        public int SetupAttachable()
        {
            var attachableCount = 0;
            for (var i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];

                if (node.extensions != null)
                {
                    glTF_VCAST_vci_attachable attachableExtension;
                    if (node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_attachable.ExtensionName, glTF_VCAST_vci_attachable_Deserializer.Deserialize, out attachableExtension))
                    {
                        var go = Nodes[i].gameObject;
                        var attachable = go.AddComponent<VCIAttachable>();
                        attachable.AttachableHumanBodyBones = attachableExtension.attachableHumanBodyBones.Select(x =>
                        {
                            foreach (HumanBodyBones bone in Enum.GetValues(typeof(HumanBodyBones)))
                            {
                                if (x == bone.ToString())
                                {
                                    return bone;
                                }
                            }

                            throw new Exception("unknown AttachableHumanBodyBones: " + x);
                        }).ToArray();

                        attachable.AttachableDistance = attachableExtension.attachableDistance;
                        attachable.Scalable = attachableExtension.scalable;
                        attachable.Offset = attachableExtension.offset;
                        attachableCount++;
                    }
                }
            }

            return attachableCount;
        }

        /// <summary>
        /// VCI の物理挙動を有効・無効にする
        /// </summary>
        public void EnablePhysicalBehaviour(bool enable)
        {
            foreach (var collider in _colliderComponents)
            {
                if (collider != null)
                {
                    collider.enabled = enable;
                }
            }

            foreach (var rigidBodySetting in _rigidBodySettings)
            {
                if (rigidBodySetting.Key != null)
                {
                    if (enable)
                    {
                        // Restore the original constraints.
                        // NOTE: If EnablePhysicalBehaviour(false) was called before, OriginalConstraints contains
                        //       the constraints that were present when EnablePhysicalBehaviour(false) was last called.
                        //       If EnablePhysicalBehaviour(false) was never called, OriginalConstraints contains
                        //       the constraints that were setup at the time when the Rigidbody was initialized.
                        rigidBodySetting.Key.constraints = rigidBodySetting.Value.Constraints;
                    }
                    else
                    {
                        // Save constraints value to restore it later.
                        rigidBodySetting.Value.Constraints = rigidBodySetting.Key.constraints;

                        // Set constraints to FreezeAll to ensure that gameObjects aren't falling down due to gravity.
                        rigidBodySetting.Key.constraints = RigidbodyConstraints.FreezeAll;
                    }
                }
            }
        }

        public virtual void SetupEffekseer()
        {
            // Effekseer
            glTF_Effekseer effekseerExtension;
            if (GLTF.extensions.TryDeserializeExtensions(glTF_Effekseer.ExtensionName, glTF_Effekseer_Deserializer.Deserialize, out effekseerExtension))
            {
                for (var i = 0; i < GLTF.nodes.Count; i++)
                {
                    var node = GLTF.nodes[i];
                    var go = Nodes[i].gameObject;
                    glTF_Effekseer_emitters effekseerEmitterExtension;
                    if (node.extensions.TryDeserializeExtensions(glTF_Effekseer_emitters.ExtensionName, glTF_Effekseer_emitters_Deserializer.Deserialize, out effekseerEmitterExtension))
                    {
                        if (effekseerEmitterExtension == null || effekseerEmitterExtension.emitters == null)
                        {
                            continue;
                        }

                        foreach (var emitter in effekseerEmitterExtension.emitters)
                        {
                            var effectIndex = emitter.effectIndex;

                            if (Application.isPlaying)
                            {
                                var effect = effekseerExtension.effects[effectIndex];
                                var bodySegment = GLTF.GetViewBytes(effect.body.bufferView);
                                byte[] body = new byte[bodySegment.Count];
                                Buffer.BlockCopy(bodySegment.Array, bodySegment.Offset, body, 0, body.Count());

                                var resourcePath = new Effekseer.EffekseerResourcePath();
                                if (!Effekseer.EffekseerEffectAsset.ReadResourcePath(body, ref resourcePath))
                                {
                                    continue;
                                }

                                // Images
                                var effekseerTextures = new List<Effekseer.Internal.EffekseerTextureResource>();
                                if (effect.images != null && effect.images.Any())
                                {
                                    for (int t = 0; t < effect.images.Count; t++)
                                    {
                                        var image = effect.images[t];
                                        var path = resourcePath.TexturePathList[t];
                                        var buffer = GLTF.GetViewBytes(image.bufferView);

                                        if (image.mimeType == glTF_Effekseer_image.MimeTypeString.Png)
                                        {
                                            Texture2D texture = new Texture2D(2, 2);
                                            byte[] copyBuffer = new byte[buffer.Count];
                                            Buffer.BlockCopy(buffer.Array, buffer.Offset, copyBuffer, 0,
                                                copyBuffer.Count());
                                            texture.LoadImage(copyBuffer);
                                            effekseerTextures.Add(new Effekseer.Internal.EffekseerTextureResource()
                                            {
                                                path = path,
                                                texture = texture
                                            });
                                        }
                                        else
                                        {
                                            Debug.LogError(string.Format("image format {0} is not suppported.",
                                                image.mimeType));
                                        }
                                    }
                                }

                                // Models
                                var effekseerModels = new List<Effekseer.Internal.EffekseerModelResource>();
                                if (effect.models != null && effect.models.Any())
                                {
                                    for (int t = 0; t < effect.models.Count; t++)
                                    {
                                        var model = effect.models[t];
                                        var path = resourcePath.ModelPathList[t];
                                        path = Path.ChangeExtension(path, "asset");
                                        var modelSegment = GLTF.GetViewBytes(model.bufferView);
                                        byte[] modelBuffer = new byte[modelSegment.Count];
                                        Buffer.BlockCopy(modelSegment.Array, modelSegment.Offset, modelBuffer, 0,
                                            modelBuffer.Count());

                                        effekseerModels.Add(new Effekseer.Internal.EffekseerModelResource()
                                        {
                                            path = path,
                                            asset = new Effekseer.EffekseerModelAsset() { bytes = modelBuffer }
                                        });
                                    }
                                }

                                Effekseer.EffekseerEffectAsset effectAsset =
                                    ScriptableObject.CreateInstance<Effekseer.EffekseerEffectAsset>();
                                effectAsset.name = effect.effectName;
                                effectAsset.efkBytes = body;
                                effectAsset.Scale = effect.scale;
                                effectAsset.textureResources = effekseerTextures.ToArray();
                                effectAsset.modelResources = effekseerModels.ToArray();
                                effectAsset.soundResources = new Effekseer.Internal.EffekseerSoundResource[0];

                                var emitterComponent = go.AddComponent<Effekseer.EffekseerEmitter>();
                                emitterComponent.effectAsset = effectAsset;
                                emitterComponent.playOnStart = emitter.isPlayOnStart;
                                emitterComponent.isLooping = emitter.isLoop;

                                emitterComponent.effectAsset.LoadEffect();
                                EffekseerEmitterComponents.Add(emitterComponent);
                            }
                            else
                            {
#if UNITY_EDITOR
                                var emitterComponent = go.AddComponent<Effekseer.EffekseerEmitter>();
                                emitterComponent.effectAsset = EffekseerEffectAssets[effectIndex];
                                emitterComponent.playOnStart = emitter.isPlayOnStart;
                                emitterComponent.isLooping = emitter.isLoop;
                                EffekseerEmitterComponents.Add(emitterComponent);
#endif
                            }
                        }
                    }
                }
            }
        }

        public void SetupText()
        {
            for (var i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                glTF_VCAST_vci_text textExtension;
                glTF_VCAST_vci_rectTransform rectTransformExtension;
                if (node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_text.ExtensionName, glTF_VCAST_vci_text_Deserializer.Deserialize, out textExtension)
                    && node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_rectTransform.ExtensionName, glTF_VCAST_vci_rectTransform_Deserializer.Deserialize, out rectTransformExtension)
                    )
                {
                    var go = Nodes[i].gameObject;
                    var rt = go.AddComponent<RectTransform>();
                    // RectTransformのAddComponentで元のtransformが置き換わるのでNodesも更新する。
                    Nodes[i] = rt.transform;

                    var tmp = go.AddComponent<TextMeshPro>();
                    var vci_text = textExtension.text;

                    // Get font
                    if (_fontProvider != null)
                    {
                        var font = _fontProvider.GetDefaultFont();
                        if (font != null) tmp.font = font;
                    }

                    // Set TMPText
                    tmp.text = vci_text.text;
                    tmp.richText = vci_text.richText;
                    tmp.fontSize = vci_text.fontSize;
                    tmp.autoSizeTextContainer = vci_text.autoSize;
                    tmp.fontStyle = (FontStyles)vci_text.fontStyle;
                    tmp.color = new Color(vci_text.color[0], vci_text.color[1], vci_text.color[2], vci_text.color[3]);
                    tmp.enableVertexGradient = vci_text.enableVertexGradient;
                    tmp.colorGradient = new VertexGradient(
                        new Color(vci_text.topLeftColor[0], vci_text.topLeftColor[1], vci_text.topLeftColor[2],
                            vci_text.topLeftColor[3]),
                        new Color(vci_text.topRightColor[0], vci_text.topRightColor[1], vci_text.topRightColor[2],
                            vci_text.topRightColor[3]),
                        new Color(vci_text.bottomLeftColor[0], vci_text.bottomLeftColor[1], vci_text.bottomLeftColor[2],
                            vci_text.bottomLeftColor[3]),
                        new Color(vci_text.bottomRightColor[0], vci_text.bottomRightColor[1],
                            vci_text.bottomRightColor[2], vci_text.bottomRightColor[3])
                    );
                    tmp.characterSpacing = vci_text.characterSpacing;
                    tmp.wordSpacing = vci_text.wordSpacing;
                    tmp.lineSpacing = vci_text.lineSpacing;
                    tmp.paragraphSpacing = vci_text.paragraphSpacing;
                    tmp.alignment = (TextAlignmentOptions)vci_text.alignment;
                    tmp.enableWordWrapping = vci_text.enableWordWrapping;
                    // overflowModeのインポート時にエラーになる可能性があるので無効にする。
                    //tmp.overflowMode = (TextOverflowModes) vci_text.overflowMode;
                    tmp.enableKerning = vci_text.enableKerning;
                    tmp.extraPadding = vci_text.extraPadding;
                    tmp.margin = new Vector4(vci_text.margin[0], vci_text.margin[1], vci_text.margin[2],
                        vci_text.margin[3]);

                    // Set RectTransform
                    var vci_rt = rectTransformExtension.rectTransform;
                    rt.anchorMin = new Vector2(vci_rt.anchorMin[0], vci_rt.anchorMin[1]);
                    rt.anchorMax = new Vector2(vci_rt.anchorMax[0], vci_rt.anchorMax[1]);
                    rt.anchoredPosition = new Vector2(vci_rt.anchoredPosition[0], vci_rt.anchoredPosition[1]);
                    rt.sizeDelta = new Vector2(vci_rt.sizeDelta[0], vci_rt.sizeDelta[1]);
                    rt.pivot = new Vector2(vci_rt.pivot[0], vci_rt.pivot[1]);

                    AddText(tmp);
                }
            }
        }

        public void SetupSpringBone()
        {
            glTF_VCAST_vci_spring_bone springBoneExtension;
            if (GLTF.extensions.TryDeserializeExtensions(glTF_VCAST_vci_spring_bone.ExtensionName, glTF_VCAST_vci_spring_bone_Deserializer.Deserialize, out springBoneExtension))
            {
                foreach (var bone in springBoneExtension.springBones)
                {
                    var sb = Root.AddComponent<VCISpringBone>();
                    if (bone.center >= 0) sb.m_center = Nodes[bone.center];
                    sb.m_dragForce = bone.dragForce;
                    sb.m_gravityDir = bone.gravityDir;
                    sb.m_gravityPower = bone.gravityPower;
                    sb.m_hitRadius = bone.hitRadius;
                    sb.m_stiffnessForce = bone.stiffiness;
                    if (bone.colliderIds != null && bone.colliderIds.Any())
                        sb.m_colliderObjects = bone.colliderIds.Select(id => Nodes[id]).ToList();
                    sb.RootBones = bone.bones.Select(x => Nodes[x]).ToList();
                }
            }
        }

        public void SetupPlayerSpawnPoint()
        {
            for (var i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                if (node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_player_spawn_point.ExtensionName, glTF_VCAST_vci_player_spawn_point_Deserializer.Deserialize,
                    out glTF_VCAST_vci_player_spawn_point playerSpawnPointExtension))
                {
                    var go = Nodes[i].gameObject;
                    var spawnPoint = go.AddComponent<VCIPlayerSpawnPoint>();
                    spawnPoint.Order = playerSpawnPointExtension.playerSpawnPoint.order;
                    spawnPoint.Radius = playerSpawnPointExtension.playerSpawnPoint.radius;

                    if (node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_player_spawn_point_restriction.ExtensionName, glTF_VCAST_vci_player_spawn_point_restriction_Deserializer.Deserialize,
                        out glTF_VCAST_vci_player_spawn_point_restriction playerSpawnPointRestrictionExtension))
                    {
                        var spawnPointRestriction = go.AddComponent<VCIPlayerSpawnPointRestriction>();
                        var nodePspR = playerSpawnPointRestrictionExtension.playerSpawnPointRestriction;
                        switch (nodePspR.rangeOfMovementRestriction)
                        {
                            case glTF_VCAST_vci_PlayerSpawnPointRestriction.Circle:
                                spawnPointRestriction.RangeOfMovementRestriction = RangeOfMovement.Circle;
                                break;
                            case glTF_VCAST_vci_PlayerSpawnPointRestriction.Rectangle:
                                spawnPointRestriction.RangeOfMovementRestriction = RangeOfMovement.Rectangle;
                                break;
                            case glTF_VCAST_vci_PlayerSpawnPointRestriction.NoLimit:
                            default:
                                spawnPointRestriction.RangeOfMovementRestriction = RangeOfMovement.NoLimit;
                                break;
                        }

                        spawnPointRestriction.LimitRadius = playerSpawnPointRestrictionExtension
                            .playerSpawnPointRestriction.limitRadius;
                        spawnPointRestriction.LimitRectLeft = nodePspR.limitRectLeft;
                        spawnPointRestriction.LimitRectRight = nodePspR.limitRectRight;
                        spawnPointRestriction.LimitRectForward = nodePspR.limitRectForward;
                        spawnPointRestriction.LimitRectBackward = nodePspR.limitRectBackward;

                        switch (nodePspR.postureRestriction)
                        {
                            case glTF_VCAST_vci_PlayerSpawnPointRestriction.SitOn:
                                spawnPointRestriction.PostureRestriction = Posture.SitOn;
                                break;
                            case glTF_VCAST_vci_PlayerSpawnPointRestriction.NoLimit:
                            default:
                                spawnPointRestriction.PostureRestriction = Posture.NoLimit;
                                break;
                        }

                        spawnPointRestriction.SeatHeight = nodePspR.seatHeight;
                    }
                }
            }
        }

        public void SetupLocationBounds()
        {
            glTF_VCAST_vci_location_bounds locationBoundsExtension;
            if (GLTF.extensions.TryDeserializeExtensions(glTF_VCAST_vci_location_bounds.ExtensionName, glTF_VCAST_vci_location_bounds_Deserializer.Deserialize, out locationBoundsExtension))
            {
                var locationBounds = Root.AddComponent<VCILocationBounds>();
                var values = locationBoundsExtension.LocationBounds;
                locationBounds.Bounds = new Bounds(values.bounds_center, values.bounds_size);
            }
        }

        public IEnumerator SetupLightmapCoroutine()
        {
            var supportImporting = Application.isPlaying;
            if (!supportImporting)
            {
                yield break;
            }

            glTF_VCAST_vci_location_lighting locationLightingExtension;
            if (!GLTF.extensions.TryDeserializeExtensions(glTF_VCAST_vci_location_lighting.ExtensionName, glTF_VCAST_vci_location_lighting_Deserializer.Deserialize, out locationLightingExtension))
            {
                yield break;
            }

            if (locationLightingExtension.locationLighting == null)
            {
                yield break;
            }

            var locationLighting = locationLightingExtension.locationLighting;

            // 現在のところはこの組み合わせしかサポートしない
            if (locationLighting.skyboxCubemap.GetSkyboxCompressionModeAsEnum() !=
                CubemapCompressionType.Rgbm) yield break;
            if (locationLighting.GetLightmapCompressionModeAsEnum() != LightmapCompressionType.Rgbm) yield break;
            if (locationLighting.GetLightmapDirectionalModeAsEnum() != LightmapDirectionalType.NonDirectional)
                yield break;

            // Lightmap
            var lightmapCompressionMode = locationLighting.GetLightmapCompressionModeAsEnum();
            var lightmapDirectionalMode = locationLighting.GetLightmapDirectionalModeAsEnum();
            var lightmapTextureImporter =
                new LightmapTextureImporter(GetTexture, lightmapCompressionMode, lightmapDirectionalMode);
            var colorTextures = new List<Texture2D>();
            for (var idx = 0; idx < locationLighting.lightmapTextures.Length; ++idx)
            {
                var x = locationLighting.lightmapTextures[idx];
                var importerResult = new LightmapTextureImporterResult();
                yield return lightmapTextureImporter.GetOrConvertColorTextureCoroutine(x.index, importerResult);
                colorTextures.Add(importerResult.Result);
            }

            for (var i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                glTF_VCAST_vci_lightmap lightmapExtension;
                if (node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_lightmap.ExtensionName, glTF_VCAST_vci_lightmap_Deserializer.Deserialize, out lightmapExtension))
                {
                    var go = Nodes[i].gameObject;
                    var renderer = go.GetComponent<MeshRenderer>();
                    if (renderer == null) continue;

                    var lightmap = lightmapExtension.lightmap;
                    var importerResult = new LightmapTextureImporterResult();
                    yield return lightmapTextureImporter.GetOrConvertColorTextureCoroutine(lightmap.texture.index,
                        importerResult);
                    var offset = new Vector2(lightmap.offset[0], lightmap.offset[1]);
                    var scale = new Vector2(lightmap.scale[0], lightmap.scale[1]);

                    // Coordinate conversion
                    offset.y = (offset.y + scale.y - 1.0f) * -1.0f;

                    // Get LightmapData Index
                    var lightmapDataIndex = colorTextures.FindIndex(x => x == importerResult.Result);
                    if (lightmapDataIndex == -1) continue;

                    // Apply to Renderer
                    renderer.lightmapIndex = lightmapDataIndex;
                    renderer.lightmapScaleOffset = new Vector4(scale.x, scale.y, offset.x, offset.y);
                }
            }

            // Ambient Lighting
            var skyboxCompressionMode = locationLighting.skyboxCubemap.GetSkyboxCompressionModeAsEnum();
            var skyboxCubemapImporter = new CubemapTextureImporter(GetTexture, skyboxCompressionMode);
            var skyboxImporter = new SkyboxImporter(skyboxCubemapImporter);
            var lightProbeImporter = new LightProbeImporter();

            var behaviour = Root.AddComponent<VCILocationLighting>();
            behaviour.LightmapDataArray = colorTextures.Select(x => new LightmapData { lightmapColor = x }).ToArray();
            switch (lightmapDirectionalMode)
            {
                case LightmapDirectionalType.Directional:
                    behaviour.LightmapMode = LightmapsMode.CombinedDirectional;
                    break;
                case LightmapDirectionalType.NonDirectional:
                    behaviour.LightmapMode = LightmapsMode.NonDirectional;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var skyboxResult = new SkyboxImporterResult();
            yield return skyboxImporter.CovertToSkyboxCoroutine(locationLighting.skyboxCubemap, skyboxResult);
            behaviour.Skybox = skyboxResult.Result;
            var (lightProbePosArray, lightProbeCoefficientArray) =
                lightProbeImporter.Import(locationLighting.lightProbes);
            behaviour.LightProbePositions = lightProbePosArray;
            behaviour.LightProbeCoefficients = lightProbeCoefficientArray;

            yield return null;


            // ReflectionProbe
            for (var i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                glTF_VCAST_vci_reflectionProbe reflectionProbeExtension;
                if (node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_reflectionProbe.ExtensionName, glTF_VCAST_vci_reflectionProbe_Deserializer.Deserialize, out reflectionProbeExtension))
                {
                    var go = Nodes[i].gameObject;
                    var reflectionProbe = go.AddComponent<ReflectionProbe>();

                    var data = reflectionProbeExtension.reflectionProbe;
                    var gltfOffset = data.boxOffset;
                    var gltfSize = data.boxSize;
                    var cubemapImporter =
                        new CubemapTextureImporter(GetTexture, data.cubemap.GetSkyboxCompressionModeAsEnum());

                    var cubemapResult = new CubemapTextureImporterResult();
                    yield return cubemapImporter.ConvertCubemapCoroutine(data.cubemap, cubemapResult);

                    reflectionProbe.center = new Vector3(-gltfOffset[0], gltfOffset[1], gltfOffset[2]); // Invert X-axis
                    reflectionProbe.size = new Vector3(gltfSize[0], gltfSize[1], gltfSize[2]);
                    reflectionProbe.intensity = data.intensity;
                    reflectionProbe.boxProjection = data.useBoxProjection;
                    reflectionProbe.bakedTexture = cubemapResult.Result;

                    reflectionProbe.mode = ReflectionProbeMode.Baked;
                }
            }
        }

        public void ExtractAudio(UnityPath prefabPath)
        {
#if UNITY_EDITOR

            glTF_VCAST_vci_audios audioExtension;
            if (!GLTF.extensions.TryDeserializeExtensions(glTF_VCAST_vci_audios.ExtensionName, glTF_VCAST_vci_audios_Deserializer.Deserialize, out audioExtension))
            {
                return;
            }

            if (audioExtension == null
                || audioExtension.audios == null
                || audioExtension.audios.Count == 0)
            {
                return;
            }

            var prefabParentDir = prefabPath.Parent;

            // glb buffer
            var folder = prefabPath.GetAssetFolder(".Audios");

            var created = 0;
            for (var i = 0; i < audioExtension.audios.Count; ++i)
            {
                folder.EnsureFolder();
                var audio = audioExtension.audios[i];
                var bytes = GLTF.GetViewBytes(audio.bufferView);

                var audioBuffer = new byte[bytes.Count];
                Buffer.BlockCopy(bytes.Array, bytes.Offset, audioBuffer, 0, audioBuffer.Length);

                System.Text.RegularExpressions.Regex r =
                    new System.Text.RegularExpressions.Regex(
                        @"(?<type>.*)/(?<ext>.*)",
                        System.Text.RegularExpressions.RegexOptions.IgnoreCase
                        | System.Text.RegularExpressions.RegexOptions.Singleline);
                var match = r.Match(audio.mimeType);
                var ext = match.Groups["ext"].Value;
                if (!string.IsNullOrEmpty(ext))
                {
                    var path = string.Format("{0}/{1}.{2}", folder.Value, audio.name, ext);
                    File.WriteAllBytes(path, audioBuffer);
                    AudioAssetPathList.Add(audio.name, path);
                }

                created++;
            }

            if (created > 0) AssetDatabase.Refresh();
#endif
        }

        public void ExtractEffekseer(UnityPath prefabPath)
        {
#if UNITY_EDITOR

            glTF_Effekseer effekseerExtension;
            if (!GLTF.extensions.TryDeserializeExtensions(glTF_Effekseer.ExtensionName, glTF_Effekseer_Deserializer.Deserialize, out effekseerExtension))
            {
                return;
            }

            if (effekseerExtension == null
                || effekseerExtension.effects == null
                || effekseerExtension.effects.Count == 0)
            {
                return;
            }

            var prefabParentDir = prefabPath.Parent;
            var folder = prefabPath.GetAssetFolder(".Effekseers");

            for (var i = 0; i < effekseerExtension.effects.Count; ++i)
            {
                folder.EnsureFolder();
                var effect = effekseerExtension.effects[i];
                var effectDir = string.Format("{0}/{1}", folder.Value, effect.effectName);
                SafeCreateDirectory(effectDir);
                var textureDir = string.Format("{0}/{1}", effectDir, "Textures");
                var modelDir = string.Format("{0}/{1}", effectDir, "Models");

                var body = GLTF.GetViewBytes(effect.body.bufferView).ToArray();
                var resourcePath = new Effekseer.EffekseerResourcePath();
                if (!Effekseer.EffekseerEffectAsset.ReadResourcePath(body, ref resourcePath))
                {
                    continue;
                }

                // Texture
                var writeTexturePathList = new List<string>();
                if (effect.images != null && effect.images.Any())
                {
                    for (int t = 0; t < effect.images.Count; t++)
                    {
                        var image = effect.images[t];
                        var path = resourcePath.TexturePathList[t];
                        var buffer = GLTF.GetViewBytes(image.bufferView);
                        var texturePath = string.Format("{0}/{1}", textureDir, Path.GetFileName(path));
                        SafeCreateDirectory(Path.GetDirectoryName(texturePath));

                        if (image.mimeType == glTF_Effekseer_image.MimeTypeString.Png)
                        {
                            File.WriteAllBytes(texturePath, buffer.ToArray());
                            writeTexturePathList.Add(texturePath);
                        }
                        else
                        {
                            Debug.LogError(string.Format("image format {0} is not suppported.", image.mimeType));
                        }
                    }
                }

                // Models
                if (effect.models != null && effect.models.Any())
                {
                    for (int t = 0; t < effect.models.Count; t++)
                    {
                        var model = effect.models[t];
                        var path = resourcePath.ModelPathList[t];
                        var buffer = GLTF.GetViewBytes(model.bufferView);

                        var modelPath = string.Format("{0}/{1}", modelDir, Path.GetFileName(path));
                        SafeCreateDirectory(Path.GetDirectoryName(modelPath));

                        File.WriteAllBytes(modelPath, buffer.ToArray());
                        AssetDatabase.ImportAsset(modelPath);
                    }
                }

                EditorApplication.delayCall += () =>
                {
                    // fix texture setting
                    foreach (var texturePath in writeTexturePathList)
                    {
                        AssetDatabase.ImportAsset(texturePath);
                        var textureImporter = (TextureImporter)TextureImporter.GetAtPath(texturePath);
                        if (textureImporter != null)
                        {
                            textureImporter.isReadable = true;
                            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
                            textureImporter.SaveAndReimport();
                        }
                    }

                    // efk
                    var assetPath = string.Format("{0}/{1}.efk", effectDir, effect.effectName);
                    Effekseer.EffekseerEffectAsset.CreateAsset(assetPath, body);
                    var effectAsset =
                        AssetDatabase.LoadAssetAtPath<Effekseer.EffekseerEffectAsset>(
                            System.IO.Path.ChangeExtension(assetPath, "asset"));
                    EffekseerEffectAssets.Add(effectAsset);


                    // find assets
                    // textures
                    for (int t = 0; t < effectAsset.textureResources.Count(); t++)
                    {
                        var path = effectAsset.textureResources[t].path;
                        if (string.IsNullOrEmpty(path))
                            continue;

                        var fileName = Path.GetFileName(path);
                        if (string.IsNullOrEmpty(fileName))
                            continue;

                        Texture2D texture = UnityEditor.AssetDatabase.LoadAssetAtPath(
                            string.Format("{0}/{1}", textureDir, fileName), typeof(Texture2D)) as Texture2D;

                        if (texture != null)
                        {
                            effectAsset.textureResources[t].texture = texture;
                        }
                    }

                    // models
                    for (int t = 0; t < effectAsset.modelResources.Count(); t++)
                    {
                        var path = effectAsset.modelResources[t].path;
                        if (string.IsNullOrEmpty(path))
                            continue;

                        var fileName = Path.GetFileName(path);
                        if (string.IsNullOrEmpty(fileName))
                            continue;

                        Effekseer.EffekseerModelAsset model = UnityEditor.AssetDatabase.LoadAssetAtPath(
                                string.Format("{0}/{1}", modelDir, fileName), typeof(Effekseer.EffekseerModelAsset)) as
                            Effekseer.EffekseerModelAsset;

                        if (model != null)
                        {
                            effectAsset.modelResources[t].asset = model;
                        }
                    }
                };
            }
#endif
        }

        public void ExtractAnimation(UnityPath prefabPath)
        {
#if UNITY_EDITOR

            if (GLTF.animations == null || GLTF.animations.Count == 0)
                return;

            var prefabParentDir = prefabPath.Parent;
            var folder = prefabPath.GetAssetFolder(".Animation");
            folder.EnsureFolder();

            var inverter = Axises.Z.Create();

            var animationClips = new AnimationClip[GLTF.animations.Count];
            for (int i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                glTF_VCAST_vci_animation animationProbeExtension;
                if (!node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_animation.ExtensionName, glTF_VCAST_vci_animation_Deserializer.Deserialize, out animationProbeExtension))
                {
                    continue;
                }

                var vciAnimation = animationProbeExtension;

                foreach (var animationReference in vciAnimation.animationReferences)
                {
                    var gltfAnimation = GLTF.animations[animationReference.animation];
                    AnimationClip clip = null;
                    if (animationClips[animationReference.animation] == null)
                    {
                        clip = AnimationImporterUtil.ConvertAnimationClip(GLTF, gltfAnimation, inverter, node);
                        animationClips[animationReference.animation] = clip;
                    }
                }
            }

            // Import root animation clips.
            for (var i = 0; i < animationClips.Length; i++)
            {
                if (animationClips[i] != null) continue;
                animationClips[i] = AnimationImporterUtil.ConvertAnimationClip(GLTF, GLTF.animations[i], inverter);
            }

            var saveAnimationPath = new string[GLTF.animations.Count];
            for (int i = 0; i < animationClips.Count(); i++)
            {
                if (animationClips[i] != null)
                {
                    var path = string.Format("{0}/{1}.{2}", folder.Value, animationClips[i].name, "asset");
                    AssetDatabase.CreateAsset(animationClips[i], path);
                    saveAnimationPath[i] = path;
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var saveAnimationClips = new AnimationClip[GLTF.animations.Count];
            for (int i = 0; i < saveAnimationPath.Count(); i++)
            {
                if (!string.IsNullOrEmpty(saveAnimationPath[i]))
                {
                    saveAnimationClips[i] =
                        AssetDatabase.LoadAssetAtPath(saveAnimationPath[i], typeof(AnimationClip)) as AnimationClip;
                }
            }

            AnimationClips = new List<AnimationClip>(saveAnimationClips);
#endif
        }


        public void ExportScriptFile(UnityPath prefabPath)
        {
#if UNITY_EDITOR
            var prefabParentDir = prefabPath.Parent;
            var folder = prefabPath.GetAssetFolder(".Scripts");
            folder.EnsureFolder();

            var vciObject = this.Root.GetComponent<VCIObject>();
            if (vciObject == null)
            {
                return;
            }

            foreach (var script in vciObject.Scripts)
            {
                var scriptPath = $"{folder.Value}/{script.name}.lua";
                Debug.Log(scriptPath);
                File.WriteAllBytes(scriptPath, System.Text.Encoding.UTF8.GetBytes(script.source));
            }

            AssetDatabase.Refresh();

            foreach (var script in vciObject.Scripts)
            {
                var scriptPath = $"{folder.Value}/{script.name}.lua";
                script.textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(scriptPath);
            }
#endif
        }

        private static DirectoryInfo SafeCreateDirectory(string path)
        {
#if UNITY_EDITOR
            try
            {
                if (Directory.Exists(path))
                {
                    return null;
                }

                var info = Directory.CreateDirectory(path);
                AssetDatabase.ImportAsset(path);
                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }

#else
            return null;
#endif
        }

#if UNITY_EDITOR
        protected override UnityPath GetAssetPath(UnityPath prefabPath, UnityEngine.Object o)
        {
            if (o is AnimationClip)
            {
                var dir = prefabPath.GetAssetFolder(".Animation");
                var assetPath = dir.Child(o.name.EscapeFilePath() + ".asset");
                return assetPath;
            }
            else
            {
                return base.GetAssetPath(prefabPath, o);
            }
        }
#endif

        #region Meta

        [Serializable]
        public struct Meta
        {
            public string title;
            public string author;
            public string contactInformation;
            public string reference;
            public Texture2D thumbnail;
            public string version;

            [SerializeField] [TextArea(1, 16)] public string description;

            public string exporterVersion;
            public string specVersion;


            #region Model Data License Url

            [Header("Model Data License")] public LicenseType modelDataLicenseType;
            public string modelDataOtherLicenseUrl;

            #endregion

            #region Script License Url

            [Header("Script License")] public LicenseType scriptLicenseType;
            public string scriptOtherLicenseUrl;

            #endregion

            #region Script Settings

            [Header("Script Settings")] public bool scriptWriteProtected;
            public bool scriptEnableDebugging;

            public ScriptFormat scriptFormat;

            #endregion
        }

        public IEnumerator ToUnity(Action<Meta> callback)
        {
            if (VciMeta == null)
            {
                callback(default(Meta));
                yield break;
            }

            var meta = VciMeta;
            if (meta == null)
            {
                callback(default(Meta));
                yield break;
            }

            var texture = GetTexture(meta.thumbnail);
            if (texture != null) yield return texture.ProcessCoroutine(GLTF, Storage);

            callback(new Meta
            {
                exporterVersion = meta.exporterVCIVersion,
                specVersion = meta.specVersion,

                title = meta.title,
                version = meta.version,
                author = meta.author,
                contactInformation = meta.contactInformation,
                reference = meta.reference,
                description = meta.description,
                thumbnail = texture != null ? texture.Texture : null,

                modelDataLicenseType = meta.modelDataLicenseType,
                modelDataOtherLicenseUrl = meta.modelDataOtherLicenseUrl,
                scriptLicenseType = meta.scriptLicenseType,
                scriptOtherLicenseUrl = meta.scriptOtherLicenseUrl,

                scriptWriteProtected = meta.scriptWriteProtected,
                scriptEnableDebugging = meta.scriptEnableDebugging,
                scriptFormat = meta.scriptFormat,
            });
        }

        #endregion

        /// <summary>
        /// VCIファイルとして書き出す
        /// </summary>
        /// <param name="path"></param>
        public void Rewrite(string path)
        {
            /* ToDo
            GLTF.extensions.VCAST_vci_audios = null;
            var bytes = GLTF.ToGlbBytes(true);
            File.WriteAllBytes(path, bytes);
            */
        }

        public static VCIImporter CreateForTest()
        {
            var root = new GameObject("root");
            var vciObject = root.AddComponent<VCIObject>();
            var item = new GameObject("item");
            item.AddComponent<VCISubItem>();
            item.transform.SetParent(root.transform);

            var importer = new VCIImporter()
            {
                Root = root,
                VCIObject = vciObject,
            };

            return importer;
        }

        public void AddMeshWitMaterials(MeshWithMaterials meshWithMats)
        {
            Meshes.Add(meshWithMats);
        }

        List<TextMeshPro> m_texts = new List<TextMeshPro>();

        public void AddText(TextMeshPro text)
        {
            m_texts.Add(text);
        }

        public IEnumerable<TextMeshPro> GetTexts(string id)
        {
            return m_texts.Where(t => t.name.ToLower() == id.ToLower());
        }
    }
}