using System;
using System.Collections;
using UnityEngine;
using UniGLTF;
using UniGLTF.Legacy;

namespace VCI
{
    public sealed class CubemapTextureImporterResult
    {
        public Cubemap Result { get; set; }
    }

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

        public IEnumerator ConvertCubemapCoroutine(glTFCubemapTexture gltfCubemapTexture, CubemapTextureImporterResult result)
        {
            var success = true;
            var mipLength = gltfCubemapTexture.mipmapCount; // original を含めない mipmap だけの個数
            var width = _textureGetter(gltfCubemapTexture.texture.cubemapPositiveX.index).Texture.width;

            var cubemap = new Cubemap(width, TextureFormat.RGBAHalf, mipLength + 1);

            // Set original
            var successResult = new CoroutineSuccessResult();
            yield return UpdateCubemapFace(gltfCubemapTexture.texture, cubemap, 0, successResult);
            success &= successResult.Result;

            // Set mipmaps
            for (var idx = 0; idx < mipLength; ++idx)
            {
                var tex = gltfCubemapTexture.mipmapTextures[idx];
                var mipValue = idx + 1;
                var mipmapSuccessResult = new CoroutineSuccessResult();
                yield return UpdateCubemapFace(tex, cubemap, mipValue, mipmapSuccessResult);
                success &= mipmapSuccessResult.Result;
            }

            // Apply
            cubemap.Apply(updateMipmaps: false, makeNoLongerReadable: true);
            yield return null;

            if (success)
            {
                result.Result = cubemap;
            }
            else
            {
                UnityEngine.Object.DestroyImmediate(cubemap);
            }
        }

        private IEnumerator UpdateCubemapFace(glTFCubemapFaceTextureSet src, Cubemap dst, int mipmap, CoroutineSuccessResult result)
        {
            var positiveX = _textureGetter(src.cubemapPositiveX.index).Texture;
            var negativeX = _textureGetter(src.cubemapNegativeX.index).Texture;
            var positiveY = _textureGetter(src.cubemapPositiveY.index).Texture;
            var negativeY = _textureGetter(src.cubemapNegativeY.index).Texture;
            var positiveZ = _textureGetter(src.cubemapPositiveZ.index).Texture;
            var negativeZ = _textureGetter(src.cubemapNegativeZ.index).Texture;

            result.Result = false;
            var faceResult = new CoroutineSuccessResult();

            yield return RenderCubemapFaceTextureCoroutine(positiveX, dst, GltfCubemapFace.PositiveX.ConvertToUnityCubemapFace(), mipmap, faceResult);
            if (!faceResult.Result) yield break;
            yield return RenderCubemapFaceTextureCoroutine(negativeX, dst, GltfCubemapFace.NegativeX.ConvertToUnityCubemapFace(), mipmap, faceResult);
            if (!faceResult.Result) yield break;
            yield return RenderCubemapFaceTextureCoroutine(positiveY, dst, GltfCubemapFace.PositiveY.ConvertToUnityCubemapFace(), mipmap, faceResult);
            if (!faceResult.Result) yield break;
            yield return RenderCubemapFaceTextureCoroutine(negativeY, dst, GltfCubemapFace.NegativeY.ConvertToUnityCubemapFace(), mipmap, faceResult);
            if (!faceResult.Result) yield break;
            yield return RenderCubemapFaceTextureCoroutine(positiveZ, dst, GltfCubemapFace.PositiveZ.ConvertToUnityCubemapFace(), mipmap, faceResult);
            if (!faceResult.Result) yield break;
            yield return RenderCubemapFaceTextureCoroutine(negativeZ, dst, GltfCubemapFace.NegativeZ.ConvertToUnityCubemapFace(), mipmap, faceResult);
            if (!faceResult.Result) yield break;

            result.Result = true;
        }

        private IEnumerator RenderCubemapFaceTextureCoroutine(Texture2D src, Cubemap dst, CubemapFace srcFace, int mipmap, CoroutineSuccessResult result)
        {
            result.Result = false;

            var shader = GetImporterShader();
            if (shader == null) yield break;
            var importerMaterial = new Material(shader);

            if (!Mathf.IsPowerOfTwo(dst.width)) yield break;
            if (!Mathf.IsPowerOfTwo(dst.height)) yield break;

            var originalWidth = dst.width;
            var originalHeight = dst.height;
            var width = Math.Max(2, originalWidth >> mipmap);
            var height = Math.Max(2, originalHeight >> mipmap);

            var rt = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.DefaultHDR, RenderTextureReadWrite.Linear);
            // Decode into Linear RenderTexture
            Graphics.Blit(src, rt, importerMaterial);
            UnityEngine.Object.DestroyImmediate(importerMaterial);
            yield return null;

            // Copy to Linear Texture2D
            var tex = new Texture2D(rt.width, rt.height, TextureFormat.RGBAHalf, mipCount: 0, linear: true);
            var tmpActive = RenderTexture.active;
            RenderTexture.active = rt;
            tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            tex.Apply(updateMipmaps: false, makeNoLongerReadable: false);
            RenderTexture.active = tmpActive;
            RenderTexture.ReleaseTemporary(rt);
            yield return null;

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
            yield return null;

            UnityEngine.Object.Destroy(tex);

            result.Result = true;
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

        public sealed class CoroutineSuccessResult
        {
            public bool Result { get; set; }
        }
    }
}
