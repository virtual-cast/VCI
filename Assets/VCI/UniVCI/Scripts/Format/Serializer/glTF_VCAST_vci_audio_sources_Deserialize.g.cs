using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_audio_sources_Deserializer
    {


public static glTF_VCAST_vci_audio_sources Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_audio_sources();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="audioSources"){
            value.audioSources = glTF_VCAST_vci_audio_sources_Deserializevci_audioSources(kv.Value);
            continue;
        }

    }
    return value;
}

public static List<VCI.AudioSourceJsonObject> glTF_VCAST_vci_audio_sources_Deserializevci_audioSources(JsonNode parsed)
{
    var value = new List<AudioSourceJsonObject>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_VCAST_vci_audio_sources_Deserializevci_audioSources_ITEM(x));
    }
	return value;
}
public static AudioSourceJsonObject glTF_VCAST_vci_audio_sources_Deserializevci_audioSources_ITEM(JsonNode parsed)
{
    var value = new AudioSourceJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="audio"){
            value.audio = kv.Value.GetInt32();
            continue;
        }

        if(key=="spatialBlend"){
            value.spatialBlend = kv.Value.GetSingle();
            continue;
        }

    }
    return value;
}

    } // class
} // namespace
