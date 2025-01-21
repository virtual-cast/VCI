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

        // PhysicsRigidbodyImporterでは不明値をInterpolateとして扱っている。
        // 該当箇所のコメントに（なぜ？）と書かれているので、経緯を確認する必要がある。
        // Rigidbodyを新しく作成したときの値に合わせて、ここではデフォルト値をNoneとしておく。
        public string interpolate = NoneInterpolateString;
        public string collisionDetection = DiscreteCollisionDetectionString;

        public bool freezePositionX = false;
        public bool freezePositionY = false;
        public bool freezePositionZ = false;

        public bool freezeRotationX = false;
        public bool freezeRotationY = false;
        public bool freezeRotationZ = false;
    }
}
