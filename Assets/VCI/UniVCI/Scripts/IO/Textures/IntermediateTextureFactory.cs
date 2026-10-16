using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UniGLTF;

namespace VCI
{
    /// <summary>
    /// 最終成果物 (Lightmap / Cubemap) への変換元として一時的に利用するテクスチャを管理する TextureFactory。
    /// 変換完了後は VCIImporter が Dispose して解放する想定で、最終成果物の所有権は持たない。
    /// </summary>
    public sealed class IntermediateTextureFactory : IDisposable
    {
        private readonly TextureFactory _textureFactory;
        private bool _disposed;

        public IntermediateTextureFactory(ITextureDeserializer textureDeserializer, IReadOnlyDictionary<SubAssetKey, Texture> externalTextures = null, bool isLegacySquaredRoughness = false)
        {
            _textureFactory = new TextureFactory(textureDeserializer, externalTextures, isLegacySquaredRoughness);
        }

        /// <summary>
        /// テクスチャ生成情報を基に、テクスチャ生成を行う。
        /// SubAssetKey が同じ場合はキャッシュを返す。
        /// </summary>
        public async Task<Texture> GetTextureAsync(TextureDescriptor texDesc, IAwaitCaller awaitCaller) => await _textureFactory.GetTextureAsync(texDesc, awaitCaller);

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _textureFactory?.Dispose();
        }
    }
}
