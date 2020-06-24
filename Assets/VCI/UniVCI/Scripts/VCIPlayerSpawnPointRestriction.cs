using UnityEngine;

namespace VCI
{
    [RequireComponent(typeof(VCIPlayerSpawnPoint))]
    [DisallowMultipleComponent]
    public class VCIPlayerSpawnPointRestriction : MonoBehaviour
    {
        public RangeOfMovement RangeOfMovementRestriction;

        public float LimitRadius;

        public float LimitRectLeft;

        public float LimitRectRight;

        public float LimitRectBackward;

        public float LimitRectForward;

        public Posture PostureRestriction;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            switch (RangeOfMovementRestriction)
            {
                case RangeOfMovement.Circle:
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(transform.position, LimitRadius);
                    break;

                case RangeOfMovement.Rectangle:
                {
                    var t = transform;
                    var origin = t.position;
                    var rotation = t.rotation;

                    var rectA = rotation * new Vector3(LimitRectLeft, 0, LimitRectForward) + origin;
                    var rectB = rotation * new Vector3(LimitRectLeft, 0, LimitRectBackward) + origin;
                    var rectC = rotation * new Vector3(LimitRectRight, 0, LimitRectBackward) + origin;
                    var rectD = rotation * new Vector3(LimitRectRight, 0, LimitRectForward) + origin;

                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(rectA, rectB);
                    Gizmos.DrawLine(rectB, rectC);
                    Gizmos.DrawLine(rectC, rectD);
                    Gizmos.DrawLine(rectD, rectA);
                    break;
                }
            }
        }
#endif
    }

    public enum RangeOfMovement
    {
        NoLimit = 0,
        Circle,
        Rectangle,
    }

    public enum Posture
    {
        NoLimit = 0,
        SitOn,
    }
}
