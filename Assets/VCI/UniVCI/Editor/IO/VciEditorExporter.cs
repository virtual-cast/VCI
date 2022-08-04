using System;
using UniGLTF;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public static class VciEditorExporter
    {
        public static byte[] Export(GameObject root, ITextureSerializer textureDeserializer = null, IVciColliderLayerProvider collisionLayerProvider = null)
        {
            VciExportingTextureSettingFixer.Fix(root);

            var exportingGltfData = new ExportingGltfData();
            using (var exporter = new VCIExporter(exportingGltfData, new EditorAnimationNodeExporter(), collisionLayerProvider))
            {
                exporter.Prepare(root);
                exporter.Export(textureDeserializer ?? new EditorTextureSerializer());
                return exportingGltfData.ToGlbBytes();
            }
        }
    }
}
