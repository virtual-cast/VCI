using System;
using System.Collections;
using UnityEngine;
using UniGLTF.Legacy;

namespace VCI
{
    public sealed class LightmapTextureImporterResult
    {
        public Texture2D Result { get; set; }
    }

    public sealed class LightmapTextureImporter
    {
        private const string ConvertedTexKey = "LightmapLinearTexture";

        private readonly Func<int, TextureItem> _textureGetter;
        private readonly Shader _importFromDLdrShader;
        private readonly Shader _importFromRgbmShader;

        public LightmapCompressionType CompressionType { get; }
        public LightmapDirectionalType DirectionalType { get; }

        public LightmapTextureImporter(Func<int, TextureItem> textureGetter, LightmapCompressionType compressionType, LightmapDirectionalType directionalType)
        {
            _textureGetter = textureGetter;
            CompressionType = compressionType;
            DirectionalType = directionalType;
            _importFromDLdrShader = Shader.Find("Hidden/UniVCI/LightmapConversion/ImportFromDLdr");
            _importFromRgbmShader = Shader.Find("Hidden/UniVCI/LightmapConversion/ImportFromRgbm");
        }

        public IEnumerator GetOrConvertColorTextureCoroutine(int colorTextureGltfIndex, LightmapTextureImporterResult result)
        {
            var textureItem = _textureGetter.Invoke(colorTextureGltfIndex);

            if (textureItem.Converts.ContainsKey(ConvertedTexKey))
            {
                result.Result = textureItem.Converts[ConvertedTexKey];
                yield break;
            }

            yield return ConvertLightmapTexture(textureItem.Texture, result);

            if (result.Result == null)
            {
                yield break;
            }

            textureItem.Converts.Add("LightmapLinearTexture", result.Result);
        }

        private IEnumerator ConvertLightmapTexture(Texture2D src, LightmapTextureImporterResult result)
        {
            var importerShader = GetImporterShader();
            if (importerShader == null)
            {
                result.Result = null;
                yield break;
            }

            var importerMaterial = new Material(importerShader);
            var rt = RenderTexture.GetTemporary(src.width, src.height, 0, RenderTextureFormat.DefaultHDR, RenderTextureReadWrite.Linear);

            // Decode into Linear RenderTexture
            Graphics.Blit(src, rt, importerMaterial);
            yield return null;

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
            yield return null;

            RenderTexture.ReleaseTemporary(rt);
            UnityEngine.Object.DestroyImmediate(importerMaterial);

            result.Result = dst;
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