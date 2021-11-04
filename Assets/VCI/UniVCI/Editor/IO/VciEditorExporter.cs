using System;
using UniGLTF;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public static class VciEditorExporter
    {
        public static byte[] Export(GameObject root, IVciColliderLayerProvider collisionLayerProvider = null)
        {
            VciExportingTextureSettingFixer.Fix(root);

            var exportingGltfData = new ExportingGltfData();
            using (var exporter = new VCIExporter(exportingGltfData, collisionLayerProvider))
            {
                exporter.Prepare(root);
                exporter.Export(new EditorTextureSerializer());
                return exportingGltfData.ToGlbBytes();
            }
        }
    }
}
