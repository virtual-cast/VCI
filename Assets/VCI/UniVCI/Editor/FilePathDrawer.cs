using System;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace VCI
{
    [CustomPropertyDrawer(typeof(FilePathAttribute))]
    public sealed class FilePathDrawer : PropertyDrawer
    {
        private string EmbeddedScriptWorkspacePath =>
            Path.GetFullPath(System.Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\LocalLow\infiniteloop Co,Ltd\VirtualCast\EmbeddedScriptWorkspace");

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.HelpBox(position, "This attribute is a string only.", MessageType.Error);
                return;
            }
            EditorGUI.BeginProperty(position, label, property);
            var buttonPosition = new Rect(position.x + position.width - 30, position.y, 30, position.height);
            var textPosition = new Rect(position.x, position.y, position.width - buttonPosition.width - 5, position.height);

            if (GUI.Button(buttonPosition, "..."))
            {
                GUI.FocusControl("...");
                var dirpath = EmbeddedScriptWorkspacePath;
                if (File.Exists(property.stringValue))
                {
                    dirpath = Path.GetDirectoryName(property.stringValue);
                }
                var filepath = (FilePathAttribute)attribute;
                var path = EditorUtility.OpenFilePanel("select file", dirpath, filepath.extensionFilter);
                if (false == string.IsNullOrEmpty(path))
                {
                    property.stringValue = path;
                    property.serializedObject.ApplyModifiedProperties();
                    GUIUtility.ExitGUI();
                }
            }
            else
            {
                property.stringValue = EditorGUI.TextField(textPosition, label, property.stringValue);
            }
            EditorGUI.EndProperty();
        }
    }
}




