using System;

namespace VCI
{
    [Serializable]
    public sealed class LocationLightingJsonObject
    {
        public const string LightmapCompressionModeRaw = "RAW";
        public const string LightmapCompressionModeDoubleLdr = "DLDR";
        public const string LightmapCompressionModeRgbm = "RGBM";
        public const string LightmapDirectionalModeDirectional = "DIRECTIONAL";
        public const string LightmapDirectionalModeNonDirectional = "NONDIRECTIONAL";

        /// <summary>
        /// このロケーション VCI のライトマップ焼き込み方式
        /// </summary>
        public string lightmapDirectionalMode = LightmapDirectionalModeNonDirectional;

        /// <summary>
        /// このロケーション VCI のライトマップのデータの圧縮形式
        /// </summary>
        public string lightmapCompressionMode = LightmapCompressionModeRaw;

        /// <summary>
        /// このロケーション VCI の参照するライトマップテクスチャ群
        /// </summary>
        public LightmapTextureInfoJsonObject[] lightmapTextures = new LightmapTextureInfoJsonObject[0];

        /// <summary>
        /// このロケーション VCI のスカイボックスのキューブマップ
        /// </summary>
        public CubemapTextureJsonObject skyboxCubemap = new CubemapTextureJsonObject();

        /// <summary>
        /// このロケーション VCI の LightProbe 集合
        /// </summary>
        public LightProbeJsonObject[] lightProbes;

        public LightmapCompressionType GetLightmapCompressionModeAsEnum()
        {
            switch (lightmapCompressionMode)
            {
                case LightmapCompressionModeRaw:
                    return LightmapCompressionType.Raw;
                case LightmapCompressionModeDoubleLdr:
                    return LightmapCompressionType.DoubleLdr;
                case LightmapCompressionModeRgbm:
                    return LightmapCompressionType.Rgbm;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lightmapCompressionMode), lightmapCompressionMode, null);
            }
        }

        public LightmapDirectionalType GetLightmapDirectionalModeAsEnum()
        {
            switch (lightmapDirectionalMode)
            {
                case LightmapDirectionalModeDirectional:
                    return LightmapDirectionalType.Directional;
                case LightmapDirectionalModeNonDirectional:
                    return LightmapDirectionalType.NonDirectional;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lightmapDirectionalMode), lightmapDirectionalMode, null);
            }
        }

        public static string ConvertLightmapCompressionMode(LightmapCompressionType type)
        {
            switch (type)
            {
                case LightmapCompressionType.Raw:
                    return LightmapCompressionModeRaw;
                case LightmapCompressionType.DoubleLdr:
                    return LightmapCompressionModeDoubleLdr;
                case LightmapCompressionType.Rgbm:
                    return LightmapCompressionModeRgbm;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static string ConvertLightmapDirectionalMode(LightmapDirectionalType type)
        {
            switch (type)
            {
                case LightmapDirectionalType.Directional:
                    return LightmapDirectionalModeDirectional;
                case LightmapDirectionalType.NonDirectional:
                    return LightmapDirectionalModeNonDirectional;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
