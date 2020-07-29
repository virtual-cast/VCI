using System;
using UnityEngine;

namespace VCI
{
    [DisallowMultipleComponent]
    public sealed class VCIAttachable : MonoBehaviour
    {
        // アタッチ対象となるHumanBodyBones
        [SerializeField] private HumanBodyBones[] _attachableHumanBodyBones;

        // アタッチする距離
        [SerializeField] private float _attachableDistance;

        [SerializeField] private bool _scalable;

        [SerializeField] private Vector3 _offset;

        public Func<bool, bool> AttachFunc { get; set; }

        public bool IsAttached { get; set; }

        public HumanBodyBones[] AttachableHumanBodyBones
        {
            get => _attachableHumanBodyBones;
            set => _attachableHumanBodyBones = value;
        }

        public float AttachableDistance
        {
            get => _attachableDistance;
            set => _attachableDistance = value;
        }

        public bool Scalable
        {
            get => _scalable;
            set => _scalable = value;
        }

        public Vector3 Offset
        {
            get => _offset;
            set => _offset = value;
        }

# if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0, 1.0f, 0, 0.3f);
            if (_scalable)
            {
                var s = transform.lossyScale;
                var p = transform.TransformPoint(_offset);
                Gizmos.DrawLine(transform.position, p);
                Gizmos.DrawSphere(p, _attachableDistance * Mathf.Min(s.x, s.y, s.z));
            }
            else
            {
                var p = transform.TransformPoint(_offset);
                Gizmos.DrawLine(transform.position, p);
                Gizmos.DrawSphere(p, _attachableDistance);
            }
        }
#endif
    }
}
