
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_audios_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_audios value)
{
    f.BeginMap();


    if(value.audios!=null&&value.audios.Count>=1){
        f.Key("audios");                
        Serialize_vci_audios(f, value.audios);
    }

    f.EndMap();
}

public static void Serialize_vci_audios(JsonFormatter f, List<glTF_VCAST_vci_audio> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    Serialize_vci_audios_ITEM(f, item);

    }
    f.EndList();
}

public static void Serialize_vci_audios_ITEM(JsonFormatter f, glTF_VCAST_vci_audio value)
{
    f.BeginMap();


    if(!string.IsNullOrEmpty(value.name)){
        f.Key("name");                
        f.Value(value.name);
    }

    if(value.bufferView>=0){
        f.Key("bufferView");                
        f.Value(value.bufferView);
    }

    if(!string.IsNullOrEmpty(value.mimeType)){
        f.Key("mimeType");                
        f.Value(value.mimeType);
    }

    f.EndMap();
}

} // VciSerializer
} // VCI
