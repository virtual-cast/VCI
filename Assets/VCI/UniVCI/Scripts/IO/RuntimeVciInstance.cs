using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Effekseer;
using TMPro;
using UniGLTF;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public sealed class RuntimeVciInstance : IDisposable, IResponsibilityForDestroyObjects
    {
        public RuntimeGltfInstance GltfModel { get; }
        public GameObject Root => GltfModel.Root;
        public IReadOnlyList<Transform> Nodes { get; }
        public IReadOnlyList<AnimationClip> AnimationClips { get; }
        public IReadOnlyList<AudioClip> AudioClips { get; }
        public IReadOnlyList<PhysicMaterial> PhysicMaterials { get; }
        public VCIObject VCIObject { get; }
        public IReadOnlyList<Material> LoadedMaterials { get; }
        public IReadOnlyList<Collider> ColliderComponents { get; }
        public IReadOnlyDictionary<Rigidbody, RigidbodySetting> RigidbodySettings { get; }
        public IReadOnlyList<Effekseer.EffekseerEmitter> EffekseerEmitterComponents { get; }
        public IReadOnlyList<Renderer> RendererComponents { get; }

        private readonly List<(SubAssetKey, UnityEngine.Object)> _runtimeVciResources;
        private readonly List<(string key, TextMeshPro text)> _texts;

        public RuntimeVciInstance(
            RuntimeGltfInstance gltfModel,
            List<(SubAssetKey, UnityEngine.Object)> runtimeVciResources,
            IReadOnlyList<Transform> nodes,
            IReadOnlyList<Collider> colliders,
            IReadOnlyDictionary<Rigidbody, RigidbodySetting> rigidbodies,
            IReadOnlyList<TextMeshPro> texts,
            IReadOnlyList<EffekseerEmitter> effekseerEmitterComponents)
        {
            // NOTE: GLTF Objects
            GltfModel = gltfModel;
            Nodes = nodes;
            RendererComponents = Root.GetComponentsInChildren<Renderer>(includeInactive: true);

            // NOTE: GLTF Resources
            AnimationClips = GltfModel.RuntimeResources
                .Where(x => x.Item1.Type == typeof(AnimationClip))
                .Select(x => x.Item2 as AnimationClip)
                .ToList();
            LoadedMaterials = GltfModel.RuntimeResources
                .Where(x => x.Item1.Type == typeof(Material))
                .Select(x => x.Item2 as Material)
                .ToList();

            // NOTE: VCI Objects
            VCIObject = Root.GetComponent<VCIObject>();
            ColliderComponents = colliders;
            RigidbodySettings = rigidbodies;
            EffekseerEmitterComponents = effekseerEmitterComponents;
            _texts = texts.Select(x => (x.name.ToLower(CultureInfo.InvariantCulture), x)).ToList();

            // NOTE: VCI Resources
            _runtimeVciResources = runtimeVciResources;
            AudioClips = _runtimeVciResources
                .Where(x => x.Item1.Type == typeof(AudioClip))
                .Select(x => x.Item2 as AudioClip)
                .ToList();
            PhysicMaterials = _runtimeVciResources
                .Where(x => x.Item1.Type == typeof(PhysicMaterial))
                .Select(x => x.Item2 as PhysicMaterial)
                .ToList();
        }

        public void Dispose()
        {
            TransferOwnership((key, obj) => UnityObjectDestoyer.DestroyRuntimeOrEditor(obj));

            if (GltfModel != null)
            {
                GltfModel.Dispose();
            }
        }

        public void TransferOwnership(TakeResponsibilityForDestroyObjectFunc take)
        {
            foreach (var (key, obj) in _runtimeVciResources)
            {
                take(key, obj);
            }
            _runtimeVciResources.Clear();

            if (GltfModel != null)
            {
                GltfModel.TransferOwnership(take);
            }
        }

        public void ShowMeshes()
        {
            // Lua スプリプトの呼び出し順の問題があるため
            // この関数を呼び出したときにすでに SetActive(false) を呼び出された GameObject がある可能性がある。
            // したがって、非アクティブな GameObject も対象に含めるよう注意する必要がある。
            // その点、GltfModel.ShowMeshes() はそれを満たさない。
            // GltfModel.ShowMeshes();
            foreach (var renderer in RendererComponents)
            {
                renderer.enabled = true;
            }
        }

        public void EnableUpdateWhenOffscreen()
        {
            // ShowMeshes() と同様の理由で GltfModel の実装は使わない。
            // GltfModel.EnableUpdateWhenOffscreen();
            foreach (var renderer in RendererComponents)
            {
                if (renderer != null && renderer is SkinnedMeshRenderer skinnedRenderer)
                {
                    skinnedRenderer.updateWhenOffscreen = true;
                }
            }
        }

        /// <summary>
        /// VCI の物理挙動を有効・無効にする.
        /// RuntimeVciInstance が生成されたとき（ Importer.LoadAsync() が完了したとき）は、無効化されている.
        /// </summary>
        public void EnablePhysicalBehaviour(bool enable)
        {
            foreach (var collider in ColliderComponents)
            {
                if (enable)
                {
                    PhysicalBehaviourChanger.EnableCollider(collider);
                }
                else
                {
                    PhysicalBehaviourChanger.DisableCollider(collider);
                }
            }

            foreach (var kv in RigidbodySettings)
            {
                var rigidbody = kv.Key;
                var fileDefaultSetting = kv.Value;

                if (enable)
                {
                    PhysicalBehaviourChanger.EnableRigidbody(rigidbody, fileDefaultSetting);
                }
                else
                {
                    PhysicalBehaviourChanger.DisableRigidbody(rigidbody);
                }
            }
        }

        public IEnumerable<TextMeshPro> GetTexts(string id)
        {
            var sanitizedId = id.ToLower(CultureInfo.InvariantCulture);

            foreach (var (key, text) in _texts)
            {
                if (key == sanitizedId)
                {
                    yield return text;
                }
            }
        }
    }
}