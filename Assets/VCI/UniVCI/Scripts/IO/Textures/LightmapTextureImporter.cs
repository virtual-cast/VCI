using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UniGLTF;
using VRMShaders;

namespace VCI
{
    public sealed class LightmapTextureImporter
    {
        private readonly GltfData _data;
        private readonly TextureFactory _textureFactory;
        private readonly Shader _importFromDLdrShader;
        private readonly Shader _importFromRgbmShader;
        private readonly Dictionary<int, Texture2D> _lightmapTextureCache = new Dictionary<int, Texture2D>();

        public LightmapCompressionType CompressionType { get; }
        public LightmapDirectionalType DirectionalType { get; }

        public LightmapTextureImporter(GltfData data, TextureFactory textureFactory,LightmapCompressionType compressionType, LightmapDirectionalType directionalType)
        {
            _data = data;
            _textureFactory = textureFactory;
            CompressionType = compressionType;
            DirectionalType = directionalType;
            _importFromDLdrShader = Shader.Find("Hidden/UniVCI/LightmapConversion/ImportFromDLdr");
            _importFromRgbmShader = Shader.Find("Hidden/UniVCI/LightmapConversion/ImportFromRgbm");
        }

        public async Task<Texture2D> GetOrConvertLightmapTextureAsync(int colorTextureGltfIndex, IAwaitCaller awaitCaller)
        {
            // FIXME: GLTF の TextureFactory でライトマップテクスチャを生成できるようにする
            // FIXME: 自前でキャッシュを持つべきではない
            if (!_lightmapTextureCache.ContainsKey(colorTextureGltfIndex))
            {
                var (key, param) =
                    GltfTextureImporter.CreateSRGB(_data, colorTextureGltfIndex, Vector2.zero, Vector2.one);
                var colorTexture = await _textureFactory.GetTextureAsync(param, awaitCaller);

                var lightmapTexture = await ConvertLightmapTexture(colorTexture, awaitCaller);

                _lightmapTextureCache.Add(colorTextureGltfIndex, lightmapTexture);
            }

            return _lightmapTextureCache[colorTextureGltfIndex];
        }

        private async Task<Texture2D> ConvertLightmapTexture(Texture src, IAwaitCaller awaitCaller)
        {
            var importerShader = GetImporterShader();
            if (importerShader == null)
            {
                return default;
            }

            var importerMaterial = new Material(importerShader);
            var rt = RenderTexture.GetTemporary(src.width, src.height, 0, RenderTextureFormat.DefaultHDR, RenderTextureReadWrite.Linear);

            // Decode into Linear RenderTexture
            Graphics.Blit(src, rt, importerMaterial);
            await awaitCaller.NextFrame();

            // Create Linear Texture2D with settings
            var dst = new Texture2D(rt.width, rt.height, TextureFormat.RGBAHalf, mipCount: src.mipmapCount, linear: true);
            dst.name = $"{src.name}_decoded";
            dst.mipMapBias = src.mipMapBias;
            dst.wrapMode = src.wrapMode;
            dst.wrapModeU = src.wrapModeU;
            dst.wrapModeV = src.wrapModeV;
            dst.wrapModeW = src.wrapModeW;
            dst.filterMode = src.filterMode;

            // Copy to Linear Texture2D
            var tmpActive = RenderTexture.active;
            RenderTexture.active = rt;
            dst.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            dst.Apply(updateMipmaps: true, makeNoLongerReadable: true);
            RenderTexture.active = tmpActive;
            await awaitCaller.NextFrame();

            RenderTexture.ReleaseTemporary(rt);
            UnityEngine.Object.DestroyImmediate(importerMaterial);

            return dst;
        }

        private Shader GetImporterShader()
        {
            switch (CompressionType)
            {
                case LightmapCompressionType.Raw:
                    return null;
                case LightmapCompressionType.DoubleLdr:
                    return _importFromDLdrShader;
                case LightmapCompressionType.Rgbm:
                    return _importFromRgbmShader;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}