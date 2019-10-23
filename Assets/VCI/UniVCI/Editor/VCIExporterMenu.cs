using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VCIGLTF;

namespace VCI
{
    public static class VCIObjectExporterMenu
    {
        #region Export NonHumanoid

        private const string CONVERT_OBJECT_KEY = VCIVersion.MENU + "/Export VCI";

        [MenuItem(CONVERT_OBJECT_KEY)]
        public static void ExportObject()
        {
            EditorApplication.isPlaying = false;
            try
            {
                var errorMessage = "";
                if (!Validate(out errorMessage))
                {
                    Debug.LogAssertion(errorMessage);
                    EditorUtility.DisplayDialog("Error", errorMessage, "OK");
                    return;
                }

                // save dialog
                var root = Selection.activeObject as GameObject;

                var path = VCI.FileDialogForWindows.SaveDialog("Save " + VCIVersion.EXTENSION, root.name + VCIVersion.EXTENSION);
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
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    System.Diagnostics.Process.Start("explorer.exe", " /e,/select," + path.Replace("/", "\\"));
                }
            }
            catch
            {
                throw;
            }
        }

        private static bool Validate(out string errorMessage)
        {
            var selectedGameObjects = Selection.gameObjects;
            if (selectedGameObjects.Length == 0)
            {
                errorMessage = "VCIObjectがアタッチされたGameObjectを選択して下さい。";
                return false;
            }

            if (2 <= selectedGameObjects.Length)
            {
                errorMessage = "VCIObjectがアタッチされたGameObjectを1つ選択して下さい。";
                return false;
            }

            var vciObject = selectedGameObjects[0].GetComponent<VCIObject>();
            if (vciObject == null)
            {
                errorMessage = "VCIObjectがアタッチされたGameObjectを選択して下さい。";
                return false;
            }

            // Script
            if(vciObject.Scripts.Any())
            {
                if (vciObject.Scripts[0].name != "main")
                {
                    errorMessage = "The first script must be named \"main\".";
                    return false;
                }

                var empties = vciObject.Scripts.Where(x => string.IsNullOrEmpty(x.name));
                if (empties.Any())
                {
                    errorMessage = "Some have no script name.";
                    return false;
                }

                var duplicates = vciObject.Scripts.GroupBy(script => script.name)
                    .Where(name => name.Count() > 1)
                    .Select(group => group.Key).ToList();
                if (duplicates.Any())
                {
                    errorMessage = "Duplicate script name.";
                    return false;
                }

                var invalidChars = Path.GetInvalidFileNameChars().Concat(new []{ '.' }).ToArray();
                foreach (var script in vciObject.Scripts)
                {
                    if (script.name.IndexOfAny(invalidChars) >= 0)
                    {
                        EditorGUILayout.HelpBox("Contains characters that can not be used as scriptName. " + script.name, MessageType.Warning);
                    }
                };
            }


            var isValid = VCIMetaValidator.Validate(vciObject, out errorMessage);
            return isValid;
        }

        #endregion
    }
}