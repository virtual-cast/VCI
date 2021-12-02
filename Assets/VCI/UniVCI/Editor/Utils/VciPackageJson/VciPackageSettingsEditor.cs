using UnityEditor;
using UnityEngine;

namespace VCI
{
    [CustomEditor(typeof(VciPackageSettings))]
    public sealed class VciPackageSettingsEditor : Editor
    {
        private VciPackageSettings _vciPackageSettings;
        private bool _showUnityVersionSection;
        private bool _isDescriptionSectionExpanded;

        private void OnEnable()
        {
            _vciPackageSettings = target as VciPackageSettings;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawOfficialPackageName(serializedObject.FindProperty("_officialPackageName"), _vciPackageSettings.OfficialPackageName);
            DrawVciPackageVersion();
            DrawPackageDisplayName();
            DrawPackageDescription();
            DrawUnityVersion();
            DrawKeywords();
            DrawAuthor();
            DrawDependencies();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawOfficialPackageName(SerializedProperty officialPackageNameProp, OfficialPackageName packageName)
        {
            var domainNameExtensionProp = officialPackageNameProp.FindPropertyRelative("_domainNameExtension");
            var companyNameExtensionProp = officialPackageNameProp.FindPropertyRelative("_companyName");
            var packageNamespaceProp = officialPackageNameProp.FindPropertyRelative("_packageNamespace");

            using (new EditorGUILayout.HorizontalScope())
            {
                // Foldout付きのラベルを作る
                EditorGUILayout.PropertyField(officialPackageNameProp, false);

                // パッケージ名を表示する
                // * read onlyにするためにdisableする
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.TextField(packageName.GetValue());
                }
            }

            // Foldoutが展開されている場合のみ表示
            if (officialPackageNameProp.isExpanded)
            {
                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    EditorGUILayout.PropertyField(domainNameExtensionProp);
                    EditorGUILayout.PropertyField(companyNameExtensionProp);
                    EditorGUILayout.PropertyField(packageNamespaceProp);
                }
            }
        }

        private void DrawVciPackageVersion()
        {
            var packageVersionProp = serializedObject.FindProperty("_packageVersion");
            var majorProp = packageVersionProp.FindPropertyRelative("_major");
            var minorProp = packageVersionProp.FindPropertyRelative("_minor");
            var patchProp = packageVersionProp.FindPropertyRelative("_patch");

            // SerializedPropertyの各値を現在のバージョン値で上書きする
            // * OnInspectorGUIの最後にApplyModifiedPropertyされることで変更がassetに反映される
            majorProp.intValue = VCIVersion.MAJOR;
            minorProp.intValue = VCIVersion.MINOR;
            patchProp.intValue = VCIVersion.PATCH;

            // パッケージのバージョンを表示する
            // * readonlyにするためにdisableする
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.TextField("Package Version", _vciPackageSettings.PackageVersion.GetValue());
            }
        }

        private void DrawDependencyPackageVersion(SerializedProperty packageVersionProp, PackageVersion packageVersion)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                // Foldout付きのLabelを作る
                EditorGUILayout.PropertyField(packageVersionProp, false);
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.TextField(packageVersion.GetValue());
                }
            }

            if (packageVersionProp.isExpanded)
            {
                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    var majorProp = packageVersionProp.FindPropertyRelative("_major");
                    var minorProp = packageVersionProp.FindPropertyRelative("_minor");
                    var patchProp = packageVersionProp.FindPropertyRelative("_patch");

                    EditorGUILayout.PropertyField(majorProp);
                    EditorGUILayout.PropertyField(minorProp);
                    EditorGUILayout.PropertyField(patchProp);
                }
            }
        }

        private void DrawPackageDisplayName()
        {
            var displayNameProp = serializedObject.FindProperty("_displayName");
            EditorGUILayout.PropertyField(displayNameProp);
        }

        private void DrawPackageDescription()
        {
            var descriptionProp = serializedObject.FindProperty("_description");

            _isDescriptionSectionExpanded = EditorGUILayout.Foldout(_isDescriptionSectionExpanded, "Package Description");

            if (_isDescriptionSectionExpanded)
            {
                descriptionProp.stringValue = EditorGUILayout.TextArea(descriptionProp.stringValue);
            }
        }

        private void DrawUnityVersion()
        {
            var unityVersionProp = serializedObject.FindProperty("_unityVersion");
            var majorVersionProp = unityVersionProp.FindPropertyRelative("_major");
            var minorVersionProp = unityVersionProp.FindPropertyRelative("_minor");

            using (new EditorGUILayout.HorizontalScope())
            {
                // Foldout付きのLabelを作る
                EditorGUILayout.PropertyField(unityVersionProp, new GUIContent("Compatible Unity Version"), false);
                // Unityバージョンを表示する
                // * read onlyにするためにdisableする
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.TextField(_vciPackageSettings.UnityVersion);
                }
            }

            // 展開されているときのみ表示
            if (unityVersionProp.isExpanded)
            {
                // 見やすくするためにboxで囲む
                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    EditorGUILayout.PropertyField(majorVersionProp);
                    EditorGUILayout.PropertyField(minorVersionProp);
                }
            }
        }

        private void DrawKeywords()
        {
            var keywordProperty = serializedObject.FindProperty("_keywords");
            EditorGUILayout.PropertyField(keywordProperty);
        }

        private void DrawAuthor()
        {
            var authorProp = serializedObject.FindProperty("_author");
            var nameProp = authorProp.FindPropertyRelative("_name");
            var emailProp = authorProp.FindPropertyRelative("_email");
            var urlProp = authorProp.FindPropertyRelative("_url");

            // Foldout付きのLabelを作る
            EditorGUILayout.PropertyField(authorProp, false);

            // 展開されているときのみ表示する
            if (authorProp.isExpanded)
            {
                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    EditorGUILayout.PropertyField(nameProp);
                    EditorGUILayout.PropertyField(emailProp);
                    EditorGUILayout.PropertyField(urlProp);
                }
            }
        }

        private void DrawDependencies()
        {
            var dependenciesProp = serializedObject.FindProperty("_dependencies");

            // Foldout付きのLabelを作る
            EditorGUILayout.PropertyField(dependenciesProp, false);
            if (dependenciesProp.isExpanded)
            {
                EditorGUILayout.PropertyField(dependenciesProp.FindPropertyRelative("Array.size"));
                // Dependencyを増減させた結果を後続のGUIに反映する
                serializedObject.ApplyModifiedProperties();

                using (new EditorGUI.IndentLevelScope())
                {
                    for (var i = 0; i < dependenciesProp.arraySize; i++)
                    {
                        var dependencyProp = dependenciesProp.GetArrayElementAtIndex(i);
                        ShowDependency(dependencyProp, i);
                    }
                }
            }
        }

        private void ShowDependency(SerializedProperty dependencyProp, int arrayIndex)
        {
            // Foldout付きのLabelを作る
            EditorGUILayout.PropertyField(dependencyProp, new GUIContent($"Dependency {arrayIndex}"), false);
            if (dependencyProp.isExpanded)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    DrawOfficialPackageName(dependencyProp.FindPropertyRelative("_officialPackageName"), _vciPackageSettings.Dependencies[arrayIndex].OfficialPackageName);
                    DrawDependencyPackageVersion(dependencyProp.FindPropertyRelative("_packageVersion"), _vciPackageSettings.Dependencies[arrayIndex].PackageVersion);
                }
            }
        }
    }
}
