using UnityEditor;
using UnityEngine;

namespace VCI
{
    [CustomEditor(typeof(VCITermData))]
    public sealed class VCITermDataEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var termData = target as VCITermData;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Key", GUILayout.Width(120));
            EditorGUILayout.LabelField("Text");
            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < termData.KeyValues.Count; i++)
            {
                var kv = termData.KeyValues[i];
                EditorGUILayout.BeginHorizontal();
                kv.key = EditorGUILayout.TextField( kv.key, GUILayout.Width(120));
                kv.value = EditorGUILayout.TextField( kv.value);
                termData.KeyValues[i] = kv;
                if (GUILayout.Button("X",GUILayout.Width(20)))
                {
                    termData.KeyValues.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add"))
            {
                termData.KeyValues.Add(new VCITermData.KeyValue());
            }

            if (GUILayout.Button("Sort"))
            {
                termData.KeyValues.Sort((a, b) => string.CompareOrdinal(a.key, b.key));
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
            }

            serializedObject.ApplyModifiedProperties();
        }

    }
}