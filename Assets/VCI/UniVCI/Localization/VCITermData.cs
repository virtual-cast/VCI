using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VCI
{
    [CreateAssetMenu]
    public class VCITermData: ScriptableObject
    {
        [SerializeField] private List<KeyValue> _texts;

        [Serializable]
        public struct KeyValue
        {
            public string key;
            public string value;
        }

        public List<KeyValue> KeyValues => _texts;

        public string GetText(string key)
        {
            return _texts.FirstOrDefault(kv => kv.key == key).value;
        }
    }
}