using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniGLTF;
using VRM;
using VRMShaders;
using ColorSpace = VRMShaders.ColorSpace;

namespace VCI
{
    public sealed class VciMaterialDescriptorGenerator : IMaterialDescriptorGenerator
    {
        private readonly glTF_VRM_extensions _temporaryConvertedVrmExt = new glTF_VRM_extensions();
        private readonly bool _migrateSrgbColor;

        public VciMaterialDescriptorGenerator(glTF_VCAST_vci_material_unity vciMaterials, bool migrateSrgbColor)
        {
            _temporaryConvertedVrmExt.materialProperties = vciMaterials.materials
                .Select(x => new glTF_VRM_Material
                {
                    name = x.name,
                    shader = x.shader,
                    renderQueue = x.renderQueue,
                    floatProperties = x.floatProperties,
                    vectorProperties = x.vectorProperties,
                    textureProperties = x.textureProperties,
                    keywordMap = x.keywordMap,
                    tagMap = x.tagMap
                })
                .ToList();
            _migrateSrgbColor = migrateSrgbColor;
        }

        public MaterialDescriptor Get(GltfData data, int i)
        {
            // mtoon
            if (VRMMToonMaterialImporter.TryCreateParam(data, _temporaryConvertedVrmExt, i, out MaterialDescriptor matDesc))
            {
                return matDesc;
            }

            // unlit
            if (VciUnlitMaterialImporter.TryCreateParam(data, i, out matDesc, _migrateSrgbColor))
            {
                return matDesc;
            }

            // pbr
            if (VciPbrMaterialImporter.TryCreateParam(data, i, out matDesc, _migrateSrgbColor))
            {
                return matDesc;
            }

            // fallback
            if (VciSymbols.IsDevelopmentEnabled)
            {
                Debug.LogWarning($"material: {i} out of range. fallback");
            }
            return new MaterialDescriptor(
                GltfMaterialDescriptorGenerator.GetMaterialName(i, null),
                GltfPbrMaterialImporter.ShaderName,
                null,
                new Dictionary<string, TextureDescriptor>(),
                new Dictionary<string, float>(),
                new Dictionary<string, Color>(),
                new Dictionary<string, Vector4>(),
                new Action<Material>[]{});
        }
    }
}