using System;
using System.Collections.Generic;
using UniGLTF;

namespace VCI
{
    /// <summary>
    /// extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_audio_sources
    {
        public static string ExtensionName => "VCAST_vci_audio_sources";
        [JsonSchema(Required = true, MinItems = 1)]
        public List<AudioSourceJsonObject> audioSources;
    }
}
