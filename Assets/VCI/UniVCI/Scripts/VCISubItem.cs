using System;
using UnityEngine;

namespace VCI
{
    [DisallowMultipleComponent]
    public class VCISubItem : MonoBehaviour
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

        #region

        public bool Grabbable;
        public bool Scalable;
        public bool UniformScaling;
        public int GroupId;
        public int NodeIndex;

        public VCISubItem CopyTo(GameObject go)
        {
            var subItem = go.AddComponent<VCISubItem>();
            subItem.Grabbable = Grabbable;
            subItem.Scalable = Scalable;
            subItem.UniformScaling = UniformScaling;
            subItem.GroupId = GroupId;
            subItem.NodeIndex = NodeIndex;
            return subItem;
        }

        #endregion

        private void Reset()
        {
            var rb = GetComponent<Rigidbody>();
            if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        }

#if UNITY_EDITOR
        public event Action ActionTriggered;
        public void TriggerAction()
        {
            var handler = ActionTriggered;
            if (handler == null) return;
            handler();
        }
#endif

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

        private void OnCollisionStay(Collision collision)
        {
            RaiseCollisionEvent(EnterStatus.Stay, collision);
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

        private void OnTriggerStay(Collider other)
        {
            RaiseTrigger(EnterStatus.Stay, other);
        }

        private void OnTriggerExit(Collider other)
        {
            RaiseTrigger(EnterStatus.Exit, other);
        }

        #endregion
    }
}