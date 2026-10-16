using UniGLTF;
using UnityEngine;

namespace VCI
{
    public sealed class BuiltInVciPbrMaterialImporter
    {
        private readonly BuiltInGltfPbrMaterialImporter _builtinGltfPbrMaterialImporter = new();
        public Shader Shader => _builtinGltfPbrMaterialImporter.Shader;

        public bool TryCreateParam(GltfData data, int i, out MaterialDescriptor matDesc, bool migrateSrgbColor)
        {
            // NOTE: GLTF の PBR 定義に準じる.
            if (!_builtinGltfPbrMaterialImporter.TryCreateParam(data, i, out matDesc))
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
