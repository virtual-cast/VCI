using UnityEditor;

namespace VCI
{
    [CustomEditor(typeof(VCIConfig))]
    public sealed class VCIConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Export Settings", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("_openDirectoryAfterExport"));

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Language Settings", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("_primaryLanguageIndex"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("_terms"), true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}