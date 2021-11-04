using System;

namespace VCI
{
    /// <summary>
    /// sound item
    /// </summary>
    [Serializable]
    public class AudioJsonObject
    {
        public const string WavMimeType = "audio/wav";
        public const string Mp3MimeType = "audio/mp3";

        public string name;

        [UniGLTF.JsonSchema(Required = true, Minimum = 0)]
        public int bufferView = -1;

        public string mimeType;
    }
}
