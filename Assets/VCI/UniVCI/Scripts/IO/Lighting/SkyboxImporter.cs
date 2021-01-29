using System.Collections;
using UnityEngine;

namespace VCI
{
    public sealed class SkyboxImporterResult
    {
        public Material Result { get; set; }
    }

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

        public IEnumerator CovertToSkyboxCoroutine(glTFCubemapTexture cubemapTexture, SkyboxImporterResult result)
        {
            var cubemapResult = new CubemapTextureImporterResult();
            yield return _importer.ConvertCubemapCoroutine(cubemapTexture, cubemapResult);
            var mat = new Material(_skyboxPrefab);
            mat.SetTexture(TexProperty, cubemapResult.Result);
            result.Result = mat;
        }
    }
}
