using UnityEditor;
using UnityEngine;

namespace VCI
{
    [CustomEditor(typeof(VCISubItem))]
    [CanEditMultipleObjects]
    public sealed class VCISubItemEditor : Editor
    {
        private SerializedProperty grabbable;
        private SerializedProperty scalable;
        private SerializedProperty uniform;
        private SerializedProperty attractable;
        private SerializedProperty group;

        private void OnEnable()
        {
            grabbable = serializedObject.FindProperty("Grabbable");
            scalable = serializedObject.FindProperty("Scalable");
            uniform = serializedObject.FindProperty("UniformScaling");
            attractable = serializedObject.FindProperty("Attractable");
            group = serializedObject.FindProperty("GroupId");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            {
                EditorGUILayout.PropertyField(grabbable);

                {
                    EditorGUI.BeginDisabledGroup(!grabbable.boolValue);
                    EditorGUILayout.PropertyField(scalable);
                    {
                        EditorGUI.BeginDisabledGroup(!scalable.boolValue);
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(uniform);
                        EditorGUI.indentLevel--;
                        EditorGUI.EndDisabledGroup();
                    }

                    EditorGUILayout.PropertyField(attractable);
                    EditorGUI.EndDisabledGroup();
                }
               attractable.boolValue = attractable.boolValue && grabbable.boolValue;

                if (!grabbable.boolValue)
                    scalable.boolValue = uniform.boolValue = attractable.boolValue = false;
                else if (!scalable.boolValue) uniform.boolValue = false;

                EditorGUILayout.PropertyField(group);
            }
            serializedObject.ApplyModifiedProperties();

#if UNITY_EDITOR
            // debug 用
            if (Application.isPlaying)
                if (GUILayout.Button("onUse"))
                    (target as VCISubItem).TriggerAction();
#endif
        }
    }
}