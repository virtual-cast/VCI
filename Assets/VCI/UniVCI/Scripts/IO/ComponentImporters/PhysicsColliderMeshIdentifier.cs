using System;

namespace VCI
{
    internal readonly struct PhysicsColliderMeshIdentifier : IEquatable<PhysicsColliderMeshIdentifier>
    {
        public int PositionAccessorIndex { get; }
        public int IndicesAccessorIndex { get; }

        public PhysicsColliderMeshIdentifier(int positionAccessorIndex, int indicesAccessorIndex)
        {
            PositionAccessorIndex = positionAccessorIndex;
            IndicesAccessorIndex = indicesAccessorIndex;
        }

        public bool Equals(PhysicsColliderMeshIdentifier other)
        {
            return PositionAccessorIndex == other.PositionAccessorIndex && IndicesAccessorIndex == other.IndicesAccessorIndex;
        }

        public override bool Equals(object obj)
        {
            return obj is PhysicsColliderMeshIdentifier other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (PositionAccessorIndex * 397) ^ IndicesAccessorIndex;
            }
        }
    }
}
