using System;
using VCIGLTF;

namespace VCI
{
    [Serializable]
    public sealed class glTFCubemapFaceTextureInfo : glTFTextureInfo
    {
        public override glTFTextureTypes TextureType => glTFTextureTypes.Unknown;

        public glTFCubemapFaceTextureInfo()
        {
        }
    }
}
