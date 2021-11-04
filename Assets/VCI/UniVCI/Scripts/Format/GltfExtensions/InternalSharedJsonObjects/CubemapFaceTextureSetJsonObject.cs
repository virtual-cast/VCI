using System;

namespace VCI
{
    [Serializable]
    public sealed class CubemapFaceTextureSetJsonObject
    {
        public CubemapFaceTextureInfoJsonObject cubemapPositiveX;
        public CubemapFaceTextureInfoJsonObject cubemapNegativeX;
        public CubemapFaceTextureInfoJsonObject cubemapPositiveY;
        public CubemapFaceTextureInfoJsonObject cubemapNegativeY;
        public CubemapFaceTextureInfoJsonObject cubemapPositiveZ;
        public CubemapFaceTextureInfoJsonObject cubemapNegativeZ;
    }
}
