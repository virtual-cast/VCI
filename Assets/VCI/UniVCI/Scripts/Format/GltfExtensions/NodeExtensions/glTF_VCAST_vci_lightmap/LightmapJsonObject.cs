using System;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// ライトマップ情報をノードに記録する extension.
    /// </summary>
    [Serializable]
    public sealed class LightmapJsonObject
    {
        /// <summary>
        /// ライトマップ用テクスチャの参照
        /// Required
        /// </summary>
        public LightmapTextureInfoJsonObject texture;

        /// <summary>
        /// ライトマップ用テクスチャを参照するための UV Offset
        /// ノードごとに一意な値であるため、テクスチャではなくこの拡張に記録する
        /// The offset of the UV coordinate origin as a factor of the texture dimensions.
        /// </summary>
        public float[] offset = new float[2] { 0.0f, 0.0f };

        /// <summary>
        /// ライトマップ用テクスチャを参照するための UV Scale
        /// ノードごとに一意な値であるため、テクスチャではなくこの拡張に記録する
        /// The scale factor applied to the components of the UV coordinates.
        /// </summary>
        public float[] scale = new float[2] { 1.0f, 1.0f };
    }
}