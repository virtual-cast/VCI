using System;
using UniGLTF;
using UniGLTF.ShaderPropExporter;
using UnityEngine;
using VRMShaders;
using ColorSpace = VRMShaders.ColorSpace;

namespace VCI
{
    /// <summary>
    /// VCI 拡張に書き込むマテリアル情報のエクスポート。
    /// 現状 MToon のみを対象とする。
    /// </summary>
    public static class BuiltInVciExtensionMaterialPropertyExporter
    {
        private static readonly string[] TAGS = new string[]
        {
            "RenderType",
            // "Queue",
        };

        public static VciMaterialJsonObject ExportMaterial(Material m, ITextureExporter textureExporter)
        {
            var material = new VciMaterialJsonObject
            {
                name = m.name,
                shader = m.shader.name,
                renderQueue = m.renderQueue,
            };

            if (m.shader.name != MToon.Utils.ShaderName)
            {
                material.shader = VciMaterialJsonObject.VRM_USE_GLTFSHADER;
                return material;
            }

            var prop = PreShaderPropExporter.GetPropsForMToon();
            if (prop == null)
            {
                // ありえない
                throw new MaterialPropertyExporterException();
            }
            else
            {
                foreach (var keyword in m.shaderKeywords)
                {
                    material.keywordMap.Add(keyword, m.IsKeywordEnabled(keyword));
                }

                // get properties
                //material.SetProp(prop);
                foreach (var kv in prop.Properties)
                {
                    switch (kv.ShaderPropertyType)
                    {
                        case ShaderPropertyType.Color:
                            {
                                // No color conversion. Because color property is serialized to raw float array.
                                var value = m.GetColor(kv.Key).ToFloat4(ColorSpace.Linear, ColorSpace.Linear);
                                material.vectorProperties.Add(kv.Key, value);
                            }
                            break;

                        case ShaderPropertyType.Range:
                        case ShaderPropertyType.Float:
                            {
                                var value = m.GetFloat(kv.Key);
                                material.floatProperties.Add(kv.Key, value);
                            }
                            break;

                        case ShaderPropertyType.TexEnv:
                            {
                                var texture = m.GetTexture(kv.Key);
                                if (texture != null)
                                {
                                    var value = kv.Key == "_BumpMap"
                                            ? textureExporter.RegisterExportingAsNormal(texture)
                                            : textureExporter.RegisterExportingAsSRgb(texture, kv.Key == "_MainTex")
                                        ;
                                    if (value == -1)
                                    {
                                        Debug.LogFormat("not found {0}", texture.name);
                                    }
                                    else
                                    {
                                        material.textureProperties.Add(kv.Key, value);
                                    }
                                }

                                // offset & scaling
                                var offset = m.GetTextureOffset(kv.Key);
                                var scaling = m.GetTextureScale(kv.Key);
                                material.vectorProperties.Add(kv.Key,
                                    new float[] { offset.x, offset.y, scaling.x, scaling.y });
                            }
                            break;

                        case ShaderPropertyType.Vector:
                            {
                                var value = m.GetVector(kv.Key).ToArray();
                                material.vectorProperties.Add(kv.Key, value);
                            }
                            break;

                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            foreach (var tag in TAGS)
            {
                var value = m.GetTag(tag, false);
                if (!String.IsNullOrEmpty(value))
                {
                    material.tagMap.Add(tag, value);
                }
            }

            return material;
        }
    }
}
