using System;
using System.Collections.Generic;
using UniGLTF;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_audios
    {
        public static string ExtensionName = "VCAST_vci_audios";
        [JsonSchema(Required = true, MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<AudioJsonObject> audios;
    }
}
