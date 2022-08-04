using System;

namespace VCI
{
    [Serializable]
    public sealed class LimitsJsonObject
    {
        public float min;
        public float max;
        public float bounciness;
        public float bounceMinVelocity = 0.2f;
        public float contactDistance;
    }
}
