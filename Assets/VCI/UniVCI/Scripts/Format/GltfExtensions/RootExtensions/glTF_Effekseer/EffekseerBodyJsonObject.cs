using System;

namespace VCI
{
    [Serializable]
    public class EffekseerBodyJsonObject
    {
        [UniGLTF.JsonSchema(Required = true, Minimum = 0)]
        public int bufferView = -1;
    }
}
