using System;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_text
    {
        public static string ExtensionName => "VCAST_vci_text";

        public TextJsonObject text;
    }
}
