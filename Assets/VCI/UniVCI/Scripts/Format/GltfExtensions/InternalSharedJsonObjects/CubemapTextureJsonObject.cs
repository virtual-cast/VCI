using System;

namespace VCI
{
    [Serializable]
    public sealed class CubemapTextureJsonObject
    {
        public const string CubemapCompressionModeRaw = "RAW";
        public const string CubemapCompressionModeDoubleLdr = "DLDR";
        public const string CubemapCompressionModeRgbm = "RGBM";

        /// <summary>
        /// このキューブマップに含まれる gltfCubemapFaceTexture の圧縮形式
        /// </summary>
        public string compressionMode = CubemapCompressionModeRaw;

        /// <summary>
        /// mipmapTextures の個数。オリジナルの texture は含めない。
        /// </summary>
        public int mipmapCount = 0;

        /// <summary>
        /// オリジナルのテクスチャ。
        /// </summary>
        public CubemapFaceTextureSetJsonObject texture;

        /// <summary>
        /// ミップマップのテクスチャ群。個数は mipmapCount
        /// </summary>
        public CubemapFaceTextureSetJsonObject[] mipmapTextures;

        public CubemapCompressionType GetSkyboxCompressionModeAsEnum()
        {
            switch (compressionMode)
            {
                case CubemapCompressionModeRaw:
                    return CubemapCompressionType.Raw;
                case CubemapCompressionModeDoubleLdr:
                    return CubemapCompressionType.DoubleLdr;
                case CubemapCompressionModeRgbm:
                    return CubemapCompressionType.Rgbm;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compressionMode), compressionMode, null);
            }
        }

        public static string ConvertCubemapCompressionMode(CubemapCompressionType type)
        {
            switch (type)
            {
                case CubemapCompressionType.Raw:
                    return CubemapCompressionModeRaw;
                case CubemapCompressionType.DoubleLdr:
                    return CubemapCompressionModeDoubleLdr;
                case CubemapCompressionType.Rgbm:
                    return CubemapCompressionModeRgbm;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
