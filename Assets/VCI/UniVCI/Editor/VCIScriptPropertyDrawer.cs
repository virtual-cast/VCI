using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

namespace VCI
{
    [CustomPropertyDrawer(typeof(VCIObject.Script))]
    public sealed class VCIScriptPropertyDrawer : PropertyDrawer
    {

        private float GetHeight(SerializedProperty property)
        {
            SerializedProperty source = property.FindPropertyRelative("source");
            return EditorGUIUtility.singleLineHeight * (source.stringValue.Count(x => x == '\n') + 1 + 7);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return GetHeight(property);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float startHeight = position.y;
            EditorGUI.BeginProperty(position, label, property);
            Rect labelRect = position;
            labelRect.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PrefixLabel(labelRect, label);
            labelRect.y += EditorGUIUtility.singleLineHeight;

            SerializedProperty name = property.FindPropertyRelative("name");
            SerializedProperty mimeType = property.FindPropertyRelative("mimeType");
            SerializedProperty targetEngine = property.FindPropertyRelative("targetEngine");
            SerializedProperty filePath = property.FindPropertyRelative("filePath");
            SerializedProperty source = property.FindPropertyRelative("source");

            if(!string.IsNullOrEmpty(filePath.stringValue) && name.stringValue != "main")
            {
                name.stringValue = Path.GetFileNameWithoutExtension(filePath.stringValue);
            }
            EditorGUI.PropertyField(labelRect, name);
            labelRect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(labelRect, mimeType);
            labelRect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(labelRect, targetEngine);
            labelRect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(labelRect, filePath);
            labelRect.y += EditorGUIUtility.singleLineHeight;

            EditorGUI.BeginDisabledGroup(!string.IsNullOrEmpty(filePath.stringValue));
            EditorGUI.LabelField(labelRect, "Source");
            labelRect.y += EditorGUIUtility.singleLineHeight;

            var sourceRect = labelRect;
            sourceRect.height = EditorGUIUtility.singleLineHeight * (source.stringValue.Count(x => x == '\n') + 1);
            source.stringValue = EditorGUI.TextArea(sourceRect, source.stringValue);

            labelRect.y += sourceRect.height;
            EditorGUI.EndDisabledGroup();

            EditorGUI.EndProperty();
        }
    }
}


