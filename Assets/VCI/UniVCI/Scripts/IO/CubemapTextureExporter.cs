using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UniGLTF;
using VRMShaders;

namespace VCI
{
    public enum CubemapCompressionType
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

    public sealed class CubemapTextureExporter
    {
        private static readonly int ShaderPropertyFaceIndex = Shader.PropertyToID("_FaceIndex");
        private static readonly int ShaderPropertyMipValue = Shader.PropertyToID("_MipLevel");

        private readonly TextureExporter _exporter;
        private readonly glTF _gltf;
        private readonly Shader _exportAsDLdrShader;
        private readonly Shader _exportAsRgbmShader;
        private readonly Dictionary<CubemapFaceId, int> _cubemapFaceMapping = new Dictionary<CubemapFaceId, int>();

        /// <summary>
        /// サポート方式はとりあえず固定
        /// </summary>
        public CubemapCompressionType CompressionType => CubemapCompressionType.Rgbm;

        public CubemapTextureExporter(TextureExporter exporter, glTF gltf)
        {
            _exporter = exporter;
            _gltf = gltf;
            _exportAsDLdrShader = Shader.Find("Hidden/UniVCI/CubemapConversion/ExportAsDLdr");
            _exportAsRgbmShader = Shader.Find("Hidden/UniVCI/CubemapConversion/ExportAsRgbm");
        }

        public glTFCubemapTexture Export(Texture cubemap, int width, bool includeMipmaps)
        {
            // Unity の Texture のもつ mipmapCount はオリジナルもカウントする
            var mipmapCount = includeMipmaps ? cubemap.mipmapCount - 1 : 0;

            return new glTFCubemapTexture
            {
                compressionMode = glTFCubemapTexture.ConvertCubemapCompressionMode(CompressionType),
                mipmapCount = mipmapCount,
                texture = new glTFCubemapFaceTextureSet
                {
                    cubemapPositiveX = new glTFCubemapFaceTextureInfo { index = GetOrAddCubemapTexture(cubemap, GltfCubemapFace.PositiveX.ConvertToUnityCubemapFace(), width, 0) },
                    cubemapNegativeX = new glTFCubemapFaceTextureInfo { index = GetOrAddCubemapTexture(cubemap, GltfCubemapFace.NegativeX.ConvertToUnityCubemapFace(), width, 0) },
                    cubemapPositiveY = new glTFCubemapFaceTextureInfo { index = GetOrAddCubemapTexture(cubemap, GltfCubemapFace.PositiveY.ConvertToUnityCubemapFace(), width, 0) },
                    cubemapNegativeY = new glTFCubemapFaceTextureInfo { index = GetOrAddCubemapTexture(cubemap, GltfCubemapFace.NegativeY.ConvertToUnityCubemapFace(), width, 0) },
                    cubemapPositiveZ = new glTFCubemapFaceTextureInfo { index = GetOrAddCubemapTexture(cubemap, GltfCubemapFace.PositiveZ.ConvertToUnityCubemapFace(), width, 0) },
                    cubemapNegativeZ = new glTFCubemapFaceTextureInfo { index = GetOrAddCubemapTexture(cubemap, GltfCubemapFace.NegativeZ.ConvertToUnityCubemapFace(), width, 0) },
                },
                mipmapTextures = Enumerable.Range(1, mipmapCount).Select(mip => new glTFCubemapFaceTextureSet
                {
                    cubemapPositiveX = new glTFCubemapFaceTextureInfo { index = GetOrAddCubemapTexture(cubemap, GltfCubemapFace.PositiveX.ConvertToUnityCubemapFace(), width, mip) },
                    cubemapNegativeX = new glTFCubemapFaceTextureInfo { index = GetOrAddCubemapTexture(cubemap, GltfCubemapFace.NegativeX.ConvertToUnityCubemapFace(), width, mip) },
                    cubemapPositiveY = new glTFCubemapFaceTextureInfo { index = GetOrAddCubemapTexture(cubemap, GltfCubemapFace.PositiveY.ConvertToUnityCubemapFace(), width, mip) },
                    cubemapNegativeY = new glTFCubemapFaceTextureInfo { index = GetOrAddCubemapTexture(cubemap, GltfCubemapFace.NegativeY.ConvertToUnityCubemapFace(), width, mip) },
                    cubemapPositiveZ = new glTFCubemapFaceTextureInfo { index = GetOrAddCubemapTexture(cubemap, GltfCubemapFace.PositiveZ.ConvertToUnityCubemapFace(), width, mip) },
                    cubemapNegativeZ = new glTFCubemapFaceTextureInfo { index = GetOrAddCubemapTexture(cubemap, GltfCubemapFace.NegativeZ.ConvertToUnityCubemapFace(), width, mip) },
                }).ToArray(),
            };
        }

