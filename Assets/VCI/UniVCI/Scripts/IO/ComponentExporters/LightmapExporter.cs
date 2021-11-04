using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UnityEngine;
using UnityEngine.Rendering;
using VRMShaders;

namespace VCI
{
    public static class LightmapExporter
    {
        public static List<(glTFNode, glTF_VCAST_vci_lightmap)> ExportLightmaps(
            glTF gltf,
            List<Transform> nodes,
            List<Transform> originalNodes,
            LightmapTextureExporter lightmapTextureExporter)
        {
            var lightmapExtensions = new List<(glTFNode, glTF_VCAST_vci_lightmap)>();

            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                var gltfNode = gltf.nodes[i];

                var renderer = node.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    var useLightmapExtension = false;
#if UNITY_EDITOR
                    var contributeGi = UnityEditor.GameObjectUtility.GetStaticEditorFlags(node.gameObject)
                        .HasFlag(UnityEditor.StaticEditorFlags.ContributeGI);
                    var receiveLightmap = renderer.receiveGI == ReceiveGI.Lightmaps;
                    var isLightmapExistsInScene =
                        LightmapSettings.lightmaps != null && LightmapSettings.lightmaps.Length > 0;
                    useLightmapExtension = contributeGi && receiveLightmap && isLightmapExistsInScene;
#endif

                    if (useLightmapExtension)
                    {
                        var originalRenderer = originalNodes[i].GetComponent<MeshRenderer>();

                        var lightmapUnityIndex = originalRenderer.lightmapIndex;
                        if (lightmapUnityIndex < 0 || lightmapUnityIndex >= LightmapSettings.lightmaps.Length) continue;

                        var lightmapGltfIndex = lightmapTextureExporter.GetOrAddColorTexture(lightmapUnityIndex);

                        if (lightmapGltfIndex >= 0)
                        {
                            var so = originalRenderer.lightmapScaleOffset;
                            var scale = new Vector2(so.x, so.y);
                            var offset = new Vector2(so.z, so.w);
                            offset.y = (offset.y + scale.y - 1) * -1.0f;

                            lightmapExtensions.Add((gltfNode, new glTF_VCAST_vci_lightmap
                            {
                                lightmap = new LightmapJsonObject
                                {
                                    texture = new LightmapTextureInfoJsonObject {index = lightmapGltfIndex},
                                    offset = new[] {offset.x, offset.y},
                                    scale = new[] {scale.x, scale.y},
                                },
                            }));
                        }
                    }
                }
            }

            return lightmapExtensions;
        }

        public static glTF_VCAST_vci_location_lighting ExportLocationLighting(
            bool existsLightmapExtension,
            LightmapTextureExporter lightmapTextureExporter,
            CubemapTextureExporter cubemapTextureExporter)
        {
            var enableLocationLightingExtension = existsLightmapExtension;
            if (!enableLocationLightingExtension)
            {
                return null;
            }

            var skyboxExporter = new SkyboxExporter(cubemapTextureExporter);
            var lightProbeExporter = new LightProbeExporter();

            return new glTF_VCAST_vci_location_lighting
            {
                locationLighting = new LocationLightingJsonObject
                {
                    lightmapCompressionMode =
                        LocationLightingJsonObject.ConvertLightmapCompressionMode(lightmapTextureExporter
                            .CompressionType),
                    lightmapDirectionalMode =
                        LocationLightingJsonObject.ConvertLightmapDirectionalMode(lightmapTextureExporter
                            .DirectionalType),
                    lightmapTextures = lightmapTextureExporter.RegisteredColorTextureIndexArray
                        .Select(x => new LightmapTextureInfoJsonObject {index = x})
                        .ToArray(),
                    skyboxCubemap = skyboxExporter.Export(1024),
                    lightProbes = lightProbeExporter.Export(),
                },
            };
        }

        public static List<(glTFNode, glTF_VCAST_vci_reflectionProbe)> ExportReflectionProbes(
            glTF gltf,
            List<Transform> nodes,
            List<Transform> originalNodes,
            CubemapTextureExporter cubemapTextureExporter)
        {
            var reflectionProbeExtensions = new List<(glTFNode, glTF_VCAST_vci_reflectionProbe)>();

            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                var gltfNode = gltf.nodes[i];

                var reflectionProbe = originalNodes[i].GetComponent<ReflectionProbe>();
                if (reflectionProbe == null) continue;

                var exportReflectionProbeExtension = false;
                var isModeActive = reflectionProbe.mode == ReflectionProbeMode.Baked;
                var texture = reflectionProbe.bakedTexture;
                var isTextureExists = texture != null && texture.dimension == TextureDimension.Cube;

                exportReflectionProbeExtension = !Application.isPlaying && isModeActive && isTextureExists;
                if (!exportReflectionProbeExtension) continue;

                var offset = reflectionProbe.center;
                var size = reflectionProbe.size;

                reflectionProbeExtensions.Add((gltfNode, new glTF_VCAST_vci_reflectionProbe
                {
                    reflectionProbe = new ReflectionProbeJsonObject
                    {
                        boxOffset = new[] {-offset.x, offset.y, offset.z}, // invert X-axis
                        boxSize = new[] {size.x, size.y, size.z},
                        intensity = reflectionProbe.intensity,
                        useBoxProjection = reflectionProbe.boxProjection,
                        cubemap = cubemapTextureExporter.Export(texture, reflectionProbe.resolution,
                            includeMipmaps: true),
                    },
                }));
            }

            return reflectionProbeExtensions;
        }
    }
}