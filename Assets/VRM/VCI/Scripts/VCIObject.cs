#pragma warning disable
using System;
using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UnityEngine;


namespace VCI
{
    [DisallowMultipleComponent]
    public class VCIObject : MonoBehaviour
    {
        [SerializeField]
        public VCIImporter.Meta Meta;

        [Serializable]
        public class Script
        {
            public string name = "main.lua";

            public ScriptMimeType mimeType;

            public TargetEngine targetEngine;

            [SerializeField, TextArea]
            public string source;
        }

        [SerializeField]
        public List<Script> Scripts = new List<Script>();

        public string Source
        {
            get
            {
                if (Scripts.Where(x => x != null).Any())
                {
                    return Scripts[0].source;
                }
                return "";
            }
        }

        public string ScriptName
        {
            get
            {
                var first = Scripts.FirstOrDefault(x => x != null);
                if (first != null && !string.IsNullOrEmpty(first.name))
                {
                    return Scripts[0].name;
                }
                return "main.lua";
            }
        }

        void Reset()
        {
            Meta.title = name;
        }
    }
}
