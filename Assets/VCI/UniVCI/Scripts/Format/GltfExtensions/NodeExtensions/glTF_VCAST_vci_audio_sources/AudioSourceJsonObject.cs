using System;
using UniGLTF;

namespace VCI
{
    [Serializable]
    public class AudioSourceJsonObject
    {
        [JsonSchema(Required = true)]
        public int audio;
        public float spatialBlend;
    }
}
