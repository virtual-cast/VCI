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
            SetMetaPropertyField(_metaProp, "exporterVersion");
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.Space();

            // information
            EditorGUILayout.LabelField("Information",  EditorStyles.boldLabel);
            SetMetaPropertyField(_metaProp, "title");
            SetMetaPropertyField(_metaProp, "version");
            SetMetaPropertyField(_metaProp, "author");
            SetMetaPropertyField(_metaProp, "contactInformation");
            SetMetaPropertyField(_metaProp, "reference");
            
            // thumbnail
            SetMetaPropertyField(_metaProp, "thumbnail");
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            _thumbnailProp.objectReferenceValue = (Texture2D)EditorGUILayout.ObjectField(_thumbnailProp.objectReferenceValue, typeof(Texture2D), false, GUILayout.Width(100), GUILayout.Height(100));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            // description
            SetMetaPropertyField(_metaProp, "description");
            EditorGUILayout.Space();

            // License
            SetMetaPropertyField(_metaProp, "modelDataLicenseType");
            SetMetaPropertyField(_metaProp, "modelDataOtherLicenseUrl");
            EditorGUILayout.Space();

            SetMetaPropertyField(_metaProp, "scriptLicenseType");
            SetMetaPropertyField(_metaProp, "scriptOtherLicenseUrl");
            EditorGUILayout.Space();

            // script settings
            SetMetaPropertyField(_metaProp, "scriptWriteProtected");
            SetMetaPropertyField(_metaProp, "scriptEnableDebugging");
            EditorGUILayout.Space();

            // vci scripts
            EditorGUILayout.PropertyField(_vciScriptProp, true);
            serializedObject.ApplyModifiedProperties ();
        }
    }
}
