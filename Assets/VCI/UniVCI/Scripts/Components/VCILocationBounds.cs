using UnityEngine;

namespace VCI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(VCIObject))]
    public class VCILocationBounds : MonoBehaviour
    {
        public Bounds Bounds;

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(Bounds.center, Bounds.size);
        }
#endif

    }
}
