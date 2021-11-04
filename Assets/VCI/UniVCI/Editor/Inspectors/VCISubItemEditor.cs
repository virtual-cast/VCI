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
            var subItem = (VCISubItem)target;
            var subItemParent = subItem.transform.parent;
            if (subItem.GetComponent<VCIObject>() != null ||
                subItemParent == null ||
                subItemParent.GetComponent<VCIObject>() == null ||
                subItemParent.parent != null
            ) {
                EditorGUILayout.HelpBox(VCIConfig.GetText("warning_subitem_not_under_vciobject"), MessageType.Error);
            }

            serializedObject.Update();
            {
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    EditorGUILayout.PropertyField(grabbable);
                    if (check.changed)
                    {
                        attractable.boolValue = grabbable.boolValue;
                    }
                }

                using (new EditorGUI.IndentLevelScope())
                {
                    using (new EditorGUI.DisabledGroupScope(!grabbable.boolValue))
                    {
                        EditorGUILayout.PropertyField(scalable);
                        using (new EditorGUI.DisabledGroupScope(!scalable.boolValue))
                        using (new EditorGUI.IndentLevelScope())
                        {
                            EditorGUILayout.PropertyField(uniform);
                        }
                        EditorGUILayout.PropertyField(attractable);
                    }
                }

                if (!grabbable.boolValue)
                    scalable.boolValue = uniform.boolValue = attractable.boolValue = false;
                else if (!scalable.boolValue) uniform.boolValue = false;

                EditorGUILayout.PropertyField(group);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
