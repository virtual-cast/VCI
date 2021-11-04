using System;

namespace VCI
{
    [Serializable]
    public class glTF_VCAST_vci_lightmap
    {
        public static string ExtensionName => "VCAST_vci_lightmap";

        public LightmapJsonObject lightmap;
    }
}
