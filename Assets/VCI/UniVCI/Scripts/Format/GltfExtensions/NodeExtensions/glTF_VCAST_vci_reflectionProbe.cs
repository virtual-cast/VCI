using System;

namespace VCI
{
    [Serializable]
    public sealed class glTF_VCAST_vci_reflectionProbe
    {
        /// <summary>
        /// NOTE: シリアライズ文字列が camelCase になっているので互換性のために間違ったままにする必要がある.
        /// TODO: ただし、ライトベイクユーザ公開時に互換性を破壊してよいので直す.
        /// </summary>
        public static string ExtensionName => "VCAST_vci_reflectionProbe";

        public ReflectionProbeJsonObject reflectionProbe;
    }
}
