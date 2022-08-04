using System;

namespace VCI
{
    [Serializable]
    public sealed class EffekseerImageJsonObject
    {
        public const string PngMimeTypeString = "image/png";

        [UniGLTF.JsonSchema(Required = true, Minimum = 0)]
        public int bufferView = -1;

        public string mimeType;
    }
}
