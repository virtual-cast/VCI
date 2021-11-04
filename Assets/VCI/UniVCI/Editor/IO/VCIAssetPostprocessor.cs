using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UniGLTF;
using UnityEngine;
using VRMShaders;
using Object = UnityEngine.Object;

namespace VCI
{
#if !VRM_STOP_ASSETPOSTPROCESSOR
    public sealed class VCIAssetPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string path in importedAssets)
            {
                if (UnityPath.FromUnityPath(path).IsStreamingAsset)
                {
                    Debug.LogFormat("Skip StreamingAssets: {0}", path);
                    continue;
                }

                var ext = Path.GetExtension(path).ToLower();
                if (ext == Constants.Extension)
                {
                    ImportVci(UnityPath.FromUnityPath(path));
                }
            }
        }

        private static void ImportVci(UnityPath path)
        {
            if (!path.IsUnderAssetsFolder)
            {
                throw new Exception();
            }

            var data = new VciFileParser(path.FullPath).Parse();

            var prefabPath = path.Parent.Child(path.FileNameWithoutExtension + ".prefab");

            Action<VciEditorImporterResourcePaths> onCompleted = (paths) =>
            {
                var map = new Dictionary<SubAssetKey, Object>();
                foreach (var texturePath in paths.TexturePaths)
                {
                    var texture = texturePath.LoadAsset<Texture>();
                    map.Add(new SubAssetKey(texture), texture);
                }
                foreach (var audioClipPath in paths.AudioClipPaths)
                {
                    var clip = audioClipPath.LoadAsset<AudioClip>();
                    map.Add(new SubAssetKey(typeof(AudioClip), clip.name), clip);
                }

                using (var context = new VCIImporter(data, map))
                {
                    var editor = new VCIEditorImporterContext(data, context, prefabPath);
                    foreach (var textureInfo in context.TextureDescriptorGenerator.Get().GetEnumerable())
                    {
                        VRMShaders.TextureImporterConfigurator.Configure(textureInfo, context.TextureFactory.ExternalTextures);
                    }

                    // after all
                    var loaded = context.Load();
                    // NOTE: ロード中に物理演算が働かないように Disabled されているため、Enable してから保存する.
                    context.RuntimeVciInstance.EnablePhysicalBehaviour(true);
                    editor.SaveAsAsset(loaded);
                }
            };

            // extract texture images
            using (var context = new VCIImporter(data))
            {
                var editor = new VCIEditorImporterContext(data, context, prefabPath);
                editor.ConvertAndExtractAssets(onCompleted);
            }
        }
    }
#endif
}
