using System;
using System.IO;
using UniGLTF;
using UnityEditor;
using UnityEngine;

namespace VCI
{
    public static class VCIObjectExporterMenu
    {
        private const string CONVERT_OBJECT_KEY = VCIVersion.MENU + "/Export VCI";

        [MenuItem(CONVERT_OBJECT_KEY)]
        public static void ExportObject()
        {
#if UNITY_STANDALONE_WIN && UNITY_EDITOR
            EditorApplication.isPlaying = false;

            var rootGameObject = default(GameObject);
            try
            {
                rootGameObject = GameObjectSelectionService.GetSingleSelectedObject();
                VCIValidator.ValidateVCIRequirements(rootGameObject);
            }
            catch(VCIValidatorException e)
            {
                VCIValidationErrorDialog.ShowErrorDialog(e);
                GUIUtility.ExitGUI();
                return;
            }

            try
            {
                // save dialog
                var path = VCI.FileDialogForWindows.SaveDialog("Save " + VCIVersion.EXTENSION,
                    rootGameObject.name + VCIVersion.EXTENSION);

                //var path = EditorUtility.SaveFilePanel(
                //    "Save " + VCIVersion.EXTENSION,
                //    null,
                //    root.name + VCIVersion.EXTENSION,
                //    VCIVersion.EXTENSION.Substring(1));
                if (string.IsNullOrEmpty(path)) return;

                var bytes = ExportVci(rootGameObject);
                File.WriteAllBytes(path, bytes);

                if (path.StartsWithUnityAssetPath())
                {
                    AssetDatabase.ImportAsset(path.ToUnityRelativePath());
                    AssetDatabase.Refresh();
                }

                // Show the file in the explorer.
                if (VCIConfig.IsOpenDirectoryAfterExport && Application.platform == RuntimePlatform.WindowsEditor)
                {
                    System.Diagnostics.Process.Start("explorer.exe", " /e,/select," + path.Replace("/", "\\"));
                }
            }
            catch(Exception ex)
            {
                Debug.LogError(ex);
            }
            finally
            {
                // TODO: ツールバーからExport VCIを行うとExitGUIExceptionが飛ぶため、いったん握りつぶす
                try
                {
                    GUIUtility.ExitGUI();
                }
                catch
                {
                }
            }
#else
            Debug.LogError("this function works only on Windows");
#endif
        }

        public static byte[] ExportVci(GameObject root)
        {
            // export
            var gltf = new glTF();
            using (var exporter = new VCIExporter(gltf))
            {
                exporter.Prepare(root);
                exporter.Export(default, VRMShaders.AssetTextureUtil.UseAsset);
            }

            return gltf.ToGlbBytes();
        }
    }
}