using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_audio_sources_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_audio_sources value)
{
    f.BeginMap();


    if(value.audioSources!=null&&value.audioSources.Count>=1){
        f.Key("audioSources");
        glTF_VCAST_vci_audio_sources_Serializevci_audioSources(f, value.audioSources);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_audio_sources_Serializevci_audioSources(JsonFormatter f, List<AudioSourceJsonObject> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    glTF_VCAST_vci_audio_sources_Serializevci_audioSources_ITEM(f, item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_audio_sources_Serializevci_audioSources_ITEM(JsonFormatter f, AudioSourceJsonObject value)
{
    f.BeginMap();


    if(true){
        f.Key("audio");
        f.Value(value.audio);
    }

    if(true){
        f.Key("spatialBlend");
        f.Value(value.spatialBlend);
    }

    f.EndMap();
}

    } // class
} // namespace
