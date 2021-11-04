using System;
using System.Collections.Generic;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_colliders
    {
        public static string ExtensionName => "VCAST_vci_collider";

        [UniGLTF.JsonSchema(MinItems = 1)] public List<ColliderJsonObject> colliders;
    }
}
