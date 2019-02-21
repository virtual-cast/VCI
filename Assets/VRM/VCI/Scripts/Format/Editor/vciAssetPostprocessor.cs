#pragma warning disable
using System;
using System.IO;
using System.Linq;
using UniGLTF;
using UnityEditor;
using DepthFirstScheduler;


namespace VCI
{
#if !VRM_STOP_ASSETPOSTPROCESSOR
    public class vciAssetPostprocessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string path in importedAssets)
            {
                var ext = Path.GetExtension(path).ToLower();
                if (ext == VCIVersion.EXTENSION)
                {
                    ImportVci(UnityPath.FromUnityPath(path));
                }
            }
        }

        static void ImportVci(UnityPath path)
        {
            if (!path.IsUnderAssetsFolder)
            {
                throw new Exception();
            }
            var importer = new VCI.VCIImporter();
            importer.ParseGlb(File.ReadAllBytes(path.FullPath));

            var prefabPath = path.Parent.Child(path.FileNameWithoutExtension + ".prefab");

            // save texture assets !
            importer.ExtranctImages(prefabPath);
            importer.ExtractAudio(prefabPath);

            EditorApplication.delayCall += () =>
            {
                //
                // after textures imported
                //
                importer.Load();
                importer.SetupCorutine().CoroutinetoEnd();
                importer.SetupPhysics();
                importer.SaveAsAsset(prefabPath);
                importer.Destroy(false);
            };
        }
    }
#endif
}
