using System;

namespace VCI
{
    [Serializable]
    public sealed class glTFCubemapFaceTextureSet
    {
        public glTFCubemapFaceTextureInfo cubemapPositiveX;
        public glTFCubemapFaceTextureInfo cubemapNegativeX;
        public glTFCubemapFaceTextureInfo cubemapPositiveY;
        public glTFCubemapFaceTextureInfo cubemapNegativeY;
        public glTFCubemapFaceTextureInfo cubemapPositiveZ;
        public glTFCubemapFaceTextureInfo cubemapNegativeZ;
    }
}
