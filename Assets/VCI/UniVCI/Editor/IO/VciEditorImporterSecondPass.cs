using System;
using System.Collections.Generic;
using UniGLTF;
using UnityEditor;
using UnityEngine;
using VRMShaders;
using Object = UnityEngine.Object;

namespace VCI
{
    /// <summary>
    /// Editor で 2 回目の VCI インポートを行う.
    /// 2 回目は、1 回目で展開したアセットファイルを使って、VCI をロードする.
    /// また、VCI プレハブおよび通常のアセットファイルを保存する責務を持つ.
    /// </summary>
    internal sealed class VciEditorImporterSecondPass : IDisposable
    {
        private readonly VciData _data;
        private readonly UnityPath _prefabPath;
        private readonly VCIImporter _importer;

        public VciEditorImporterSecondPass(VciData data, UnityPath prefabPath, VciEditorImporterExternalAssetPathList vciAssetPathList)
        {
            _data = data;
            _prefabPath = prefabPath;

            // NOTE: VCI 固有の Asset の Path を生成する.
            var map = new Dictionary<SubAssetKey, Object>();
            foreach (var texturePath in vciAssetPathList.TexturePaths)
            {
                var texture = texturePath.LoadAsset<Texture>();
                map.Add(new SubAssetKey(texture), texture);
            }
            foreach (var audioClipPath in vciAssetPathList.AudioClipPaths)
            {
                var clip = audioClipPath.LoadAsset<AudioClip>();
                map.Add(new SubAssetKey(typeof(AudioClip), clip.name), clip);
            }

            // NOTE: FirstPass で Extract したアセットを入力にあたえてインポータを作る.
            _importer = new VCIImporter(_data, map);

            // NOTE: Extract したテクスチャに対して Editor の TextureImporter 設定を行う.
            foreach (var textureInfo in _importer.TextureDescriptorGenerator.Get().GetEnumerable())
            {
                TextureImporterConfigurator.Configure(textureInfo, _importer.TextureFactory.ExternalTextures);
            }
        }

        public void Dispose()
        {
            _importer?.Dispose();
        }

        public void Load()
        {
            _importer.Load();
            var vciInstance = _importer.RuntimeVciInstance;
            // NOTE: Editor 用の特殊な Import 処理が必要なコンポーネントの処理を呼び出す.
            SetupScriptsAfterEditorDelayCall();
            SetupEffekseerAfterEditorDelayCall();
            SetupColliderMeshAfterEditorDelayCall();
            vciInstance.ShowMeshes();
            // NOTE: ロード中に物理演算が働かないように Disabled されているため、Enable する.
            vciInstance.EnablePhysicalBehaviour(true);

            // NOTE: SubAsset を保存する.
            var assetPathList = new List<UnityPath> { _prefabPath };
            vciInstance.TransferOwnership((key, o) =>
            {
                var path = SaveSubAssetAsAsset(o, _prefabPath);
                if (path.HasValue)
                {
                    assetPathList.Add(path.Value);
                }
            });
            var root = vciInstance.Root;

            // NOTE: RuntimeGltfInstance Component は prefab には不要.
            UnityObjectDestoyer.DestroyRuntimeOrEditor(vciInstance.GltfModel);

            // NOTE: VCI そのものの Prefab を作成するか、アップデートする.
            if (_prefabPath.IsFileExists)
            {
                Debug.LogFormat("replace prefab: {0}", _prefabPath);
                var prefab = _prefabPath.LoadAsset<GameObject>();
                PrefabUtility.SaveAsPrefabAssetAndConnect(root, _prefabPath.Value, InteractionMode.AutomatedAction);
            }
            else
            {
                Debug.LogFormat("create prefab: {0}", _prefabPath);
                PrefabUtility.SaveAsPrefabAssetAndConnect(root, _prefabPath.Value, InteractionMode.AutomatedAction);
            }

            // NOTE: シーン上のインスタンスを削除
            UnityObjectDestoyer.DestroyRuntimeOrEditor(root);

            foreach (var x in assetPathList)
            {
                x.ImportAsset();
            }
        }

        private void SetupEffekseerAfterEditorDelayCall()
        {
            new EditorEffekseerImporter(_data, _importer, _prefabPath).SetupAfterEditorDelayCall();
        }

        private void SetupScriptsAfterEditorDelayCall()
        {
            new EditorScriptImporter(_data, _importer, _prefabPath).SetupAfterEditorDelayCall();
        }

        private void SetupColliderMeshAfterEditorDelayCall()
        {
            new EditorPhysicsColliderImporter(_data, _importer, _prefabPath).SetupAfterEditorDelayCall();
        }

        /// <summary>
        /// 与えられた prefab の SubAsset を、通常の Asset として保存する.
        /// </summary>
        private static UnityPath? SaveSubAssetAsAsset(UnityEngine.Object o, UnityPath prefabPath)
        {
            if (!string.IsNullOrEmpty(AssetDatabase.GetAssetPath(o)))
            {
                Debug.LogWarning($"{o} is already extracted. This will be skipped saving as file.");
                return default;
            }

            var assetPath = GetAssetPath(prefabPath, o);
            if (assetPath.IsNull) return default;

            assetPath.Parent.EnsureFolder();
            assetPath.CreateAsset(o);
            return assetPath;
        }

        private static UnityPath GetAssetPath(UnityPath prefabPath, UnityEngine.Object o)
        {
            switch (o)
            {
                // NOTE: GLTF assets
                case Material _:
                    {
                        var materialDir = prefabPath.GetAssetFolder(".Materials");
                        var materialPath = materialDir.Child(o.name.EscapeFilePath() + ".asset");
                        return materialPath;
                    }
                // NOTE: GLTF assets
                case Mesh _:
                    {
                        var meshDir = prefabPath.GetAssetFolder(".Meshes");
                        var meshPath = meshDir.Child(o.name.EscapeFilePath() + ".asset");
                        return meshPath;
                    }
                // NOTE: GLTF assets
                case AnimationClip _:
                    {
                        var animDir = prefabPath.GetAssetFolder(".Animations");
                        var animPath = animDir.Child(o.name.EscapeFilePath() + ".asset");
                        return animPath;
                    }
                // NOTE: VCI assets
                case PhysicMaterial _:
                    {
                        var materialDir = prefabPath.GetAssetFolder(".PhysicMaterials");
                        var materialPath = materialDir.Child(o.name.EscapeFilePath() + ".asset");
                        return materialPath;
                    }
                default:
                    return default;
            }
        }
    }
}
