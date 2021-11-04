using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniGLTF;
using UnityEngine;
using UnityEngine.Rendering;
using VRMShaders;

namespace VCI
{
    public static class LightmapImporter
    {
        public static async Task LoadAsync(
            VciData vciData,
            IReadOnlyList<Transform> unityNodes,
            GameObject unityRoot,
            TextureFactory textureFactory,
            bool isLocation,
            IAwaitCaller awaitCaller)
        {
            var supportImporting = Application.isPlaying && isLocation;
            if (!supportImporting)
            {
                return;
            }

            var locationLighting = vciData?.LocationLighting?.locationLighting;
            if (locationLighting == null)
            {
                return;
            }

            // 現在のところはこの組み合わせしかサポートしない
            if (locationLighting.skyboxCubemap.GetSkyboxCompressionModeAsEnum() != CubemapCompressionType.Rgbm) return;
            if (locationLighting.GetLightmapCompressionModeAsEnum() != LightmapCompressionType.Rgbm) return;
            if (locationLighting.GetLightmapDirectionalModeAsEnum() != LightmapDirectionalType.NonDirectional) return;

            await awaitCaller.NextFrame();

            // Lightmap
            var lightmapCompressionMode = locationLighting.GetLightmapCompressionModeAsEnum();
            var lightmapDirectionalMode = locationLighting.GetLightmapDirectionalModeAsEnum();
            var lightmapTextureImporter =
                new LightmapTextureImporter(vciData.GltfData, textureFactory, lightmapCompressionMode, lightmapDirectionalMode);
            var lightmapTextures = new List<Texture2D>();
            for (var idx = 0; idx < locationLighting.lightmapTextures.Length; ++idx)
            {
                var x = locationLighting.lightmapTextures[idx];
                var lightmapTexture = await lightmapTextureImporter.GetOrConvertLightmapTextureAsync(x.index, awaitCaller);
                lightmapTextures.Add(lightmapTexture);
            }

            foreach (var pair in vciData.LightmapNodes)
            {
                var (nodeIdx, lightmapExtension) = pair;
                var renderer = unityNodes[nodeIdx].gameObject.GetComponent<MeshRenderer>();
                if (renderer == null) continue;

                var lightmap = lightmapExtension.lightmap;
                var lightmapTexture = await lightmapTextureImporter.GetOrConvertLightmapTextureAsync(
                    lightmap.texture.index, awaitCaller);
                var offset = new Vector2(lightmap.offset[0], lightmap.offset[1]);
                var scale = new Vector2(lightmap.scale[0], lightmap.scale[1]);

                // Coordinate conversion
                offset.y = (offset.y + scale.y - 1.0f) * -1.0f;

                // Get LightmapData Index
                var lightmapDataIndex = lightmapTextures.FindIndex(x => x == lightmapTexture);
                if (lightmapDataIndex == -1) continue;

                // Apply to Renderer
                renderer.lightmapIndex = lightmapDataIndex;
                renderer.lightmapScaleOffset = new Vector4(scale.x, scale.y, offset.x, offset.y);
            }

            // Ambient Lighting
            var skyboxCompressionMode = locationLighting.skyboxCubemap.GetSkyboxCompressionModeAsEnum();
            var skyboxCubemapImporter = new CubemapTextureImporter(vciData.GltfData, textureFactory, skyboxCompressionMode);
            var skyboxImporter = new SkyboxImporter(skyboxCubemapImporter);
            var lightProbeImporter = new LightProbeImporter();

            var behaviour = unityRoot.AddComponent<VCILocationLighting>();
            behaviour.LightmapDataArray = lightmapTextures.Select(x => new LightmapData {lightmapColor = x}).ToArray();
            switch (lightmapDirectionalMode)
            {
                case LightmapDirectionalType.Directional:
                    behaviour.LightmapMode = LightmapsMode.CombinedDirectional;
                    break;
                case LightmapDirectionalType.NonDirectional:
                    behaviour.LightmapMode = LightmapsMode.NonDirectional;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            behaviour.Skybox = await skyboxImporter.CovertToSkyboxAsync(locationLighting.skyboxCubemap, awaitCaller);

            var (lightProbePosArray, lightProbeCoefficientArray) = await awaitCaller.Run(() => lightProbeImporter.Import(locationLighting.lightProbes));
            behaviour.LightProbePositions = lightProbePosArray;
            behaviour.LightProbeCoefficients = lightProbeCoefficientArray;

            // ReflectionProbe
            foreach (var pair in vciData.ReflectionProbeNodes)
            {
                var (nodeIdx, reflectionProbeExtension) = pair;
                var go = unityNodes[nodeIdx].gameObject;

                var reflectionProbe = go.AddComponent<ReflectionProbe>();

                var data = reflectionProbeExtension.reflectionProbe;
                var gltfOffset = data.boxOffset;
                var gltfSize = data.boxSize;
                var cubemapImporter =
                    new CubemapTextureImporter(vciData.GltfData, textureFactory, data.cubemap.GetSkyboxCompressionModeAsEnum());

                var cubemap = await cubemapImporter.GetOrConvertCubemapTextureAsync(data.cubemap, awaitCaller);

                reflectionProbe.center = new Vector3(-gltfOffset[0], gltfOffset[1], gltfOffset[2]); // Invert X-axis
                reflectionProbe.size = new Vector3(gltfSize[0], gltfSize[1], gltfSize[2]);
                reflectionProbe.intensity = data.intensity;
                reflectionProbe.boxProjection = data.useBoxProjection;
                reflectionProbe.bakedTexture = cubemap;

                reflectionProbe.mode = ReflectionProbeMode.Baked;
            }
        }
    }
}