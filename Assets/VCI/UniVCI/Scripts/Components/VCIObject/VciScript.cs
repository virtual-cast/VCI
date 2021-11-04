using System;
using UnityEngine;

namespace VCI
{
    [Serializable]
    public sealed class VciScript
    {
        public string name = "main";

        public VciScriptMimeType mimeType;

        public VciScriptTargetEngine targetEngine;

#if UNITY_EDITOR
        public TextAsset textAsset;
#endif

        [SerializeField, TextArea]
        public string source;
    }
}
