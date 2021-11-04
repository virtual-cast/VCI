using System.Collections.Generic;
using UniGLTF;
using VRM;
using VRMShaders;

namespace VCI
{
    public static class VCIMToonTextureImporter
    {
        public static IEnumerable<(SubAssetKey, TextureDescriptor)> EnumerateAllTextures(GltfData data, VciMaterialJsonObject vciMaterial, int materialIdx)
        {
            foreach (var kv in vciMaterial.textureProperties)
            {
                var vrmMaterial = new glTF_VRM_Material
                {
                    name = vciMaterial.name,
                    shader = vciMaterial.shader,
                    textureProperties = vciMaterial.textureProperties,
                    vectorProperties = vciMaterial.vectorProperties,
                    floatProperties = vciMaterial.floatProperties,
                    keywordMap = vciMaterial.keywordMap,
                    renderQueue = vciMaterial.renderQueue,
                    tagMap = vciMaterial.tagMap,
                };

                if (VRMMToonTextureImporter.TryGetTextureFromMaterialProperty(data, vrmMaterial, kv.Key, out var texture))
                {
                    yield return texture;
                }
            }
        }
    }
}