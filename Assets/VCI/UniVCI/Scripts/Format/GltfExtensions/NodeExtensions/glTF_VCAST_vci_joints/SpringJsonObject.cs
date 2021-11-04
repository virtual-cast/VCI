using System;

namespace VCI
{
    [Serializable]
    public class SpringJsonObject
    {
        public float spring;
        public float damper;
        public float minDistance;
        public float maxDistance;
        public float tolerance;
        public float targetPosition;
    }
}
