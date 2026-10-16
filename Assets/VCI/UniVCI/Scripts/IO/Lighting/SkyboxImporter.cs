using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UniGLTF;

namespace VCI
{
    public sealed class SkyboxImporter
    {
        private const string CubemapSkyboxPrefabPath = "CubemapSkybox";
        private static readonly int TexProperty = Shader.PropertyToID("_Tex");

        private readonly CubemapTextureFactory _cubemapTextureFactory;
        private readonly Material _skyboxPrefab;

        public SkyboxImporter(CubemapTextureFactory cubemapTextureFactory)
        {
            _cubemapTextureFactory = cubemapTextureFactory;
            _skyboxPrefab = Resources.Load<Material>(CubemapSkyboxPrefabPath);
        }

        public async Task<Material> ConvertToSkyboxAsync(CubemapTextureJsonObject cubemapTexture, CubemapCompressionType compressionType, IAwaitCaller awaitCaller)
        {
            var cubemap = await _cubemapTextureFactory.GetOrConvertCubemapTextureAsync(cubemapTexture, compressionType, awaitCaller);
            var mat = new Material(_skyboxPrefab);
            mat.SetTexture(TexProperty, cubemap);
            return mat;
        }
    }
}
