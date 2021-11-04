using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public sealed class SkyboxImporter
    {
        private const string CubemapSkyboxPrefabPath = "CubemapSkybox";
        private static readonly int TexProperty = Shader.PropertyToID("_Tex");

        private readonly CubemapTextureImporter _importer;
        private readonly Material _skyboxPrefab;

        public SkyboxImporter(CubemapTextureImporter importer)
        {
            _importer = importer;
            _skyboxPrefab = Resources.Load<Material>(CubemapSkyboxPrefabPath);
        }

        public async Task<Material> CovertToSkyboxAsync(CubemapTextureJsonObject cubemapTexture, IAwaitCaller awaitCaller)
        {
            var cubemap = await _importer.ConvertCubemapAsync(cubemapTexture, awaitCaller);
            var mat = new Material(_skyboxPrefab);
            mat.SetTexture(TexProperty, cubemap);
            return mat;
        }
    }
}
