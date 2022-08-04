using System;

namespace VCI
{
    [Serializable]
    public sealed class AnimationReferenceJsonObject
    {
        [UniGLTF.JsonSchema(Required = true, Minimum = 0)]
        public int animation = -1;
    }
}
