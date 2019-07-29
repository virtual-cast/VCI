using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VCIGLTF;

namespace VCI
{
    [DisallowMultipleComponent]
    public class VCIObject : MonoBehaviour
    {
        [SerializeField] public VCIImporter.Meta Meta;

        [Serializable]
        public class Script
        {
            public string name = "main";

            public ScriptMimeType mimeType;

            public TargetEngine targetEngine;

#if UNITY_EDITOR
            [SerializeField, FilePath("lua")]
            public string filePath;
#endif

            [SerializeField, TextArea]
            public string source;
        }

        [SerializeField] public List<Script> Scripts = new List<Script>();

        public string Source
        {
            get
            {
                if (Scripts.Where(x => x != null).Any()) return Scripts[0].source;
                return "";
            }
        }

        public string ScriptName
        {
            get
            {
                var first = Scripts.FirstOrDefault(x => x != null);
                if (first != null && !string.IsNullOrEmpty(first.name)) return Scripts[0].name;
                return "main";
            }
        }

        private void Reset()
        {
            Meta.title = name;
        }

        private void OnValidate()
        {

            if (Scripts.Any())
            {
                Scripts.First().name = "main";
            }
        }
    }
}