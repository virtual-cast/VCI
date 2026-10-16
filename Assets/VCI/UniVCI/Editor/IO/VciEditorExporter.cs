using System;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class VciEditorExporter
    {
        public static byte[] Export(GameObject root, ITextureSerializer textureDeserializer = null, IVciColliderLayerProvider collisionLayerProvider = null)
        {
            VciExportingTextureSettingFixer.Fix(root);

            var exportingGltfData = new ExportingGltfData();
            using (var exporter = new VCIExporter(exportingGltfData,
                animationNodeExporter: new EditorAnimationNodeExporter(), colliderLayerProvider: collisionLayerProvider,
                textureSerializer: textureDeserializer ?? new EditorTextureSerializer()))
            {
                exporter.Prepare(root);
                exporter.Export();
                return exportingGltfData.ToGlbBytes();
            }
        }
    }
}
