using UnityEngine;

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

        public Material CovertToSkybox(glTFCubemapTexture cubemapTexture)
        {
            var cubemap = _importer.ConvertCubemap(cubemapTexture);
            var mat = new Material(_skyboxPrefab);
            mat.SetTexture(TexProperty, cubemap);

            return mat;
        }
    }
}
