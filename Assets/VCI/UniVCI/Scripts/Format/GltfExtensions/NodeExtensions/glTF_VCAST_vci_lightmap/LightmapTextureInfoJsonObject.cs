using System;
using UniGLTF;

namespace VCI
{
    [Serializable]
    public sealed class LightmapTextureInfoJsonObject : glTFTextureInfo
    {
        public LightmapTextureInfoJsonObject()
        {
            base.texCoord = 1;
        }
    }
}
