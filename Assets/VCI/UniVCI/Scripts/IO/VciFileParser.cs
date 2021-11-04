using System.IO;
using UniGLTF;

namespace VCI
{
    public sealed class VciFileParser
    {
        private readonly string _filePath;

        public VciFileParser(string filePath)
        {
            _filePath = filePath;
        }

        public VciData Parse()
        {
            var gltfData = new GlbLowLevelParser(_filePath, File.ReadAllBytes(_filePath)).Parse();

            return new VciData(
                gltfData,
                VciBinaryParser.CheckMigrationFlags(gltfData),
                VciBinaryParser.DeserializeScriptExtension(gltfData.GLTF),
                VciBinaryParser.DeserializeAudioExtension(gltfData.GLTF),
                VciBinaryParser.DeserializeUnityMaterialExtension(gltfData.GLTF),
                VciBinaryParser.DeserializeLocationLightingExtension(gltfData.GLTF),
                VciBinaryParser.DeserializeSpringBoneExtension(gltfData.GLTF),
                VciBinaryParser.DeserializeEffekseerExtension(gltfData.GLTF),
                VciBinaryParser.DeserializeAudioSourcesNodeExtensions(gltfData.GLTF),
                VciBinaryParser.DeserializeAnimationNodeExtensions(gltfData.GLTF),
                VciBinaryParser.DeserializeAttachableExtensions(gltfData.GLTF),
                VciBinaryParser.DeserializeLightmapExtensions(gltfData.GLTF),
                VciBinaryParser.DeserializeReflectionProbeExtensions(gltfData.GLTF),
                VciBinaryParser.DeserializeLocationBoundsExtensions(gltfData.GLTF),
                VciBinaryParser.DeserializeCollidersExtensions(gltfData.GLTF),
                VciBinaryParser.DeserializeJointsExtensions(gltfData.GLTF),
                VciBinaryParser.DeserializeRigidbodyExtensions(gltfData.GLTF),
                VciBinaryParser.DeserializeSubItemExtensions(gltfData.GLTF),
                VciBinaryParser.DeserializePlayerSpawnPointExtensions(gltfData.GLTF),
                VciBinaryParser.DeserializePlayerSpawnPointRestrictionExtensions(gltfData.GLTF),
                VciBinaryParser.DeserializeTextExtensions(gltfData.GLTF),
                VciBinaryParser.DeserializeRectTransformExtensions(gltfData.GLTF),
                VciBinaryParser.DeserializeEffekseerEmittersExtensions(gltfData.GLTF)
            );
        }
    }
}
