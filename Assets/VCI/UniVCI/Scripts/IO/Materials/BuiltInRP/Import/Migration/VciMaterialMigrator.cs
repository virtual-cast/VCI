using System;
using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UnityEngine;
using VRMShaders;
using ColorSpace = VRMShaders.ColorSpace;

namespace VCI
{
    public static class VciMaterialMigrator
    {
        public static bool Migrate(GltfData data, int i, MaterialDescriptor matDesc, VciMaterialMigrationTarget target, bool migrateSrgbColor, out MaterialDescriptor migratedMatDesc)
        {
            var migratedColors = matDesc.Colors.ToDictionary(x => x.Key, x => x.Value);

            switch (target)
            {
                case VciMaterialMigrationTarget.Pbr:
                    MigrateEmissionColorToHdrRange(data, i, matDesc, migratedColors);
                    if (migrateSrgbColor)
                    {
                        MigrateBaseColorSpaceToLinear(data, i, matDesc, migratedColors);
                    }
                    break;
                case VciMaterialMigrationTarget.Unlit:
                    if (migrateSrgbColor)
                    {
                        MigrateBaseColorSpaceToLinear(data, i, matDesc, migratedColors);
                    }
                    break;
                case VciMaterialMigrationTarget.MToon:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), target, null);
            }

            migratedMatDesc = new MaterialDescriptor(
                matDesc.Name,
                matDesc.ShaderName,
                matDesc.RenderQueue,
                matDesc.TextureSlots,
                matDesc.FloatValues,
                migratedColors,
                matDesc.Vectors,
                matDesc.Actions
            );
            return true;
        }

        /// <summary>
        /// VCAST_materials_pbr拡張が存在する場合はパラメータを上書きする.
        /// この拡張は値を override するものであるため、UniGLTF の拡張と同時に存在しても、 UniGLTF の拡張の処理よりも後に処理する限り問題ない.
        /// Target: pbr
        /// </summary>
        private static bool MigrateEmissionColorToHdrRange(GltfData data, int i, MaterialDescriptor matDesc, IDictionary<string, Color> migratedColors)
        {
            if (!TryGetVciMaterialsPbrNodeExtension(data, i, out var vciPbrExt)) return false;

            if (vciPbrExt.emissiveFactor == null || vciPbrExt.emissiveFactor.Length != 3) return false;

            var emissionColor = vciPbrExt.emissiveFactor.ToColor3(ColorSpace.Linear, ColorSpace.Linear);

            const string emissionKey = "_EmissionColor";
            if (migratedColors.ContainsKey(emissionKey))
            {
                migratedColors[emissionKey] = emissionColor;
            }
            else
            {
                migratedColors.Add(emissionKey, emissionColor);
            }

            return true;
        }

        /// <summary>
        /// UniVCI v0.27以下のバージョンでExportしたVCIは、baseColorFactorがSrgbで値が入っているためLinearに変換する必要がある
        /// Target: unlit, pbr
        /// </summary>
        private static bool MigrateBaseColorSpaceToLinear(GltfData data, int i, MaterialDescriptor matDesc, IDictionary<string, Color> migratedColors)
        {
            const string baseColorKey = "_Color";
            if (matDesc.Colors.ContainsKey(baseColorKey))
            {
                migratedColors[baseColorKey] = matDesc.Colors[baseColorKey].linear;
            }

            return true;
        }

        private static bool TryGetVciMaterialsPbrNodeExtension(GltfData data, int materialIdx, out glTF_VCAST_materials_pbr pbrExt)
        {
            if (materialIdx < 0 || materialIdx >= data.GLTF.materials.Count)
            {
                pbrExt = default;
                return false;
            }

            var gltfMaterial = data.GLTF.materials[materialIdx];
            return gltfMaterial.extensions.TryDeserializeExtensions(glTF_VCAST_materials_pbr.ExtensionName,
                glTF_VCAST_materials_pbr_Deserializer.Deserialize, out pbrExt);
        }
    }
}
