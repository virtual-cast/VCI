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

        void OnEnable()
        {
            _target = (VCIObject)target;
            _scriptProp = serializedObject.FindProperty("Scripts");
        }

        public override void OnInspectorGUI()
        {
            // Version   
            EditorGUI.BeginDisabledGroup(true);
            if(string.IsNullOrEmpty(_target.Meta.exporterVersion))
                _target.Meta.exporterVersion = VCIVersion.VERSION;
            _target.Meta.exporterVersion = EditorGUILayout.TextField("ExporterVersion", _target.Meta.exporterVersion);
            //if(string.IsNullOrEmpty(_target.Meta.specVersion))
            //    _target.Meta.specVersion = VCISpecVersion.Version;
            //_target.Meta.specVersion = EditorGUILayout.TextField("SpecVersion", _target.Meta.specVersion);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.Space();

            // information
            EditorGUILayout.LabelField("Information",  EditorStyles.boldLabel);
            _target.Meta.title = EditorGUILayout.TextField("Title", _target.Meta.title);
            _target.Meta.version = EditorGUILayout.TextField("Version", _target.Meta.version);
            _target.Meta.author = EditorGUILayout.TextField("Author", _target.Meta.author);
            _target.Meta.contactInformation = EditorGUILayout.TextField("ContactInformation", _target.Meta.contactInformation);
            _target.Meta.reference = EditorGUILayout.TextField("Reference", _target.Meta.reference);
            
            // thumbnail
            _target.Meta.thumbnail = (Texture2D)EditorGUILayout.ObjectField("Thumbnail", _target.Meta.thumbnail, typeof(Texture2D), false);
            EditorGUILayout.Space();

            // description
            EditorGUILayout.LabelField("Description",  EditorStyles.boldLabel);
            _target.Meta.description = EditorGUILayout.TextArea(_target.Meta.description, GUILayout.MinHeight(100));
            EditorGUILayout.Space();

            // License
            EditorGUILayout.LabelField("License",  EditorStyles.boldLabel);
            _target.Meta.modelDataLicenseType = 
                (UniGLTF.glTF_VCAST_vci_meta.LicenseType)EditorGUILayout.EnumPopup("Model Data License", _target.Meta.modelDataLicenseType);
            _target.Meta.modelDataOtherLicenseUrl = 
                EditorGUILayout.TextField("ModelDataOtherLicenseUrl", _target.Meta.modelDataOtherLicenseUrl);
            EditorGUILayout.Space();

            _target.Meta.scriptLicenseType = 
                (UniGLTF.glTF_VCAST_vci_meta.LicenseType)EditorGUILayout.EnumPopup("Script License", _target.Meta.scriptLicenseType);
            _target.Meta.scriptOtherLicenseUrl = 
                EditorGUILayout.TextField("ScriptOtherLicenseUrl", _target.Meta.scriptOtherLicenseUrl);
            EditorGUILayout.Space();

            // script settings
            EditorGUILayout.LabelField("Script settings",  EditorStyles.boldLabel);
            _target.Meta.scriptWriteProtected = EditorGUILayout.Toggle("ScriptWriteProtected", _target.Meta.scriptWriteProtected);
            _target.Meta.scriptEnableDebugging = EditorGUILayout.Toggle("ScriptEnableDebugging", _target.Meta.scriptEnableDebugging);
            EditorGUILayout.Space();

            // scripts
            serializedObject.Update ();
            EditorGUILayout.PropertyField(_scriptProp, true);
            serializedObject.ApplyModifiedProperties ();
        }
    }
}
