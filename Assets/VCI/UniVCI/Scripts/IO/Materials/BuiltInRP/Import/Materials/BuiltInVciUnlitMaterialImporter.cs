using UniGLTF;

namespace VCI
{
    public sealed class BuiltInVciUnlitMaterialImporter
    {
        private readonly BuiltInGltfUnlitMaterialImporter _builtinGltfUnlitMaterialImporter = new();
        public bool TryCreateParam(GltfData data, int i, out MaterialDescriptor matDesc, bool migrateSrgbColor)
        {
            // NOTE: GLTF の KHR_materials_unlit 定義に準じる.
            if (!_builtinGltfUnlitMaterialImporter.TryCreateParam(data, i, out matDesc))
            {
                return false;
            }

            // NOTE:昔の UniVCI により出力された VCI ファイルに対するマイグレーション.
            if (VciMaterialMigrator.Migrate(data, i, matDesc, VciMaterialMigrationTarget.Unlit, migrateSrgbColor, out var migratedMatDesc))
            {
                matDesc = migratedMatDesc;
            }

            return true;
        }
    }
}
