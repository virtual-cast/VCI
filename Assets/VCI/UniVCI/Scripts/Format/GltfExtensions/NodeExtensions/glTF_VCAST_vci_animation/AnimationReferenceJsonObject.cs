using System;

namespace VCI
{
    [Serializable]
    public class AnimationReferenceJsonObject
    {
        [UniGLTF.JsonSchema(Required = true, Minimum = 0)]
        public int animation = -1;
    }
}
