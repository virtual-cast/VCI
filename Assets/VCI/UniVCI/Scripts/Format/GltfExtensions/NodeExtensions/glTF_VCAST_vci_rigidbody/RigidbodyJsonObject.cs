using System;

namespace VCI
{
    /// <summary>
    /// Rigidbody info
    /// </summary>
    [Serializable]
    public sealed class RigidbodyJsonObject
    {
        public const string NoneInterpolateString = "none";
        public const string InterpolateInterpolateString = "interpolate";
        public const string ExtrapolateInterpolateString = "extrapolate";

        public const string DiscreteCollisionDetectionString = "discrete";
        public const string ContinuousCollisionDetectionString = "continuous";
        public const string ContinuousDynamicCollisionDetectionString = "continuousdynamic";
        public const string ContinuousSpeculativeCollisionDetectionString = "continuousspeculative";

        public float mass = 1.0f;
        public float drag = 0.0f;
        public float angularDrag = 0.05f;
        public bool useGravity = true;
        public bool isKinematic = false;
        public string interpolate;
        public string collisionDetection;

        public bool freezePositionX = false;
        public bool freezePositionY = false;
        public bool freezePositionZ = false;

        public bool freezeRotationX = false;
        public bool freezeRotationY = false;
        public bool freezeRotationZ = false;
    }
}
