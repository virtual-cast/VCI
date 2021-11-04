using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UniGLTF;
using UniJSON;
using UnityEditor;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public class VCIEditorImporterContext
    {
        private readonly VciData _data;
        private readonly VCIImporter _context;
        private readonly UnityPath _prefabPath;
        List<UnityPath> m_paths = new List<UnityPath>();

        public ITextureDescriptorGenerator TextureDescriptorGenerator => _context.TextureDescriptorGenerator;

        public VCIEditorImporterContext(VciData data, VCIImporter context, UnityPath prefabPath)
        {
            _data = data;
            _context = context;
            _prefabPath = prefabPath;
        }

        /// <summary>
        /// Extract images from glb or gltf out of Assets folder.
        /// </summary>
        public void ConvertAndExtractAssets(Action<VciEditorImporterResourcePaths> onExtractionCompleted)
        {
            //
            // convert images(metallic roughness, occlusion map)
            //
            var task = _context.LoadAsync(new ImmediateCaller());
            if (!task.IsCompleted)
            {
                throw new Exception();
            }
            if (task.IsCanceled || task.IsFaulted)
            {
                throw new Exception();
            }
            var result = task.Result;
            var loadedTextures = result.RuntimeResources
                .Where(x => x.Item1.Type == typeof(Texture))
                .ToDictionary(x => x.Item1, x => x.Item2 as Texture);

            // Extract assets
            var audioClipPaths = EditorAudioImporter.ExtractAssetFiles(_data, _prefabPath);
            ExtractEffekseerAsEditorAssets();
            ExtractScriptAsEditorAssets();

            // Finally, extract textures.
            var dirName = $"{_prefabPath.FileNameWithoutExtension}.Textures";
            TextureExtractor.ExtractTextures(
                _context.Data,
                _prefabPath.Parent.Child(dirName),
                _context.TextureDescriptorGenerator,
                loadedTextures,
                (_x, _y) => { },
                (textureAssetPaths) =>
                {
                    var paths = new VciEditorImporterResourcePaths();
                    paths.TexturePaths.AddRange(textureAssetPaths);
                    paths.AudioClipPaths.AddRange(audioClipPaths);
                    onExtractionCompleted(paths);
                }
            );

            result.Dispose();
        }

        private void SaveAsAsset(SubAssetKey _, UnityEngine.Object o)
        {
            if (!string.IsNullOrEmpty(AssetDatabase.GetAssetPath(o)))
            {
#if VRM_DEVELOP
                // 来ない？
                Debug.LogWarning($"{o} already exists. skip write");
#endif
                return;
            }

            var assetPath = GetAssetPath(_prefabPath, o);
            if (!assetPath.IsNull)
            {
                // アセットとして書き込む
                assetPath.Parent.EnsureFolder();
                assetPath.CreateAsset(o);
                m_paths.Add(assetPath);
            }
        }

        private UnityPath GetAssetPath(UnityPath prefabPath, UnityEngine.Object o)
        {
            // GLTF assets
            if (o is Material)
            {
                var materialDir = prefabPath.GetAssetFolder(".Materials");
                var materialPath = materialDir.Child(o.name.EscapeFilePath() + ".asset");
                return materialPath;
            }
            else if (o is Mesh)
            {
                var meshDir = prefabPath.GetAssetFolder(".Meshes");
                var meshPath = meshDir.Child(o.name.EscapeFilePath() + ".asset");
                return meshPath;
            }
            else if (o is AnimationClip)
            {
                var animDir = prefabPath.GetAssetFolder(".Animations");
                var animPath = animDir.Child(o.name.EscapeFilePath() + ".asset");
                return animPath;
            }
            else
            {
                return default(UnityPath);
            }
        }

        public void SaveAsAsset(UniGLTF.RuntimeGltfInstance loaded)
        {
            SetupScriptsAfterEditorDelayCall();
            SetupEffekseerAfterEditorDelayCall();

            loaded.ShowMeshes();

            //
            // save sub assets
            //
            m_paths.Clear();
            m_paths.Add(_prefabPath);
            loaded.TransferOwnership(SaveAsAsset);
            var root = loaded.Root;

            // NOTE: RuntimeGltfInstance は prefab には不要.
            UnityObjectDestoyer.DestroyRuntimeOrEditor(loaded);

            // Create or update Main Asset
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

            // destroy GameObject on scene
            UnityObjectDestoyer.DestroyRuntimeOrEditor(root);


            foreach (var x in m_paths)
            {
                x.ImportAsset();
            }
        }

        private void ExtractEffekseerAsEditorAssets()
        {
            new EditorEffekseerImporter(_data, _context, _prefabPath).ExtractAssetFiles();
        }

        private void SetupEffekseerAfterEditorDelayCall()
        {
            new EditorEffekseerImporter(_data, _context, _prefabPath).SetupAfterEditorDelayCall();
        }

        private void ExtractScriptAsEditorAssets()
        {
            new EditorScriptImporter(_data, _context, _prefabPath).ExtractAssetFiles();
        }

        private void SetupScriptsAfterEditorDelayCall()
        {
            new EditorScriptImporter(_data, _context, _prefabPath).SetupAfterEditorDelayCall();
        }
    }
}
