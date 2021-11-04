using System;
using System.Collections.Generic;

namespace VCI
{
    /// <summary>
    /// glTF Root Extension.
    /// Effekseer の Effect の集合を表す.
    /// </summary>
    [Serializable]
    public class glTF_Effekseer
    {
        public static string ExtensionName => "Effekseer";

        [UniGLTF.JsonSchema(Required = true, MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<EffekseerEffectJsonObject> effects;
    }
}
