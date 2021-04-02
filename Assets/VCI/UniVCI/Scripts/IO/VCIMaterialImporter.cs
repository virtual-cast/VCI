using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniGLTF;
using UniGLTF.Legacy;

namespace VCI
{
    public class VCIMaterialImporter : MaterialImporter
    {
        private List<glTF_VCI_Material> m_materials;

        private bool m_srgbToLinearColor = false;

        public VCIMaterialImporter(LegacyImporterContext context, List<glTF_VCI_Material> materials, bool srgbToLinearColor) : base(
            new ShaderStore(), (int index) => context.GetTexture(index))
        {
            m_materials = materials;
            m_srgbToLinearColor = srgbToLinearColor;
        }

        private static string[] VRM_SHADER_NAMES =
        {
            "Standard",
            "VRM/MToon",
            "UniGLTF/UniUnlit",

            "VRM/UnlitTexture",
            "VRM/UnlitCutout",
            "VRM/UnlitTransparent",
            "VRM/UnlitTransparentZWrite",
        };

        public override Material CreateMaterial(int i, glTFMaterial src,  bool hasVertexColor)
        {

            // UniVCI v0.27以下のバージョンでExportしたVCIは、baseColorFactorがSrgbで値が入っているためLinearに変換する必要がある
            if(m_srgbToLinearColor 
                && src != null
                && src.pbrMetallicRoughness != null 
                && src.pbrMetallicRoughness.baseColorFactor != null
                && src.pbrMetallicRoughness.baseColorFactor.Length == 4)
            {
                var color = src.pbrMetallicRoughness.baseColorFactor;
                Color linearColor = (new Color(color[0], color[1], color[2], color[3])).linear;
                src.pbrMetallicRoughness.baseColorFactor[0] = linearColor[0];
                src.pbrMetallicRoughness.baseColorFactor[1] = linearColor[1];
                src.pbrMetallicRoughness.baseColorFactor[2] = linearColor[2];
                src.pbrMetallicRoughness.baseColorFactor[3] = linearColor[3];
            }

            if (i == 0 && m_materials.Count == 0)
            {
                return base.CreateMaterial(i, src, hasVertexColor);
            }

            var item = m_materials[i];
            var shaderName = item.shader;
            var shader = Shader.Find(shaderName);
            if (shader == null)
            {
                //
                // no shader
                //
                if (VRM_SHADER_NAMES.Contains(shaderName))
                {
                    Debug.LogErrorFormat(
                        "shader {0} not found. set Assets/VRM/Shaders/VRMShaders to Edit - project setting - Graphics - preloaded shaders",
                        shaderName);
                }
                else
                {
                    Debug.LogFormat("unknown shader {0}.", shaderName);
                }

                var standardMaterial = base.CreateMaterial(i, src, hasVertexColor);

                
                // VCAST_materials_pbr拡張が存在する場合はパラメータを上書きする
                if (src.extensions != null)
                {
                    glTF_VCAST_materials_pbr extensions;
                    if(src.extensions.TryDeserializeExtensions(glTF_VCAST_materials_pbr.ExtensionName, glTF_VCAST_materials_pbr_Deserializer.Deserialize, out extensions))
                    {
                        // emissiveFactor
                        var emissiveFactor = extensions.emissiveFactor;
                        if (emissiveFactor != null && emissiveFactor.Length == 3)
                        {
                            standardMaterial.SetColor("_EmissionColor", new Color(emissiveFactor[0], emissiveFactor[1], emissiveFactor[2]));
                        }
                    }
                }

                return standardMaterial;
            }

            //
            // restore VRM material
            //
            var material = new Material(shader);
            material.name = item.name;
            material.renderQueue = item.renderQueue;

            foreach (var kv in item.floatProperties)
            {
                material.SetFloat(kv.Key, kv.Value);
            }

            foreach (var kv in item.vectorProperties)
            {
                if (item.textureProperties.ContainsKey(kv.Key))
                {
                    // texture offset & scale
                    material.SetTextureOffset(kv.Key, new Vector2(kv.Value[0], kv.Value[1]));
                    material.SetTextureScale(kv.Key, new Vector2(kv.Value[2], kv.Value[3]));
                }
                else
                {
                    // vector4
                    var v = new Vector4(kv.Value[0], kv.Value[1], kv.Value[2], kv.Value[3]);
                    material.SetVector(kv.Key, v);
                }
            }

            foreach (var kv in item.textureProperties)
            {
                var texture = GetTextureFunc(kv.Value);
                if (texture != null)
                {
                    var converted = texture.ConvertTexture(kv.Key);
                    if (converted != null)
                    {
                        material.SetTexture(kv.Key, converted);
                    }
                    else
                    {
                        material.SetTexture(kv.Key, texture.Texture);
                    }
                }
            }

            foreach (var kv in item.keywordMap)
            {
                if (kv.Value)
                {
                    material.EnableKeyword(kv.Key);
                }
                else
                {
                    material.DisableKeyword(kv.Key);
                }
            }

            foreach (var kv in item.tagMap)
            {
                material.SetOverrideTag(kv.Key, kv.Value);
            }

            return material;
        }
    }
}