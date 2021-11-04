using UnityEngine;
using UnityEngine.Rendering;

namespace VCI
{
    public sealed class SkyboxExporter
    {
        private CubemapTextureExporter _exporter;

        public SkyboxExporter(CubemapTextureExporter exporter)
        {
            _exporter = exporter;
        }

        public CubemapTextureJsonObject Export(int width)
        {
            using (var rt = RenderCurrentSceneSkybox(width))
            {
                return _exporter.Export(rt.RenderTexture, width, includeMipmaps: false);
            }
        }

        private DisposableRenderTexture RenderCurrentSceneSkybox(int width)
        {
            var rt = new RenderTexture(width, width, 0, RenderTextureFormat.DefaultHDR);
            rt.dimension = TextureDimension.Cube;

            var camera = new GameObject("temporary").AddComponent<Camera>();
            camera.cullingMask = 0;
            camera.clearFlags = CameraClearFlags.Skybox;
            camera.RenderToCubemap(rt);

            UnityEngine.Object.DestroyImmediate(camera.gameObject);

            return new DisposableRenderTexture(rt);
        }
    }
}
