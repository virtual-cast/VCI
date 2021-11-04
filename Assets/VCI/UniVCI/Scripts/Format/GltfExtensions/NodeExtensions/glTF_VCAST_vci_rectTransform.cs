using System;

namespace VCI
{
    /// <summary>
    /// glTF Node Extension.
    /// Unity の RectTransform 情報を表す.
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_rectTransform
    {
        /// <summary>
        /// NOTE: シリアライズされる ExtensionName が流儀を外れて `rectTransform` と camelCase になっているが、互換性のために間違ったままとする.
        /// </summary>
        public static string ExtensionName => "VCAST_vci_rectTransform";

        public VCI.RectTransformJsonObject rectTransform;
    }
}
