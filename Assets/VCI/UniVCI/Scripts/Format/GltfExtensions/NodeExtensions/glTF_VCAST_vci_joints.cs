using System;
using System.Collections.Generic;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_joints
    {
        public static string ExtensionName => "VCAST_vci_joints";

        [UniGLTF.JsonSchema(MinItems = 1)] public List<JointJsonObject> joints;
    }
}
