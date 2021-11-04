using System;
using System.Collections.Generic;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public sealed class glTF_VCAST_vci_rigidbody
    {
        public static string ExtensionName => "VCAST_vci_rigidbody";

        [UniGLTF.JsonSchema(MinItems = 1)] public List<RigidbodyJsonObject> rigidbodies;
    }
}
