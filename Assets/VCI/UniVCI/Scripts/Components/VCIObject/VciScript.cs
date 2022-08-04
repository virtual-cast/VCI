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

        /// <summary>
        /// Editor-only. <c>VciScriptHelper</c> を経由して参照される.
        /// </summary>
        public TextAsset textAsset;

        /// <summary>
        /// Editor-only. <c>VciScriptHelper</c> を経由して参照される.
        /// </summary>
        [FilePath("lua")]
        public string filePath;

        [TextArea]
        public string source;
    }
}
