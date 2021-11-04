using System;
using System.Collections.Generic;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_animation
    {
        public static string ExtensionName => "VCAST_vci_animation";

        [UniGLTF.JsonSchema(MinItems = 1)]
        public List<AnimationReferenceJsonObject> animationReferences;
    }
}
