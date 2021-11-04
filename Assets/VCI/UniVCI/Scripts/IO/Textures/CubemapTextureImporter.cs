using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UniGLTF;
using VRMShaders;

namespace VCI
{
    public sealed class CubemapTextureImporter
    {
        private readonly GltfData _data;
        private readonly TextureFactory _textureFactory;
        private readonly Shader _importFromDLdrShader;
        private readonly Shader _importFromRgbmShader;
        private readonly Dictionary<int, Cubemap> _cubemapTextureCache = new Dictionary<int, Cubemap>();

        public CubemapCompressionType CompressionType { get; }

        public CubemapTextureImporter(GltfData data, TextureFactory textureFactory, CubemapCompressionType compressionType)
        {
            _data = data;
            _textureFactory = textureFactory;
            CompressionType = compressionType;
            _importFromDLdrShader = Shader.Find("Hidden/UniVCI/CubemapConversion/ImportFromDLdr");
            _importFromRgbmShader = Shader.Find("Hidden/UniVCI/CubemapConversion/ImportFromRgbm");
        }

        public async Task<Cubemap> GetOrConvertCubemapTextureAsync(CubemapTextureJsonObject gltfCubemapTexture, IAwaitCaller awaitCaller)
        {
            // FIXME: GLTF の TextureFactory でライトマップテクスチャを生成できるようにする
            // FIXME: 自前でキャッシュを持つべきではない
            var key = gltfCubemapTexture.texture.cubemapPositiveX.index;
            if (!_cubemapTextureCache.ContainsKey(key))
            {
                var cubemap = await ConvertCubemapAsync(gltfCubemapTexture, awaitCaller);
                _cubemapTextureCache.Add(key, cubemap);
            }

            return _cubemapTextureCache[key];
        }

