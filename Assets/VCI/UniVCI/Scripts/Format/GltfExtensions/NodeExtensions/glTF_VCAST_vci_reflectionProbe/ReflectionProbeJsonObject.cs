using System;
using UnityEngine;

namespace VCI
{
    [Serializable]
    public sealed class ReflectionProbeJsonObject
    {
        /// <summary>
        /// この拡張のついた node の位置（ワールド座標系）を基準にして、ボックスのセンターはどれくらいズレているか
        /// </summary>
        public float[] boxOffset = new float[3] { 0, 0, 0 };

        /// <summary>
        /// ボックスのサイズ。ワールド座標系。
        /// </summary>
        public float[] boxSize = new float[3] { 0, 0, 0 };

        /// <summary>
        /// cubemap に対する強さの係数
        /// </summary>
        public float intensity = 1;

        /// <summary>
        /// Box Projection を使用するかどうか
        /// </summary>
        public bool useBoxProjection = false;

        /// <summary>
        /// テクスチャ
        /// </summary>
        public CubemapTextureJsonObject cubemap;
    }
}
