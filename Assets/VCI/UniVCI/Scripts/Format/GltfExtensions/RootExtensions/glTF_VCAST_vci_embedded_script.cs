using System;
using System.Collections.Generic;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_embedded_script
    {
        public static string ExtensionName => "VCAST_vci_embedded_script";

        [UniGLTF.JsonSchema(Required = true, MinItems = 1)]
        public List<EmbeddedScriptJsonObject> scripts = new List<EmbeddedScriptJsonObject>();

        public int entryPoint;
    }
}
