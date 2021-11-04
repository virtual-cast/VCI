using System.Collections.Generic;
using UniGLTF;
using UnityEngine;
using VRM;
using VRMShaders;

namespace VCI
{
    public sealed class VciTextureDescriptorGenerator : ITextureDescriptorGenerator
    {
        private readonly GltfData _data;
        private readonly glTF_VCAST_vci_material_unity _vciMaterial;
        private readonly glTF_VCAST_vci_meta _vciMeta;
        private TextureDescriptorSet _textureDescriptorSet;

        public VciTextureDescriptorGenerator(GltfData data, glTF_VCAST_vci_material_unity vciMaterial, glTF_VCAST_vci_meta vciMeta)
        {
            _data = data;
            _vciMaterial = vciMaterial;
            _vciMeta = vciMeta;
        }

        public TextureDescriptorSet Get()
        {
            if (_textureDescriptorSet == null)
            {
                _textureDescriptorSet = new TextureDescriptorSet();
                foreach (var (_, param) in EnumerateAllTextures(_data, _vciMaterial, _vciMeta))
                {
                    _textureDescriptorSet.Add(param);
                }
            }
            return _textureDescriptorSet;
        }

        public static IEnumerable<(SubAssetKey, TextureDescriptor)> EnumerateAllTextures(
            GltfData data,
            glTF_VCAST_vci_material_unity materialExt,
            glTF_VCAST_vci_meta metaExt)
        {
            // Materials
            for (var materialIdx = 0; materialIdx < data.GLTF.materials.Count; ++materialIdx)
            {
                var material = data.GLTF.materials[materialIdx];
                var vciMaterial = materialExt.materials[materialIdx];

                if (vciMaterial.shader == VciMaterialJsonObject.VRM_USE_GLTFSHADER)
                {
                    // Unlit or PBR
                    foreach (var kv in GltfPbrTextureImporter.EnumerateAllTextures(data, materialIdx))
                    {
                        yield return kv;
                    }
                }
                else
                {
                    // MToon など任意の shader
                    foreach (var kv in VCIMToonTextureImporter.EnumerateAllTextures(data, vciMaterial, materialIdx))
                    {
                        yield return kv;
                    }
                }
            }

            // Thumbnails
            if (TryGetThumbnailTexture(data, metaExt, out var thumbnail))
            {
                yield return thumbnail;
            }
        }

        public static bool TryGetThumbnailTexture(GltfData data, glTF_VCAST_vci_meta meta, out (SubAssetKey, TextureDescriptor) texture)
        {
            if (meta.thumbnail > -1)
            {
                texture = GltfTextureImporter.CreateSRGB(data, meta.thumbnail, Vector2.zero, Vector2.one);
                return true;
            }

            texture = default;
            return false;
        }
    }
}