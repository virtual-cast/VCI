using System;

namespace VCI
{
    [Serializable]
    public sealed class EffekseerModelJsonObject
    {
        [UniGLTF.JsonSchema(Required = true, Minimum = 0)]
        public int bufferView = -1;
    }
}
