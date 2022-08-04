using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

namespace VCI
{
    [CustomPropertyDrawer(typeof(VciScript))]
    public sealed class VCIScriptPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var lineCount = 6;
            var source = property.FindPropertyRelative("source");
            var textAssetProperty = property.FindPropertyRelative("textAsset");
            var filePathProperty = property.FindPropertyRelative("filePath");

            var hasTextAsset = textAssetProperty.objectReferenceValue as TextAsset;
            var hasFilePath = !string.IsNullOrEmpty(filePathProperty.stringValue);

            if (!hasTextAsset)
            {
                if (hasFilePath)
                {
                    lineCount += 1;
                }
                else
                {
                    lineCount += source.stringValue.Count(x => x == '\n') + 2;
                }
            }

            return EditorGUIUtility.singleLineHeight * lineCount;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PrefixLabel(position, label);
            position.y += EditorGUIUtility.singleLineHeight;

            var nameProperty = property.FindPropertyRelative("name");
            var mimeTypeProperty = property.FindPropertyRelative("mimeType");
            var targetEngineProperty = property.FindPropertyRelative("targetEngine");
            var textAssetProperty = property.FindPropertyRelative("textAsset");
            var filePathProperty = property.FindPropertyRelative("filePath");
            var sourceProperty = property.FindPropertyRelative("source");

            EditorGUI.indentLevel++;

            EditorGUI.PropertyField(position, nameProperty);
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, mimeTypeProperty);
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, targetEngineProperty);
            position.y += EditorGUIUtility.singleLineHeight;

            var textAsset = textAssetProperty.objectReferenceValue as TextAsset;
            var hasTextAssets = (bool)textAsset;
            var filePath = filePathProperty.stringValue;
            var hasFilePath = !string.IsNullOrEmpty(filePath);
            var source = sourceProperty.stringValue;
            var hasSource = !string.IsNullOrEmpty(source);

            // Text Asset Field
            {
                var buttonRect = new Rect(position.x + position.width - 30, position.y, 30, position.height);
                var propertyRect = new Rect(position.x, position.y, position.width - buttonRect.width - 5, position.height);
                EditorGUI.BeginDisabledGroup(!hasTextAssets && (hasFilePath || hasSource));
                {
                    EditorGUI.PropertyField(propertyRect, textAssetProperty);
                }
                EditorGUI.EndDisabledGroup();
                EditorGUI.BeginDisabledGroup(!hasTextAssets);
                {
                    if (GUI.Button(buttonRect, "x"))
                    {
                        textAssetProperty.objectReferenceValue = null;
                        sourceProperty.stringValue = null;
                    }
                }
                EditorGUI.EndDisabledGroup();
                position.y += EditorGUIUtility.singleLineHeight;
            }

            // File Path Field
            if (!hasTextAssets)
            {
                var buttonRect = new Rect(position.x + position.width - 30, position.y, 30, position.height);
                var propertyRect = new Rect(position.x, position.y, position.width - buttonRect.width - 5, position.height);
                EditorGUI.BeginDisabledGroup(!hasFilePath && hasSource);
                {
                    EditorGUI.PropertyField(propertyRect, filePathProperty);
                }
                EditorGUI.EndDisabledGroup();
                EditorGUI.BeginDisabledGroup(!hasFilePath);
                {
                    if (GUI.Button(buttonRect, "x"))
                    {
                        filePathProperty.stringValue = null;
                        sourceProperty.stringValue = null;
                    }
                }
                EditorGUI.EndDisabledGroup();
                position.y += EditorGUIUtility.singleLineHeight;
            }

            // Source Field
            if (!hasTextAssets && !hasFilePath)
            {
                var buttonRect = new Rect(position.x + position.width - 30, position.y, 30, position.height);
                var propertyRect = new Rect(position.x, position.y, position.width - buttonRect.width - 5, position.height);
                EditorGUI.LabelField(propertyRect, "Source");
                EditorGUI.BeginDisabledGroup(!hasSource);
                {
                    if (GUI.Button(buttonRect, "x"))
                    {
                        sourceProperty.stringValue = null;
                    }
                }
                EditorGUI.EndDisabledGroup();
                position.y += EditorGUIUtility.singleLineHeight;
                position.height += EditorGUIUtility.singleLineHeight * (sourceProperty.stringValue.Count(x => x == '\n'));
                sourceProperty.stringValue = EditorGUI.TextArea(position, sourceProperty.stringValue);
            }

            // Apply file name to script name
            if (nameProperty.stringValue != "main")
            {
                if (hasTextAssets) nameProperty.stringValue = Path.GetFileNameWithoutExtension(textAsset.name);
                else if (hasFilePath) nameProperty.stringValue = Path.GetFileNameWithoutExtension(filePath);
            }

            EditorGUI.indentLevel--;

            EditorGUI.EndProperty();
        }
    }
}
