#pragma warning disable
using System.IO;
using UniGLTF;
using UnityEditor;
using UnityEngine;


namespace VCI
{
    public static class VCIObjectExporterMenu
    {
        #region Export NonHumanoid
        const string CONVERT_OBJECT_KEY = VCIVersion.MENU + "/Export VCI";

        [MenuItem(CONVERT_OBJECT_KEY)]
        private static void ExportObjectFromMenu()
        {
            var errorMessage = "";
            if (!Validate(out errorMessage))
            {
                Debug.LogAssertion(errorMessage);
                return;
            }
            
            // save dialog
            var root = Selection.activeObject as GameObject;
            var path = EditorUtility.SaveFilePanel(
                    "Save " + VCIVersion.EXTENSION,
                    null,
                    root.name + VCIVersion.EXTENSION,
                    VCIVersion.EXTENSION.Substring(1));
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            // export
            var gltf = VCI.VCIExporter.Export(root);
            var bytes = gltf.ToGlbBytes(true);
            File.WriteAllBytes(path, bytes);

            if (path.StartsWithUnityAssetPath())
            {
                AssetDatabase.ImportAsset(path.ToUnityRelativePath());
                AssetDatabase.Refresh();
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

            var isValid = VCIMetaValidator.Validate(vciObject, out errorMessage);
            return isValid;
        }
        #endregion
    }
}
