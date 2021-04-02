using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
#endif


namespace UniGLTF.Legacy
{

    public static class TextureIO
    {
        public static RenderTextureReadWrite GetColorSpace(glTFTextureTypes textureType)
        {
            switch (textureType)
            {
                case glTFTextureTypes.Metallic:
                case glTFTextureTypes.Occlusion:
                case glTFTextureTypes.Normal:
                    return RenderTextureReadWrite.Linear;
                case glTFTextureTypes.BaseColor:
                case glTFTextureTypes.Emissive:
                case glTFTextureTypes.Unknown:
                    return RenderTextureReadWrite.sRGB;
                default:
                    return RenderTextureReadWrite.sRGB;
            }
        }

        public static glTFTextureTypes GetglTFTextureType(string shaderName, string propName)
        {
            switch (propName)
            {
                case "_Color":
                    return glTFTextureTypes.BaseColor;
                case "_MetallicGlossMap":
                    return glTFTextureTypes.Metallic;
                case "_BumpMap":
                    return glTFTextureTypes.Normal;
                case "_OcclusionMap":
                    return glTFTextureTypes.Occlusion;
                case "_EmissionMap":
                    return glTFTextureTypes.Emissive;
                default:
                    return glTFTextureTypes.Unknown;
            }
        }

        public static glTFTextureTypes GetglTFTextureType(glTF glTf, int textureIndex)
        {
            foreach (var material in glTf.materials)
            {
                var textureInfo = material.GetTextures().FirstOrDefault(x => (x != null) && x.index == textureIndex);
                if (textureInfo != null)
                {
                    return textureInfo.TextureType;
                }
            }
            return glTFTextureTypes.Unknown;
        }

#if UNITY_EDITOR
        public static void MarkTextureAssetAsNormalMap(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath))
            {
                return;
            }

            var textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (null == textureImporter)
            {
                return;
            }

            //Debug.LogFormat("[MarkTextureAssetAsNormalMap] {0}", assetPath);
            textureImporter.textureType = TextureImporterType.NormalMap;
            textureImporter.SaveAndReimport();
        }
#endif
    }
}
