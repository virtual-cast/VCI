
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_audio_sources_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_audio_sources value)
{
    f.BeginMap();


    if(value.audioSources!=null&&value.audioSources.Count>=1){
        f.Key("audioSources");                
        Serialize_vci_audioSources(f, value.audioSources);
    }

    f.EndMap();
}

public static void Serialize_vci_audioSources(JsonFormatter f, List<glTF_VCAST_vci_audio_source> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    Serialize_vci_audioSources_ITEM(f, item);

    }
    f.EndList();
}

public static void Serialize_vci_audioSources_ITEM(JsonFormatter f, glTF_VCAST_vci_audio_source value)
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

} // VciSerializer
} // VCI
