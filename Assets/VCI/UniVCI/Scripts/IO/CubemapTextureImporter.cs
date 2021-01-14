using System;
using UnityEngine;
using VCIGLTF;

namespace VCI
{
    public sealed class CubemapTextureImporter
    {
        private readonly Func<int, TextureItem> _textureGetter;
        private readonly Shader _importFromDLdrShader;
        private readonly Shader _importFromRgbmShader;

        public CubemapCompressionType CompressionType { get; }

        public CubemapTextureImporter(Func<int, TextureItem> textureGetter, CubemapCompressionType compressionType)
        {
            _textureGetter = textureGetter;
            CompressionType = compressionType;
            _importFromDLdrShader = Shader.Find("Hidden/UniVCI/CubemapConversion/ImportFromDLdr");
            _importFromRgbmShader = Shader.Find("Hidden/UniVCI/CubemapConversion/ImportFromRgbm");
        }

        public Cubemap ConvertCubemap(glTFCubemapTexture gltfCubemapTexture)
        {
            var success = true;
            var mipLength = gltfCubemapTexture.mipmapCount; // original を含めない mipmap だけの個数
            var width = _textureGetter(gltfCubemapTexture.texture.cubemapPositiveX.index).Texture.width;

            var cubemap = new Cubemap(width, TextureFormat.RGBAHalf, mipLength + 1);

            // Set original
            success &= UpdateCubemapFace(gltfCubemapTexture.texture, cubemap, 0);

            // Set mipmaps
            for (var idx = 0; idx < mipLength; ++idx)
            {
                var tex = gltfCubemapTexture.mipmapTextures[idx];
                var mipValue = idx + 1;
                success &= UpdateCubemapFace(tex, cubemap, mipValue);
            }

            // Apply
            cubemap.Apply(updateMipmaps: false, makeNoLongerReadable: true);

            if (success)
            {
                return cubemap;
            }
            else
            {
                UnityEngine.Object.DestroyImmediate(cubemap);
                return null;
            }
        }

        private bool UpdateCubemapFace(glTFCubemapFaceTextureSet src, Cubemap dst, int mipmap)
        {
            var positiveX = _textureGetter(src.cubemapPositiveX.index).Texture;
            var negativeX = _textureGetter(src.cubemapNegativeX.index).Texture;
            var positiveY = _textureGetter(src.cubemapPositiveY.index).Texture;
            var negativeY = _textureGetter(src.cubemapNegativeY.index).Texture;
            var positiveZ = _textureGetter(src.cubemapPositiveZ.index).Texture;
            var negativeZ = _textureGetter(src.cubemapNegativeZ.index).Texture;

            if (!RenderCubemapFaceTexture(positiveX, dst, GltfCubemapFace.PositiveX.ConvertToUnityCubemapFace(), mipmap)) return false;
            if (!RenderCubemapFaceTexture(negativeX, dst, GltfCubemapFace.NegativeX.ConvertToUnityCubemapFace(), mipmap)) return false;
            if (!RenderCubemapFaceTexture(positiveY, dst, GltfCubemapFace.PositiveY.ConvertToUnityCubemapFace(), mipmap)) return false;
            if (!RenderCubemapFaceTexture(negativeY, dst, GltfCubemapFace.NegativeY.ConvertToUnityCubemapFace(), mipmap)) return false;
            if (!RenderCubemapFaceTexture(positiveZ, dst, GltfCubemapFace.PositiveZ.ConvertToUnityCubemapFace(), mipmap)) return false;
            if (!RenderCubemapFaceTexture(negativeZ, dst, GltfCubemapFace.NegativeZ.ConvertToUnityCubemapFace(), mipmap)) return false;

            return true;
        }

        private bool RenderCubemapFaceTexture(Texture2D src, Cubemap dst, CubemapFace srcFace, int mipmap)
        {
            var shader = GetImporterShader();
            if (shader == null) return false;
            var importerMaterial = new Material(shader);

            if (!Mathf.IsPowerOfTwo(dst.width)) return false;
            if (!Mathf.IsPowerOfTwo(dst.height)) return false;

            var originalWidth = dst.width;
            var originalHeight = dst.height;
            var width = Math.Max(2, originalWidth >> mipmap);
            var height = Math.Max(2, originalHeight >> mipmap);

            var rt = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.DefaultHDR, RenderTextureReadWrite.Linear);
            // Decode into Linear RenderTexture
            Graphics.Blit(src, rt, importerMaterial);
            UnityEngine.Object.DestroyImmediate(importerMaterial);

            // Copy to Linear Texture2D
            var tex = new Texture2D(rt.width, rt.height, TextureFormat.RGBAHalf, mipCount: 0, linear: true);
            var tmpActive = RenderTexture.active;
            RenderTexture.active = rt;
            tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            tex.Apply(updateMipmaps: false, makeNoLongerReadable: false);
            RenderTexture.active = tmpActive;
            RenderTexture.ReleaseTemporary(rt);

            // Copy to Cubemap
            var array = tex.GetPixels(0, 0, tex.width, tex.height);
            var len = array.Length;
            var array2 = new Color[len];
            for (var yIdx = 0; yIdx < tex.height; ++yIdx)
            {
                var startIdx = yIdx * tex.width;
                var endIdx = startIdx + tex.width - 1;
                for (var xIdx = 0; xIdx < tex.width; ++xIdx)
                {
                    // Cubemap が X 軸反転のレガシー仕様をひきずっている、また、Skybox 参照が反対になる、Cubemap と Texture2D で Set/GetPixel の仕様が違う、ので
                    // すべてを考えたときに
                    // データとしては左右反転になるようにする。
                    var srcIdx = (yIdx * tex.width) + xIdx;
                    var dstIdx = ((tex.height - yIdx - 1) * tex.width) + xIdx;

                    array2[dstIdx] = array[srcIdx];
                }
            }
            dst.SetPixels(array2, srcFace, mipmap);

            UnityEngine.Object.Destroy(tex);

            return true;
        }

        private Shader GetImporterShader()
        {
            switch (CompressionType)
            {
                case CubemapCompressionType.Raw:
                    return null;
                case CubemapCompressionType.DoubleLdr:
                    return _importFromDLdrShader;
                case CubemapCompressionType.Rgbm:
                    return _importFromRgbmShader;
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompressionType), CompressionType, null);
            }
        }
    }
}
