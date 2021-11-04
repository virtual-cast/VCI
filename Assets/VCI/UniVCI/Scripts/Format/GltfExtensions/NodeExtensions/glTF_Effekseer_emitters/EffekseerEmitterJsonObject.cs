using System;

namespace VCI
{
    [Serializable]
    public class EffekseerEmitterJsonObject
    {
        [UniGLTF.JsonSchema(Required = true, Minimum = 0)]
        public int effectIndex = -1;
        public bool isPlayOnStart;
        public bool isLoop;
    }
}
