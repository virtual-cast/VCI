using System;

namespace VCI
{
    [Serializable]
    public class EmbeddedScriptJsonObject
    {
        public const string LuaMimeTypeString = "application/x-lua";

        public const string MoonSharpTargetEngineString = "moonSharp";

        public string name;

        public string mimeType;

        public string targetEngine;

        [UniGLTF.JsonSchema(Minimum = 0)]
        public int source = -1;
    }
}
