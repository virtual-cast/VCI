using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VCI
{
    [DisallowMultipleComponent]
    public sealed class VCIAttachable : MonoBehaviour
    {
        // アタッチ対象となるHumanBodyBones
        [SerializeField]
        private HumanBodyBones[] _attachableHumanBodyBones;

        // アタッチする距離
        [SerializeField]
        private float _attachableDistance;

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

# if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0, 1.0f, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, _attachableDistance);
        }
#endif
    }
}