        /// <summary>
        /// Cubemap の１面を抜き出して登録する.
        /// src は Cubemap か、dimension が Cube の RenderTexture でなければならない
        /// </summary>
        private int GetOrAddCubemapTexture(Texture src, CubemapFace face, int originalWidth, int mipmap)
        {
            if (src == null || src.dimension != TextureDimension.Cube || face == CubemapFace.Unknown) return -1;

            var id = new CubemapFaceId(src, face, mipmap);
            if (!_cubemapFaceMapping.ContainsKey(id))
            {
                _cubemapFaceMapping.Add(id, ConvertAndAddCubemapTexture(src, face, originalWidth, mipmap));
            }
            return _cubemapFaceMapping[id];
        }

        private int ConvertAndAddCubemapTexture(Texture src, CubemapFace face, int originalWidth, int mipmap)
        {
            if (QualitySettings.activeColorSpace != UnityEngine.ColorSpace.Linear)
            {
                throw new NotSupportedException("ColorSpace の設定は Linear である必要があります。");
            }

            if (src == null || src.dimension != TextureDimension.Cube)
            {
                throw new ArgumentException(nameof(src));
            }

            originalWidth = Mathf.ClosestPowerOfTwo(originalWidth);
            var width = Math.Max(1, Math.Min(originalWidth, originalWidth >> mipmap));

            var exporterShader = GetExporterShader();
            if (exporterShader == null) return -1;

            var exporterMaterial = new Material(exporterShader);
            var srgbRt = RenderTexture.GetTemporary(width, width, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);

            exporterMaterial.SetInt(ShaderPropertyFaceIndex, GetFaceIndex(face));
            exporterMaterial.SetInt(ShaderPropertyMipValue, mipmap);
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
                case CubemapCompressionType.Raw:
                    return null;
                case CubemapCompressionType.DoubleLdr:
                    return _exportAsDLdrShader;
                case CubemapCompressionType.Rgbm:
                    return _exportAsRgbmShader;
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompressionType), CompressionType, null);
            }
        }

        private static int GetFaceIndex(CubemapFace face)
        {
            switch (face)
            {
                case CubemapFace.Unknown:
                    throw new ArgumentException(face.ToString());
                case CubemapFace.PositiveX:
                    return 0;
                case CubemapFace.NegativeX:
                    return 1;
                case CubemapFace.PositiveY:
                    return 2;
                case CubemapFace.NegativeY:
                    return 3;
                case CubemapFace.PositiveZ:
                    return 4;
                case CubemapFace.NegativeZ:
                    return 5;
                default:
                    throw new ArgumentOutOfRangeException(nameof(face), face, null);
            }
        }

        private readonly struct CubemapFaceId : IEquatable<CubemapFaceId>
        {
            public Texture Texture { get; }
            public CubemapFace Face { get; }
            public int Mipmap { get; }

            public CubemapFaceId(Texture texture, CubemapFace face, int mipmap)
            {
                Texture = texture;
                Face = face;
                Mipmap = mipmap;
            }

            public bool Equals(CubemapFaceId other)
            {
                return Equals(Texture, other.Texture) && Face == other.Face && Mipmap == other.Mipmap;
            }

            public override bool Equals(object obj)
            {
                return obj is CubemapFaceId other && Equals(other);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (Texture != null ? Texture.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (int)Face;
                    hashCode = (hashCode * 397) ^ Mipmap;
                    return hashCode;
                }
            }
        }
    }
}
