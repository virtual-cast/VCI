using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UniGLTF;

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

            if (!Validate()) return;

            try
            {
                // save dialog
                var root = Selection.activeObject as GameObject;

                var path = VCI.FileDialogForWindows.SaveDialog("Save " + VCIVersion.EXTENSION,
                    root.name + VCIVersion.EXTENSION);

                //var path = EditorUtility.SaveFilePanel(
                //    "Save " + VCIVersion.EXTENSION,
                //    null,
                //    root.name + VCIVersion.EXTENSION,
                //    VCIVersion.EXTENSION.Substring(1));
                if (string.IsNullOrEmpty(path)) return;

                var bytes = ExportVci(root);
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
                exporter.Export(default);
            }

            return gltf.ToGlbBytes();
        }

        public static bool Validate()
        {
            try
            {
                var selectedGameObjects = Selection.gameObjects;
                if (selectedGameObjects.Length == 0)
                {
                    var errorText = VCIConfig.GetText($"error{(int) ValidationErrorType.GameObjectNotSelected}");
                    throw new VCIValidatorException(ValidationErrorType.GameObjectNotSelected, errorText);
                }

                if (2 <= selectedGameObjects.Length)
                {
                    var errorText = VCIConfig.GetText($"error{(int) ValidationErrorType.MultipleSelection}");
                    throw new VCIValidatorException(ValidationErrorType.MultipleSelection, errorText);
                }

                var vciObject = selectedGameObjects[0].GetComponent<VCIObject>();
                if (vciObject == null)
                {
                    var errorText = VCIConfig.GetText($"error{(int) ValidationErrorType.VCIObjectNotAttached}");
                    throw new VCIValidatorException(ValidationErrorType.VCIObjectNotAttached, errorText);
                }

                VCIValidator.ValidateVCIObject(vciObject);
            }
            catch (VCIValidatorException e)
            {
                var title =  $"Error{(int)e.ErrorType}";

                var text = e.Message;

                text = text.Replace("\\n", Environment.NewLine);

                if(e.ErrorType == ValidationErrorType.InvalidCharacter)
                    EditorGUILayout.HelpBox(e.Message, MessageType.Warning);

                EditorUtility.DisplayDialog(title, text, "OK");
                GUIUtility.ExitGUI();
                return false;
            }
            return true;
        }
    }
}