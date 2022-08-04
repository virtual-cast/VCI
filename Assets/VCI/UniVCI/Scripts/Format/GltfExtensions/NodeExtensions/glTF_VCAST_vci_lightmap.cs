using System;

namespace VCI
{
    [Serializable]
    public sealed class glTF_VCAST_vci_lightmap
    {
        public static string ExtensionName => "VCAST_vci_lightmap";

        public LightmapJsonObject lightmap;
    }
}
