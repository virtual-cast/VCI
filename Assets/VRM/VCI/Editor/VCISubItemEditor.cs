using UnityEditor;
using UnityEngine;


namespace VCI
{
    [CustomEditor(typeof(VCISubItem))]
    public class VCISubItemEditor : Editor
    {
        VCISubItem _target;

        void OnEnable()
        {
            _target = (VCISubItem)target;
        }

        public override void OnInspectorGUI()
        {
            _target.Grabbable = EditorGUILayout.Toggle("Grabbable", _target.Grabbable);
            EditorGUI.BeginDisabledGroup(!_target.Grabbable);
            _target.Scalable = EditorGUILayout.Toggle("Scalable", _target.Scalable);
            EditorGUI.BeginDisabledGroup(!_target.Scalable);
            EditorGUI.indentLevel++;
            _target.UniformScaling = EditorGUILayout.Toggle("UniformScaling", _target.UniformScaling);
            EditorGUI.indentLevel--;
            EditorGUI.EndDisabledGroup();
            EditorGUI.EndDisabledGroup();

            if (!_target.Grabbable)
            {
                _target.Scalable = _target.UniformScaling = false;
            }
            else if (!_target.Scalable)
            {
                _target.UniformScaling = false;
            }

            _target.GroupId = EditorGUILayout.IntField("GroupId", _target.GroupId);

            if (Application.isPlaying)
            {
                if (GUILayout.Button("onUse"))
                {
                    _target.TriggerAction();
                }
            }
        }
    }
}
