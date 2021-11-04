using System;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_location_bounds
    {
        public static string ExtensionName => "VCAST_vci_location_bounds";

        /// <summary>
        /// NOTE: 名前が `LocationBounds` と JSON の流儀から外れて CamelCase になってしまっているが、互換性のために間違ったままとする.
        /// </summary>
        public LocationBoundsJsonObject LocationBounds;
    }
}
