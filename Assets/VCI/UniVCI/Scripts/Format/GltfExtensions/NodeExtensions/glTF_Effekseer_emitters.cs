using System;
using System.Collections.Generic;

namespace VCI
{
    /// <summary>
    /// glTF Node Extension.
    /// Effekseer の Emitter の集合を表す.
    /// </summary>
    [Serializable]
    public class glTF_Effekseer_emitters
    {
        public static string ExtensionName => "Effekseer_emitters";

        [UniGLTF.JsonSchema(MinItems = 1)]
        public List<EffekseerEmitterJsonObject> emitters;
    }
}
