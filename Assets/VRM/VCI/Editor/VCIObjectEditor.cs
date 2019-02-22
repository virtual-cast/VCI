using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace VCI
{
    [CustomEditor(typeof(VCIObject))]
    public class VCIObjectEditor : Editor
    {
        private VCIObject _target;
        private SerializedProperty _scriptProp;
        private SerializedProperty _metaProp;
        private SerializedProperty _thumbnailProp;
        private SerializedProperty _vciScriptProp;
        

        private 

        void OnEnable()
        {
            _target = (VCIObject)target;
            _scriptProp = serializedObject.FindProperty("m_Script");
            _metaProp = serializedObject.FindProperty("Meta");
            _thumbnailProp = _metaProp.FindPropertyRelative("thumbnail");
            _vciScriptProp = serializedObject.FindProperty("Scripts");
        }

        private void SetMetaPropertyField(SerializedProperty meta, string name)
        {
            var prop = meta.FindPropertyRelative(name);
            if(prop != null)
            {
                EditorGUILayout.PropertyField(prop);
            }
            else
            {
                Debug.LogError("SetMetaPropertyField SerializedProperty not found");
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update ();
            EditorGUILayout.PropertyField(_scriptProp);

            // Version   
            EditorGUI.BeginDisabledGroup(true);
            if(string.IsNullOrEmpty(_target.Meta.exporterVersion))
                _target.Meta.exporterVersion = VCIVersion.VERSION;
            SetMetaPropertyField(_metaProp, nameof(_target.Meta.exporterVersion));
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.Space();

            // information
            EditorGUILayout.LabelField("Information",  EditorStyles.boldLabel);
            SetMetaPropertyField(_metaProp, nameof(_target.Meta.title));
            SetMetaPropertyField(_metaProp, nameof(_target.Meta.version));
            SetMetaPropertyField(_metaProp, nameof(_target.Meta.author));
            SetMetaPropertyField(_metaProp, nameof(_target.Meta.contactInformation));
            SetMetaPropertyField(_metaProp, nameof(_target.Meta.reference));
            
            // thumbnail
            SetMetaPropertyField(_metaProp, nameof(_target.Meta.thumbnail));
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            _thumbnailProp.objectReferenceValue = (Texture2D)EditorGUILayout.ObjectField(_thumbnailProp.objectReferenceValue, typeof(Texture2D), false, GUILayout.Width(100), GUILayout.Height(100));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            // description
            SetMetaPropertyField(_metaProp, nameof(_target.Meta.description));
            EditorGUILayout.Space();

            // License
            SetMetaPropertyField(_metaProp, nameof(_target.Meta.modelDataLicenseType));
            SetMetaPropertyField(_metaProp, nameof(_target.Meta.modelDataOtherLicenseUrl));
            EditorGUILayout.Space();

            SetMetaPropertyField(_metaProp, nameof(_target.Meta.scriptLicenseType));
            SetMetaPropertyField(_metaProp, nameof(_target.Meta.scriptOtherLicenseUrl));
            EditorGUILayout.Space();

            // script settings
            SetMetaPropertyField(_metaProp, nameof(_target.Meta.scriptWriteProtected));
            SetMetaPropertyField(_metaProp, nameof(_target.Meta.scriptEnableDebugging));
            EditorGUILayout.Space();

            // vci scripts
            EditorGUILayout.PropertyField(_vciScriptProp, true);
            serializedObject.ApplyModifiedProperties ();
        }
    }
}
