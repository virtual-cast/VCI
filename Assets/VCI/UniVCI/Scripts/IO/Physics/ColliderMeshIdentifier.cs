using System;

namespace VCI
{
    internal readonly struct ColliderMeshIdentifier : IEquatable<ColliderMeshIdentifier>
    {
        public int PositionAccessorIndex { get; }
        public int IndicesAccessorIndex { get; }

        public ColliderMeshIdentifier(int positionAccessorIndex, int indicesAccessorIndex)
        {
            PositionAccessorIndex = positionAccessorIndex;
            IndicesAccessorIndex = indicesAccessorIndex;
        }

        public bool Equals(ColliderMeshIdentifier other)
        {
            return PositionAccessorIndex == other.PositionAccessorIndex && IndicesAccessorIndex == other.IndicesAccessorIndex;
        }

        public override bool Equals(object obj)
        {
            return obj is ColliderMeshIdentifier other && Equals(other);
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
