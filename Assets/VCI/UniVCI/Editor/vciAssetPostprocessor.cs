using System;
using System.IO;
using UnityEditor;
using DepthFirstScheduler;
using UniGLTF;

namespace VCI
{
#if !VRM_STOP_ASSETPOSTPROCESSOR
    public sealed class VCIAssetPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (var path in importedAssets)
            {
                var ext = Path.GetExtension(path).ToLower();
                if (ext == VCIVersion.EXTENSION) ImportVci(UnityPath.FromUnityPath(path));
            }
        }

        private static void ImportVci(UnityPath path)
        {
            if (!path.IsUnderAssetsFolder) throw new Exception();
            var importer = new VCIImporter();
            importer.ParseGlb(File.ReadAllBytes(path.FullPath));

            var prefabPath = path.Parent.Child(path.FileNameWithoutExtension + ".prefab");

            // save texture assets !
            importer.ExtractImages(prefabPath);
            importer.ExtractAudio(prefabPath);
            importer.ExtractEffekseer(prefabPath);
            importer.ExtractAnimation(prefabPath);

            EditorApplication.delayCall += () =>
            {
                //
                // after textures imported
                //
                importer.Load();
                importer.SetupCoroutine().CoroutineToEnd();
                importer.EnablePhysicalBehaviour(true);
                importer.ExportScriptFile(prefabPath);
                importer.SaveAsAsset(prefabPath);
                importer.EditorDestroyRoot();
            };
        }
    }
#endif
}
