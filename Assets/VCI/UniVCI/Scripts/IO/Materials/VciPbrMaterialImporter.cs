using System.Linq;
using UniGLTF;
using VRMShaders;

namespace VCI
{
    public static class VciPbrMaterialImporter
    {
        public static bool TryCreateParam(GltfData data, int i, out MaterialDescriptor matDesc, bool migrateSrgbColor)
        {
            // NOTE: GLTF の PBR 定義に準じる.
            if (!GltfPbrMaterialImporter.TryCreateParam(data, i, out matDesc))
            {
                return false;
            }

            // NOTE:昔の UniVCI により出力された VCI ファイルに対するマイグレーション.
            if (VciMaterialMigrator.Migrate(data, i, matDesc, VciMaterialMigrationTarget.Pbr, migrateSrgbColor, out var migratedMatDesc))
            {
                matDesc = migratedMatDesc;
            }

            return true;
        }
    }
}
