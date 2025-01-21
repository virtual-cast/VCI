using System;
using UnityEngine;
using VRMShaders;
using ColorSpace = VRMShaders.ColorSpace;

namespace VCI
{
    public sealed class DummyTextureSerializer : ITextureSerializer
    {
        public bool CanExportAsEditorAssetFile(Texture texture, ColorSpace exportColorSpace) => false;

        public (byte[] bytes, string mime) ExportBytesWithMime(Texture2D texture, ColorSpace exportColorSpace)
        {
            return (Array.Empty<byte>(), string.Empty);
        }

        public void ModifyTextureAssetBeforeExporting(Texture texture)
        {
            // Do nothing
        }
    }
}
