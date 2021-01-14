using System;
using UnityEngine;

namespace VCI
{
    public enum GltfCubemapFace
    {
        PositiveX,
        NegativeX,
        PositiveY,
        NegativeY,
        PositiveZ,
        NegativeZ,
    }

    public static class GltfCubemapFaceExtensions
    {
        public static CubemapFace ConvertToUnityCubemapFace(this GltfCubemapFace face)
        {
            switch (face)
            {
                case GltfCubemapFace.PositiveX:
                    return CubemapFace.NegativeX; // X Reverse
                case GltfCubemapFace.NegativeX:
                    return CubemapFace.PositiveX; // X Reverse
                case GltfCubemapFace.PositiveY:
                    return CubemapFace.PositiveY;
                case GltfCubemapFace.NegativeY:
                    return CubemapFace.NegativeY;
                case GltfCubemapFace.PositiveZ:
                    return CubemapFace.PositiveZ;
                case GltfCubemapFace.NegativeZ:
                    return CubemapFace.NegativeZ;
                default:
                    throw new ArgumentOutOfRangeException(nameof(face), face, null);
            }
        }
    }
}
