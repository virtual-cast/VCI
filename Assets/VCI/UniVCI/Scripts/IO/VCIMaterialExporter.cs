using System;
using UnityEngine;
using VCIGLTF;

namespace VCI
{
    public class VCIMaterialExporter : MaterialExporter
    {
        public override glTFMaterial ExportMaterial(Material m, TextureExportManager textureManager)
        {
            var material = base.ExportMaterial(m, textureManager);

            ExportMaterialExtension(m, material);

            return material;
        }

        static void ExportMaterialExtension(Material m, glTFMaterial material)
        {
            // HDR Emission
            if (m.IsKeywordEnabled("_EMISSION") && m.HasProperty("_EmissionColor"))
            {
                var color = m.GetColor("_EmissionColor");
                if (color.maxColorComponent > 1)
                {
                    if(material.extensions == null)
                    {
                        material.extensions = new glTFMaterial_extensions();
                    }

                    if(material.extensions.VCAST_materials_pbr == null)
                    {
                        material.extensions.VCAST_materials_pbr = new glTF_VCAST_materials_pbr();
                    }

                    material.extensions.VCAST_materials_pbr.emissiveFactor = new float[] { color.r, color.g, color.b };
                }
            }
        }

        protected override glTFMaterial CreateMaterial(Material m)
        {
            switch (m.shader.name)
            {
                case "VRM/UnlitTexture":
                    return Export_VRMUnlitTexture(m);

                case "VRM/UnlitTransparent":
                    return Export_VRMUnlitTransparent(m);

                case "VRM/UnlitCutout":
                    return Export_VRMUnlitCutout(m);

                case "VRM/UnlitTransparentZWrite":
                    return Export_VRMUnlitTransparentZWrite(m);

                case "VRM/MToon":
                    return Export_VRMMToon(m);

                default:
                    return base.CreateMaterial(m);
            }
        }

        private static glTFMaterial Export_VRMUnlitTexture(Material m)
        {
            var material = glTF_KHR_materials_unlit.CreateDefault();
            material.alphaMode = "OPAQUE";
            return material;
        }

        private static glTFMaterial Export_VRMUnlitTransparent(Material m)
        {
            var material = glTF_KHR_materials_unlit.CreateDefault();
            material.alphaMode = "BLEND";
            return material;
        }

        private static glTFMaterial Export_VRMUnlitCutout(Material m)
        {
            var material = glTF_KHR_materials_unlit.CreateDefault();
            material.alphaMode = "MASK";
            return material;
        }

        private static glTFMaterial Export_VRMUnlitTransparentZWrite(Material m)
        {
            var material = glTF_KHR_materials_unlit.CreateDefault();
            material.alphaMode = "BLEND";
            return material;
        }

        private static glTFMaterial Export_VRMMToon(Material m)
        {
            var material = glTF_KHR_materials_unlit.CreateDefault();

            switch (m.GetTag("RenderType", true))
            {
                case "Transparent":
                    material.alphaMode = "BLEND";
                    break;

                case "TransparentCutout":
                    material.alphaMode = "MASK";
                    material.alphaCutoff = m.GetFloat("_Cutoff");
                    break;

                default:
                    material.alphaMode = "OPAQUE";
                    break;
            }

            switch ((int) m.GetFloat("_CullMode"))
            {
                case 0:
                    material.doubleSided = true;
                    break;

                case 1:
                    Debug.LogWarning("ignore cull front");
                    break;

                case 2:
                    // cull back
                    break;

                default:
                    throw new NotImplementedException();
            }

            return material;
        }
    }
}