        public async Task<Cubemap> ConvertCubemapAsync(CubemapTextureJsonObject gltfCubemapTexture, IAwaitCaller awaitCaller)
        {
            var mipLength = gltfCubemapTexture.mipmapCount; // original を含めない mipmap だけの個数

            var (key, param) = GltfTextureImporter.CreateSRGB(_data, gltfCubemapTexture.texture.cubemapPositiveX.index, Vector2.zero, Vector2.one);
            var positiveX = await _textureFactory.GetTextureAsync(param, awaitCaller);
            var width = positiveX.width;

            var cubemap = new Cubemap(width, TextureFormat.RGBAHalf, mipLength + 1);

            // Set original
            var success = await UpdateCubemapFace(gltfCubemapTexture.texture, cubemap, 0, awaitCaller);

            // Set mipmaps
            for (var idx = 0; idx < mipLength; ++idx)
            {
                var tex = gltfCubemapTexture.mipmapTextures[idx];
                var mipValue = idx + 1;
                success &= await UpdateCubemapFace(tex, cubemap, mipValue, awaitCaller);
            }

            // Apply
            cubemap.Apply(updateMipmaps: false, makeNoLongerReadable: true);
            await awaitCaller.NextFrame();

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

        private async Task<bool> UpdateCubemapFace(CubemapFaceTextureSetJsonObject src, Cubemap dst, int mipmap, IAwaitCaller awaitCaller)
        {
            var positiveX = await _textureFactory.GetTextureAsync(
                GltfTextureImporter.CreateSRGB(_data, src.cubemapPositiveX.index, Vector2.zero, Vector2.one).Item2,
                awaitCaller);
            var negativeX = await _textureFactory.GetTextureAsync(
                GltfTextureImporter.CreateSRGB(_data, src.cubemapNegativeX.index, Vector2.zero, Vector2.one).Item2,
                awaitCaller);
            var positiveY = await _textureFactory.GetTextureAsync(
                GltfTextureImporter.CreateSRGB(_data, src.cubemapPositiveY.index, Vector2.zero, Vector2.one).Item2,
                awaitCaller);
            var negativeY = await _textureFactory.GetTextureAsync(
                GltfTextureImporter.CreateSRGB(_data, src.cubemapNegativeY.index, Vector2.zero, Vector2.one).Item2,
                awaitCaller);
            var positiveZ = await _textureFactory.GetTextureAsync(
                GltfTextureImporter.CreateSRGB(_data, src.cubemapPositiveZ.index, Vector2.zero, Vector2.one).Item2,
                awaitCaller);
            var negativeZ = await _textureFactory.GetTextureAsync(
                GltfTextureImporter.CreateSRGB(_data, src.cubemapNegativeZ.index, Vector2.zero, Vector2.one).Item2,
                awaitCaller);

            if (!await RenderCubemapFaceTextureAsync(positiveX, dst, GltfCubemapFace.PositiveX.ConvertToUnityCubemapFace(), mipmap, awaitCaller)) return false;
            if (!await RenderCubemapFaceTextureAsync(negativeX, dst, GltfCubemapFace.NegativeX.ConvertToUnityCubemapFace(), mipmap, awaitCaller)) return false;
            if (!await RenderCubemapFaceTextureAsync(positiveY, dst, GltfCubemapFace.PositiveY.ConvertToUnityCubemapFace(), mipmap, awaitCaller)) return false;
            if (!await RenderCubemapFaceTextureAsync(negativeY, dst, GltfCubemapFace.NegativeY.ConvertToUnityCubemapFace(), mipmap, awaitCaller)) return false;
            if (!await RenderCubemapFaceTextureAsync(positiveZ, dst, GltfCubemapFace.PositiveZ.ConvertToUnityCubemapFace(), mipmap, awaitCaller)) return false;
            if (!await RenderCubemapFaceTextureAsync(negativeZ, dst, GltfCubemapFace.NegativeZ.ConvertToUnityCubemapFace(), mipmap, awaitCaller)) return false;

            return true;
        }

        private async Task<bool> RenderCubemapFaceTextureAsync(Texture src, Cubemap dst, CubemapFace srcFace, int mipmap, IAwaitCaller awaitCaller)
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

            await awaitCaller.NextFrame();

            // Copy to Linear Texture2D
            var tex = new Texture2D(rt.width, rt.height, TextureFormat.RGBAHalf, mipCount: 0, linear: true);
            var tmpActive = RenderTexture.active;
            RenderTexture.active = rt;
            tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            tex.Apply(updateMipmaps: false, makeNoLongerReadable: false);
            RenderTexture.active = tmpActive;
            RenderTexture.ReleaseTemporary(rt);

            await awaitCaller.NextFrame();

            // Copy to Cubemap
            var texWidth = tex.width;
            var texHeight = tex.height;
            var array = tex.GetPixels(0, 0, texWidth, texHeight);

            var convertedArray = await awaitCaller.Run(() =>
            {
                var len = array.Length;
                var array2 = new Color[len];
                for (var yIdx = 0; yIdx < texHeight; ++yIdx)
                {
                    var startIdx = yIdx * texWidth;
                    var endIdx = startIdx + texWidth - 1;
                    for (var xIdx = 0; xIdx < texWidth; ++xIdx)
                    {
                        // Cubemap が X 軸反転のレガシー仕様をひきずっている、また、Skybox 参照が反対になる、Cubemap と Texture2D で Set/GetPixel の仕様が違う、ので
                        // すべてを考えたときに
                        // データとしては左右反転になるようにする。
                        var srcIdx = (yIdx * texWidth) + xIdx;
                        var dstIdx = ((texHeight - yIdx - 1) * texWidth) + xIdx;

                        array2[dstIdx] = array[srcIdx];
                    }
                }
                return array2;
            });

            dst.SetPixels(convertedArray, srcFace, mipmap);
            UnityEngine.Object.Destroy(tex);

            await awaitCaller.NextFrame();

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
