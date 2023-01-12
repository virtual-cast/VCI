using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace VCI
{
    public static class LuaCreator
    {
        [MenuItem(itemName: "Assets/Create/VCI/Lua Script")]
        private static void CreateLua()
        {
            // NOTE: Project ビューで選択されているもののうち、最初のフォルダを取得してそこにファイルを作る
            // 該当のフォルダが存在しなかった場合は Assets 直下に作成する
            var selectedFolder = Selection
                .GetFiltered<DefaultAsset>(SelectionMode.Assets)
                .Select(AssetDatabase.GetAssetPath)
                .Where(AssetDatabase.IsValidFolder)
                .FirstOrDefault();
            var folder = selectedFolder ?? "Assets";
            var path = folder + "/main.lua";
            // NOTE: 重複した場合に備えて Unity しぐさの命名をする
            var count = 1;
            while (File.Exists(path))
            {
                path = folder + $"/main ({count++}).lua";
            }

            // NOTE: AssetDatabase.Create では lua ファイルが作れない
            File.WriteAllText(path, string.Empty);
            AssetDatabase.ImportAsset(path);
            Debug.Log($"[VCI] Created lua file: {path}");

            // Project ビューで選択状態にしてハイライトさせる
            var createdAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            Selection.activeObject = createdAsset;
            EditorGUIUtility.PingObject(createdAsset);
        }
    }
}
