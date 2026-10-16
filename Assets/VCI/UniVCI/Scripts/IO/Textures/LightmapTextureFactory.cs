using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UniGLTF;

namespace VCI
{
    public sealed class LightmapTextureFactory : IResponsibilityForDestroyObjects
    {
        private readonly GltfData _data;
        private readonly TextureFactory _textureFactory;
        private readonly IntermediateTextureFactory _intermediateTextureFactory;
        private readonly Shader _importFromDLdrShader;
        private readonly Shader _importFromRgbmShader;
        private readonly Dictionary<SubAssetKey, Texture2D> _convertedLightmapTextureCache = new();
        private bool _disposed;

        public LightmapTextureFactory(GltfData data, TextureFactory textureFactory, IntermediateTextureFactory intermediateTextureFactory)
        {
            _data = data;
            _textureFactory = textureFactory;
            _intermediateTextureFactory = intermediateTextureFactory;
            _importFromDLdrShader = Shader.Find("Hidden/UniVCI/LightmapConversion/ImportFromDLdr");
            _importFromRgbmShader = Shader.Find("Hidden/UniVCI/LightmapConversion/ImportFromRgbm");
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            foreach (var (_, v) in _convertedLightmapTextureCache)
            {
                UnityObjectDestroyer.DestroyRuntimeOrEditor(v);
            }
            _convertedLightmapTextureCache.Clear();
        }

        public void TransferOwnership(TakeResponsibilityForDestroyObjectFunc take)
        {
            foreach (var (k, v) in _convertedLightmapTextureCache.ToArray())
            {
                take(k, v);
                _convertedLightmapTextureCache.Remove(k);
            }
        }

        public async Task<Texture2D> GetOrConvertLightmapTextureAsync(int colorTextureGltfIndex, LightmapCompressionType compressionType, IAwaitCaller awaitCaller)
        {
            // Get TextureDescription
            if (!GltfTextureImporter.TryCreateSrgb(_data, colorTextureGltfIndex, Vector2.zero, Vector2.one, out _, out var param))
            {
                return null;
            }

            // Raw Lightmap
            if (compressionType == LightmapCompressionType.Raw)
            {
                return await GetRawLightmapTextureAsync(param, awaitCaller);
            }

            // Compressed Lightmap
            return await GetConvertedLightmapTextureAsync(param, compressionType, awaitCaller);
        }

        private async Task<Texture2D> GetRawLightmapTextureAsync(TextureDescriptor param, IAwaitCaller awaitCaller)
        {
            // 変換が不要な Lightmap の寿命管理は TextureFactory に委譲する
            var rawTex = await _textureFactory.GetTextureAsync(param, awaitCaller);
            return rawTex as Texture2D;
        }

        private async Task<Texture2D> GetConvertedLightmapTextureAsync(TextureDescriptor param, LightmapCompressionType compressionType, IAwaitCaller awaitCaller)
        {
            var key = param.SubAssetKey;

            // Check cache
            if (_convertedLightmapTextureCache.TryGetValue(key, out var cache)) return cache;

            // Convert to Lightmap
            var convertedLightmapTexture = await ConvertLightmapTexture(param, compressionType, awaitCaller);
            _convertedLightmapTextureCache.Add(key, convertedLightmapTexture);

            return convertedLightmapTexture;
        }

        private async Task<Texture2D> ConvertLightmapTexture(TextureDescriptor param, LightmapCompressionType compressionType, IAwaitCaller awaitCaller)
        {
            var importerShader = GetImporterShader(compressionType);
            if (importerShader == null)
            {
                throw new ArgumentException($"Could not find importer shader for {param.SubAssetKey}");
            }

            var src = await _intermediateTextureFactory.GetTextureAsync(param, awaitCaller);
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
            UnityObjectDestroyer.DestroyRuntimeOrEditor(importerMaterial);

            return dst;
        }

        private Shader GetImporterShader(LightmapCompressionType compressionType)
        {
            switch (compressionType)
            {
                case LightmapCompressionType.Raw:
                    return null;
                case LightmapCompressionType.DoubleLdr:
                    return _importFromDLdrShader;
                case LightmapCompressionType.Rgbm:
                    return _importFromRgbmShader;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compressionType), compressionType, null);
            }
        }
    }
}
