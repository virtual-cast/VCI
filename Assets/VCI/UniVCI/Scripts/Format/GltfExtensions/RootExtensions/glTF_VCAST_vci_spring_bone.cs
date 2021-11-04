using System;
using System.Collections.Generic;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_spring_bone
    {
        public static string ExtensionName => "VCAST_vci_spring_bone";

        [UniGLTF.JsonSchema(Required = true, MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<SpringBoneJsonObject> springBones;
    }
}
