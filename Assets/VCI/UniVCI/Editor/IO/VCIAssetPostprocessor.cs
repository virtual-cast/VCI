using System;
using System.IO;
using UnityEditor;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public sealed class VciAssetPostprocessor : AssetPostprocessor
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

            var editorImporter = new VciEditorImporter(data, prefabPath);
            editorImporter.Load();
        }
    }
}
