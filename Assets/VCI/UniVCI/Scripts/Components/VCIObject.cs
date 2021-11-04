using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniGLTF;

namespace VCI
{
    [DisallowMultipleComponent]
    public sealed class VCIObject : MonoBehaviour
    {
        [SerializeField] public VCIMeta Meta;

        [SerializeField] public List<VciScript> Scripts = new List<VciScript>();

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
#if UNITY_EDITOR
                foreach (var script in Scripts)
                {
                    if (script.textAsset != null)
                    {
                        script.source = script.textAsset.text;
                    }
                }
#endif
            }
        }
    }
}