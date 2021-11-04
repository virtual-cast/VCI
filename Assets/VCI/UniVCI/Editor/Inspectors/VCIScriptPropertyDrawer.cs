using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

namespace VCI
{
    [CustomPropertyDrawer(typeof(VciScript))]
    public sealed class VCIScriptPropertyDrawer : PropertyDrawer
    {
        private static float GetHeight(SerializedProperty property)
        {
            var source = property.FindPropertyRelative("source");
            var textAssetProperty = property.FindPropertyRelative("textAsset");
            var textAsset = textAssetProperty.objectReferenceValue as TextAsset;
            var lineCount = 6;
            if (!textAsset)
            {
                lineCount += source.stringValue.Count(x => x == '\n') + 2;
            }

            return EditorGUIUtility.singleLineHeight * lineCount;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return GetHeight(property);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var labelRect = position;
            labelRect.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PrefixLabel(labelRect, label);
            labelRect.y += EditorGUIUtility.singleLineHeight;

            var name = property.FindPropertyRelative("name");
            var mimeType = property.FindPropertyRelative("mimeType");
            var targetEngine = property.FindPropertyRelative("targetEngine");
            var textAssetProperty = property.FindPropertyRelative("textAsset");
            var source = property.FindPropertyRelative("source");

            var textAsset = textAssetProperty.objectReferenceValue as TextAsset;
            if (textAsset && name.stringValue != "main")
            {
                name.stringValue = Path.GetFileNameWithoutExtension(textAsset.name);
            }

            EditorGUI.indentLevel++;

            EditorGUI.PropertyField(labelRect, name);
            labelRect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(labelRect, mimeType);
            labelRect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(labelRect, targetEngine);
            labelRect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(labelRect, textAssetProperty);
            labelRect.y += EditorGUIUtility.singleLineHeight;

            if (!textAsset)
            {
                EditorGUI.LabelField(labelRect, "Source");
                labelRect.y += EditorGUIUtility.singleLineHeight;

                var sourceRect = labelRect;
                sourceRect.height = EditorGUIUtility.singleLineHeight * (source.stringValue.Count(x => x == '\n') + 1);
                source.stringValue = EditorGUI.TextArea(sourceRect, source.stringValue);

                labelRect.y += sourceRect.height;
            }

            EditorGUI.indentLevel--;

            EditorGUI.EndProperty();
        }
    }
}