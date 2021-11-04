using System;
using System.Collections.Generic;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_material_unity
    {
        public static string ExtensionName => "VCAST_vci_material_unity";

        /// <summary>
        /// Same as vci material
        /// </summary>
        public List<VciMaterialJsonObject> materials;
    }
}
