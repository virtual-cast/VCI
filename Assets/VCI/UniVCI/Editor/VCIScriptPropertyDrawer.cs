using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VCI
{
    [CustomPropertyDrawer(typeof(VCI.VCIObject.Script))]
    public class VCIScriptPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight *16;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
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

            EditorGUI.PropertyField(labelRect, name);
            labelRect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(labelRect, mimeType);
            labelRect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(labelRect, targetEngine);
            labelRect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(labelRect, filePath);
            labelRect.y += EditorGUIUtility.singleLineHeight;

            EditorGUI.BeginDisabledGroup(!string.IsNullOrEmpty(filePath.stringValue));
            var sourceRect = labelRect;
            sourceRect.height = EditorGUIUtility.singleLineHeight * 10;
            EditorGUI.PropertyField(sourceRect, source);
            labelRect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.EndDisabledGroup();

            EditorGUI.EndProperty();
        }
    }
}


