using UnityEngine;

namespace VCI
{
    /// <summary>
    /// ルートに置くと、シーンのライトマップ・ライトプローブ・リフレクションプローブ・Skybox を出力に含める。
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(VCIObject))]
    [HelpURL("https://developer.virtualcast.jp/vci-docs/manual/location/lightbake.html")]
    public sealed class VCIBakedLighting : MonoBehaviour
    {
    }
}
