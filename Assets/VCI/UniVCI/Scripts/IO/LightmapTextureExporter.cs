using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniGLTF;
using VRMShaders;

namespace VCI
{
    public enum LightmapCompressionType
    {
        /// <summary>
        /// 生の HDR range Linear 値のテクスチャ
        /// </summary>
        Raw,

        /// <summary>
        /// [0, 2] の範囲を [0, 1] の範囲に圧縮
        /// </summary>
        DoubleLdr,

        /// <summary>
        /// [0, 5^2.2] の範囲をアルファチャンネルを用いて表す
        /// </summary>
        Rgbm,
    }

    public enum LightmapDirectionalType
    {
        Directional,
        NonDirectional,
    }

    /// <summary>
    /// ライトマップ用のテクスチャを変換して iTextureExporter に渡して登録する。
    /// </summary>
    public sealed class LightmapTextureExporter
    {
        private readonly TextureExporter _exporter;
        private readonly glTF _gltf;
        private readonly Shader _exportAsDLdrShader;
        private readonly Shader _exportAsRgbmShader;
        private readonly Dictionary<int, int> _colorTextureMapping = new Dictionary<int, int>();

        /// <summary>
        /// 現在は RGBM 圧縮形式のみサポート
        /// </summary>
        public LightmapCompressionType CompressionType => LightmapCompressionType.Rgbm;

        /// <summary>
        /// 現在は Non-Directional のみサポート
        /// </summary>
        public LightmapDirectionalType DirectionalType => LightmapDirectionalType.NonDirectional;

        /// <summary>
        /// この Exporter で Convert した色テクスチャのリスト
        /// </summary>
        public IEnumerable<int> RegisteredColorTextureIndexArray => _colorTextureMapping.Values;

        public LightmapTextureExporter(TextureExporter exporter, glTF gltf)
        {
            _exporter = exporter;
            _gltf = gltf;
            _exportAsDLdrShader = Shader.Find("Hidden/UniVCI/LightmapConversion/ExportAsDLdr");
            _exportAsRgbmShader = Shader.Find("Hidden/UniVCI/LightmapConversion/ExportAsRgbm");
        }

        /// <summary>
        /// シーンのライトマップインデックスを渡して、それの glTF Texture インデックスを得る
        /// </summary>
        public int GetOrAddColorTexture(int lightmapIndex)
        {
            if (!_colorTextureMapping.ContainsKey(lightmapIndex))
            {
                _colorTextureMapping.Add(lightmapIndex,
                    ConvertAndAddTexture(LightmapSettings.lightmaps[lightmapIndex].lightmapColor));
            }
            return _colorTextureMapping[lightmapIndex];
        }

        public int GetOrAddDirectionalTexture(int lightmapIndex)
        {
            throw new NotImplementedException("現在のところ Non-Directional なライトマップにしか対応していません。");
        }

        private int ConvertAndAddTexture(Texture src)
        {
            if (QualitySettings.activeColorSpace != UnityEngine.ColorSpace.Linear)
            {
                throw new NotSupportedException("ColorSpace の設定は Linear である必要があります。");
            }

            var exporterShader = GetExporterShader();
            if (exporterShader == null) return -1;

            var exporterMaterial = new Material(exporterShader);
            var srgbRt = RenderTexture.GetTemporary(src.width, src.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);

            Graphics.Blit(src, srgbRt, exporterMaterial);
            var idx = _exporter.ExportSRGB(srgbRt);

            RenderTexture.ReleaseTemporary(srgbRt);
            UnityEngine.Object.DestroyImmediate(exporterMaterial);

            return idx;
        }

        private Shader GetExporterShader()
        {
            switch (CompressionType)
            {
                case LightmapCompressionType.Raw:
                    return null;
                case LightmapCompressionType.DoubleLdr:
                    return _exportAsDLdrShader;
                case LightmapCompressionType.Rgbm:
                    return _exportAsRgbmShader;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}