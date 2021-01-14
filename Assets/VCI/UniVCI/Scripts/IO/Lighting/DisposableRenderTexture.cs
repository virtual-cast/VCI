using System;
using UnityEngine;

namespace VCI
{
    public readonly struct DisposableRenderTexture : IDisposable
    {
        public RenderTexture RenderTexture { get; }

        public DisposableRenderTexture(RenderTexture texture)
        {
            RenderTexture = texture;
        }

        public void Dispose()
        {
            UnityEngine.Object.DestroyImmediate(RenderTexture);
        }
    }
}
