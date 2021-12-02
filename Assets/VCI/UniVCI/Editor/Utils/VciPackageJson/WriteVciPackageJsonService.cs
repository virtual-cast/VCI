using System.IO;
using UniJSON;
using UnityEditor;
using UnityEngine;

namespace VCI
{
    public static class WriteVciPackageJsonService
    {
        // package.json を配置するパス
        // * Application.dataPath からの相対パス
        private const string VciPackageJsonPath = "VCI\\package.json";
        // package.jsonに書き込む内容が保存されているScriptableObjectの名前
        private const string VciPackageSettingsFileName = "VciPackageSettings";

        public static void WritePackageJsonFile()
        {
            var vciPackageSettings = LoadCurrentPackageSettings();
            var jsonText = GeneratePackageJsonText(vciPackageSettings);

            var writePath =
                Path.Combine(Application.dataPath, VciPackageJsonPath)
                    .Replace("/", "\\"); // to windows directory separator

            File.WriteAllText(writePath, jsonText);
            Debug.Log($"Wrote package.json: {writePath}");

            AssetDatabase.Refresh();
        }

        // VCI/UniVCI/Resourcesに置いてあるScriptableObjectから、package.jsonに書き込む内容をロードする
        private static VciPackageSettings LoadCurrentPackageSettings()
        {
            return Resources.Load<VciPackageSettings>(VciPackageSettingsFileName);
        }

        private static string GeneratePackageJsonText(VciPackageSettings vciPackageSettings)
        {
            // jsonFormatterを使って package.json の中身を構築する
            var jsonFormatter = new JsonFormatter(2);

            // {
            jsonFormatter.BeginMap();
            {
                // "name": パッケージ名
                jsonFormatter.Key("name");
                jsonFormatter.Value(vciPackageSettings.OfficialPackageName.GetValue());

                // "version": パッケージのバージョン
                jsonFormatter.Key("version");
                jsonFormatter.Value(vciPackageSettings.PackageVersion.GetValue());

                // "displayName": ユーザーから見えるパッケージ名
                jsonFormatter.Key("displayName");
                jsonFormatter.Value(vciPackageSettings.DisplayName);

                // "description": パッケージの説明
                jsonFormatter.Key("description");
                jsonFormatter.Value(vciPackageSettings.Description);

                // "unity": 互換性のある最小 unity バージョン
                jsonFormatter.Key("unity");
                jsonFormatter.Value(vciPackageSettings.UnityVersion);

                // "keywords": パッケージ検索用のキーワード
                jsonFormatter.Key("keywords");
                // [
                jsonFormatter.BeginList();
                foreach (var keyword in vciPackageSettings.Keywords)
                {
                    jsonFormatter.Value(keyword);
                }
                // ]
                jsonFormatter.EndList();

                // author: パッケージの作者
                jsonFormatter.Key("author");
                // {
                jsonFormatter.BeginMap();
                //   name: 作者名(required)
                jsonFormatter.Key("name");
                jsonFormatter.Value(vciPackageSettings.Author.Name);
                //   email: 作者email(optional)
                if (!string.IsNullOrEmpty(vciPackageSettings.Author.Email))
                {
                    jsonFormatter.Key("email");
                    jsonFormatter.Value(vciPackageSettings.Author.Email);
                }
                //   url: 作者ページのurl(optional)
                if (!string.IsNullOrEmpty(vciPackageSettings.Author.Url))
                {
                    jsonFormatter.Key("url");
                    jsonFormatter.Value(vciPackageSettings.Author.Url);
                }
                // }
                jsonFormatter.EndMap();

                // dependencies: 依存パッケージ
                jsonFormatter.Key("dependencies");
                // {
                jsonFormatter.BeginMap();
                foreach (var dependency in vciPackageSettings.Dependencies)
                {
                    // パッケージ名: パッケージのバージョン
                    jsonFormatter.Key(dependency.OfficialPackageName.GetValue());
                    jsonFormatter.Value(dependency.PackageVersion.GetValue());
                }
                // }
                jsonFormatter.EndMap();
            }
            // }
            jsonFormatter.EndMap();

            return jsonFormatter.ToString();
        }
    }
}
