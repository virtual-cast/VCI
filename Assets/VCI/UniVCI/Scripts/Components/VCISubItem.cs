using System;
using UnityEngine;

namespace VCI
{
    [DisallowMultipleComponent]
    public sealed class VCISubItem : MonoBehaviour
    {
        public string ExportWarning
        {
            get
            {
                if (transform.parent == null) return "VCISubItem require parent";

                if (transform.parent.parent != null) return "VCISubItem's parent should be root";

                return null;
            }
        }

        private void Reset()
        {
            if (GetComponent<Rigidbody>() == null)
            {
                gameObject.AddComponent<Rigidbody>();
            }
            InitializeKey();
        }

        private void OnValidate()
        {
            InitializeKey();
        }

        public void InitializeKey()
        {
            var parent = transform.parent;
            if (parent == null) { return; }

            var parentVciObject = parent.GetComponent<VCIObject>();
            if (parentVciObject == null) { return; }

            if (Key != 0 && !parentVciObject.IsDuplicatedSubItemKey(Key)) { return; }

            Key = parentVciObject.GenerateSubItemKey();
        }

        #region

        public bool Grabbable;
        public bool Scalable;
        public bool UniformScaling;
        public bool Attractable = true;
        public float AttractableDistance = DefaultAttractableDistance;
        public int GroupId;
        public int NodeIndex;
        /// <summary>VCI内で一意となる値</summary>
        /// <remarks>0 の時は未設定と見なす</remarks>
        public int Key;

        public static readonly float DefaultAttractableDistance = 20;

        public VCISubItem CopyTo(GameObject go)
        {
            var subItem = go.AddComponent<VCISubItem>();
            subItem.Grabbable = Grabbable;
            subItem.Scalable = Scalable;
            subItem.UniformScaling = UniformScaling;
            subItem.Attractable = Attractable;
            subItem.AttractableDistance = AttractableDistance;
            subItem.GroupId = GroupId;
            subItem.NodeIndex = NodeIndex;
            subItem.Key = Key;
            return subItem;
        }

        #endregion

        #region RigidBody Collision

        public event Action<VCICollisionTrigger> CollisionEvent;

        private void RaiseCollisionEvent(EnterStatus status, Collision collision)
        {
            var handler = CollisionEvent;
            if (handler == null) return;
            handler(VCICollisionTrigger.Create(this, status, collision));
        }

        private void OnCollisionEnter(Collision collision)
        {
            RaiseCollisionEvent(EnterStatus.Enter, collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            RaiseCollisionEvent(EnterStatus.Exit, collision);
        }

        #endregion

        #region Collider Trigger

        public event Action<VCICollisionTrigger> TriggerEvent;

        private void RaiseTrigger(EnterStatus status, Collider collider)
        {
            var handler = TriggerEvent;
            if (handler == null) return;
            handler(VCICollisionTrigger.Create(this, status, collider));
        }

        private void OnTriggerEnter(Collider other)
        {
            RaiseTrigger(EnterStatus.Enter, other);
        }

        private void OnTriggerExit(Collider other)
        {
            RaiseTrigger(EnterStatus.Exit, other);
        }

        #endregion
    }
}
