using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using VCIGLTF;

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

                // export
                var gltf = new glTF();
                using (var exporter = new VCIExporter(gltf))
                {
                    exporter.Prepare(root);
                    exporter.Export();
                }

                var bytes = gltf.ToGlbBytes();
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
            finally
            {
                GUIUtility.ExitGUI();
            }
#else
            Debug.LogError("this function works only on Windows");
#endif
        }

        public static bool Validate()
        {
            // Validation
            try
            {
                var selectedGameObjects = Selection.gameObjects;
                if (selectedGameObjects.Length == 0)
                    throw new VCIValidatorException(ValidationErrorType.GameObjectNotSelected);

                if (2 <= selectedGameObjects.Length)
                    throw new VCIValidatorException(ValidationErrorType.MultipleSelection);

                var vciObject = selectedGameObjects[0].GetComponent<VCIObject>();
                if (vciObject == null)
                    throw new VCIValidatorException(ValidationErrorType.VCIObjectNotAttached);

                VCIValidator.ValidateVCIObject(vciObject);
            }
            catch (VCIValidatorException e)
            {
                var title =  $"Error{(int)e.ErrorType}";

                var text = "";

                if (string.IsNullOrEmpty(e.Message))
                {
                    text = VCIConfig.GetText($"error{(int)e.ErrorType}");
                }
                else
                {
                    text = e.Message;
                }

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