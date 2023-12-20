using System;
using System.IO;
using UniGLTF;
using UnityEditor;
using UnityEngine;

namespace VCI
{
    public static class VCIObjectExporterMenu
    {
        private const string CONVERT_OBJECT_KEY = Constants.MenuPrefix + "/Export VCI";

        [MenuItem(CONVERT_OBJECT_KEY, priority = 1)]
        public static void ExportObject()
        {
            if (Application.isPlaying)
            {
                Debug.LogError("this function works only on runtime.");
            }

            var rootGameObject = default(GameObject);
            try
            {
                rootGameObject = GameObjectSelectionService.GetSingleSelectedObject();
                VciValidator.ValidateVciRequirements(rootGameObject);
            }
            catch (VciValidatorException e)
            {
                VciValidatorExceptionUtils.ShowErrorDialog(e);
                VciValidatorExceptionUtils.SelectObject(e);
                GUIUtility.ExitGUI();
                return;
            }

            try
            {
                // save dialog
                var path = EditorUtility.SaveFilePanel(
                    "Save VCI file",
                    null,
                    rootGameObject.name + Constants.Extension,
                    Constants.Extension.Substring(1)
                );
                if (string.IsNullOrEmpty(path)) return;

                var bytes = VciEditorExporter.Export(rootGameObject);
                File.WriteAllBytes(path, bytes);

                if (path.StartsWithUnityAssetPath())
                {
                    AssetDatabase.ImportAsset(path.ToUnityRelativePath());
                    AssetDatabase.Refresh();
                }

                // Show success dialog
                var openExplorer = !EditorUtility.DisplayDialog("エクスポート成功", $"VCIファイルの出力に成功しました。\nPath: {path}", "閉じる", "ファイルを表示");
                if (openExplorer)
                {
                    // Show the file in the explorer.
                    if (Application.platform == RuntimePlatform.WindowsEditor)
                    {
                        System.Diagnostics.Process.Start("explorer.exe", " /e,/select," + path.Replace("/", "\\"));
                    }
                    else if(Application.platform == RuntimePlatform.OSXEditor)
                    {
                        System.Diagnostics.Process.Start("open", "-R " + path);
                    }
                }
            }
            catch (Exception ex)
            {
                EditorUtility.DisplayDialog("エクスポート失敗", $"VCIファイルの出力に失敗しました。\n\nエラー原因:\n{ex.Message}\n\n詳細:\n{ex}", "閉じる");
                throw;
            }
        }
    }
}
