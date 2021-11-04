using UnityEngine;

namespace VCI
{
    public readonly struct RigidbodySetting
    {
        public RigidbodyConstraints Constraints { get; }
        public bool IsKinematic { get; }
        public bool UseGravity { get; }

        public RigidbodySetting(Rigidbody rigidbody)
        {
            Constraints = rigidbody.constraints;
            IsKinematic = rigidbody.isKinematic;
            UseGravity = rigidbody.useGravity;
        }
    }
}