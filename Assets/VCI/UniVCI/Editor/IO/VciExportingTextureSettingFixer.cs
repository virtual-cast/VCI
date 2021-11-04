using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// エクスポート対象のテクスチャの TextureImporter 設定を変更し、エクスポートに適切なものとする.
    /// </summary>
    public static class VciExportingTextureSettingFixer
    {
        public static void Fix(GameObject vciRoot)
        {
            if (vciRoot == null) return;

            var vciRootPath = AssetDatabase.GetAssetPath(vciRoot);
            if (string.IsNullOrEmpty(vciRootPath))
            {
                // NOTE: vciRoot が Scene 上の Asset だった場合
                var temporaryPrefabPath = $"Assets/{Guid.NewGuid()}.prefab";
                // NOTE: いったん Prefab Asset 化し、Dependencies を見つけられるようにする.
                PrefabUtility.SaveAsPrefabAsset(vciRoot, temporaryPrefabPath);
                Fix(AssetDatabase.GetDependencies(temporaryPrefabPath, recursive: true));
                AssetDatabase.DeleteAsset(temporaryPrefabPath);
            }
            else
            {
                // NOTE: vciRoot が Project 上の Asset だった場合
                Fix(AssetDatabase.GetDependencies(vciRootPath, recursive: true));
            }
        }

        private static void Fix(IEnumerable<string> dependencies)
        {
            foreach (var path in dependencies)
            {
                var textureAsset = AssetDatabase.LoadAssetAtPath<Texture>(path);
                if (textureAsset == null) continue;

                var textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
                if (textureImporter == null) continue;

                // NOTE: すでに Fix 済みであれば、なにもしない.
                if (textureImporter.textureCompression == TextureImporterCompression.Uncompressed) continue;

                // NOTE: エクスポート時の品質劣化を防ぐため、Uncompressed とする.
                textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
                textureImporter.SaveAndReimport();
            }
        }
    }
}
