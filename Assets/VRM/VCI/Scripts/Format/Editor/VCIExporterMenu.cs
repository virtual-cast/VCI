#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UniGLTF;
using UnityEditor;
using UnityEngine;


namespace VCI
{
    public static class VCIObjectExporterMenu
    {
        #region Export NonHumanoid
        const string CONVERT_OBJECT_KEY = VCIVersion.MENU + "/Export VCI";
        const int DescriptionLimit = 2048;

        [MenuItem(CONVERT_OBJECT_KEY, true)]
        private static bool ExportObjectValidate()
        {
            var root = Selection.activeObject as GameObject;
            if (root == null)
            {
                return false;
            }

            var vciObject = root.GetComponent<VCIObject>();
            if (vciObject == null)
            {
                return false;
            }

            return true;
        }

        [MenuItem(CONVERT_OBJECT_KEY, false)]
        private static void ExportObjectFromMenu()
        {
            var root = Selection.activeObject as GameObject;

            var vciObject = root.GetComponent<VCIObject>();

            var errorMessage = "";
            if (!Validate(root, out errorMessage))
            {
                Debug.LogWarning(errorMessage);
                return;
            }
            
            // save dialog
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

        private static bool Validate(GameObject root, out string errorMessage)
        {
            var vciObject = root.GetComponent<VCIObject>();

            var errorMessages = new List<string>();
            if (string.IsNullOrEmpty(vciObject.Meta.author))
            {
                errorMessages.Add("Authorを入力して下さい。");
            }

            if (DescriptionLimit < vciObject.Meta.description.Length)
            {
                errorMessages.Add($"Descriptionは{DescriptionLimit}文字以内にして下さい。");
            }

            if (errorMessages.Any())
            {
                errorMessage = string.Join(Environment.NewLine, errorMessages);
                return false;
            }

            errorMessage = "";
            return true;
        }
        #endregion
    }
}
