using UnityEditor;
using UnityEngine;
using System.Linq;
using System.IO;

namespace VCI
{
    [CustomEditor(typeof(VCIObject))]
    public sealed class VCIObjectEditor : Editor
    {
        private VCIObject _target;
        private SerializedProperty _scriptProp;
        private SerializedProperty _metaProp;
        private SerializedProperty _thumbnailProp;
        private SerializedProperty _vciScriptProp;

        private void OnEnable()
        {
            _target = (VCIObject)target;
            _scriptProp = serializedObject.FindProperty("m_Script");
            _metaProp = serializedObject.FindProperty("Meta");
            _thumbnailProp = _metaProp.FindPropertyRelative("thumbnail");
            _vciScriptProp = serializedObject.FindProperty("Scripts");
        }

        private void SetMetaPropertyField(SerializedProperty meta, string metaName)
        {
            var prop = meta.FindPropertyRelative(metaName);
            if (prop != null)
                EditorGUILayout.PropertyField(prop);
            else
                Debug.LogError("SetMetaPropertyField SerializedProperty not found");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_scriptProp);

            // Version
            EditorGUI.BeginDisabledGroup(true);
            if (string.IsNullOrEmpty(_target.Meta.exporterVersion))
                _target.Meta.exporterVersion = VCIVersion.VERSION;
            SetMetaPropertyField(_metaProp, "exporterVersion");
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.Space();

            // Information
            EditorGUILayout.LabelField("Information", EditorStyles.boldLabel);
            SetMetaPropertyField(_metaProp, "title");
            SetMetaPropertyField(_metaProp, "version");
            SetMetaPropertyField(_metaProp, "author");
            SetMetaPropertyField(_metaProp, "contactInformation");
            SetMetaPropertyField(_metaProp, "reference");

            // Thumbnail
            SetMetaPropertyField(_metaProp, "thumbnail");
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            _thumbnailProp.objectReferenceValue = (Texture2D)EditorGUILayout.ObjectField(
                _thumbnailProp.objectReferenceValue, typeof(Texture2D), false, GUILayout.Width(100),
                GUILayout.Height(100));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            // Description
            SetMetaPropertyField(_metaProp, "description");
            EditorGUILayout.Space();

            // License
            SetMetaPropertyField(_metaProp, "modelDataLicenseType");
            SetMetaPropertyField(_metaProp, "modelDataOtherLicenseUrl");
            EditorGUILayout.Space();

            SetMetaPropertyField(_metaProp, "scriptLicenseType");
            SetMetaPropertyField(_metaProp, "scriptOtherLicenseUrl");
            EditorGUILayout.Space();

            // Script settings
            SetMetaPropertyField(_metaProp, "scriptWriteProtected");
            SetMetaPropertyField(_metaProp, "scriptEnableDebugging");

            EditorGUILayout.Space();

            if (_target.Scripts.Any())
            {
                if (_target.Scripts[0].name != "main")
                {
                    EditorGUILayout.HelpBox("The first script must be named \"main\".", MessageType.Warning);
                }

                var empties = _target.Scripts.Where(x => string.IsNullOrEmpty(x.name));
                if (empties.Any())
                {
                    EditorGUILayout.HelpBox("Some have no script name.", MessageType.Warning);
                }

                var duplicates = _target.Scripts.GroupBy(script => script.name)
                    .Where(n => n.Count() > 1)
                    .Select(group => group.Key).ToList();
                if (duplicates.Any())
                {
                    EditorGUILayout.HelpBox("Duplicate script name.", MessageType.Warning);
                }

                var invalidChars = Path.GetInvalidFileNameChars().Concat(new[] { '.' }).ToArray();
                foreach (var script in _target.Scripts)
                {
                    if (script.name.IndexOfAny(invalidChars) >= 0)
                    {
                        EditorGUILayout.HelpBox("Contains characters that can not be used as scriptName. " + script.name, MessageType.Warning);
                    }
                }
            }

            // vci scripts
            EditorGUILayout.PropertyField(_vciScriptProp, true);
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();

            // Export Button
            if (GUILayout.Button(VCIConfig.GetText("validate_button"), GUILayout.MinHeight(32)))
            {
                if (VCIObjectExporterMenu.Validate())
                    EditorUtility.DisplayDialog("Result", VCIConfig.GetText("no_error"), "OK");
            }

            EditorGUILayout.Space();

            // Export Button
            if (GUILayout.Button(VCIConfig.GetText("export_button"), GUILayout.MinHeight(32)))
            {
#if UNITY_EDITOR_WIN
                VCIObjectExporterMenu.ExportObject();
#endif
            }
        }
    }
}
