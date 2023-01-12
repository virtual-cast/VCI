using System;

namespace VCI
{
    [Serializable]
    public sealed class EffekseerEmitterJsonObject
    {
        public const string GlobalEmitterScale = "global";
        public const string LocalEmitterScale = "local";

        [UniGLTF.JsonSchema(Required = true, Minimum = 0)]
        public int effectIndex = -1;
        public bool isPlayOnStart;
        public bool isLoop;
        public string emitterScale;
    }
}
