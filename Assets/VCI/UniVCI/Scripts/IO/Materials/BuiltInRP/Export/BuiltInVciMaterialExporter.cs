using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UnityEngine;
using VRM;
using VRMShaders;

namespace VCI
{
    /// <summary>
    /// glTF の material 仕様に従ってエクスポートする。
    /// </summary>
    public sealed class BuiltInVciMaterialExporter : IMaterialExporter
    {
        private readonly BuiltInGltfMaterialExporter _gltfExporter = new BuiltInGltfMaterialExporter();

        public glTFMaterial ExportMaterial(Material src, ITextureExporter textureExporter, GltfExportSettings settings)
        {
            switch (src.shader.name)
            {
                case BuiltInVrmMToonMaterialExporter.TargetShaderName:
                    if (BuiltInVrmMToonMaterialExporter.TryExportMaterial(src, textureExporter, out var dst)) return dst;
                    break;
            }

            return _gltfExporter.ExportMaterial(src, textureExporter, settings);
        }
    }
}
