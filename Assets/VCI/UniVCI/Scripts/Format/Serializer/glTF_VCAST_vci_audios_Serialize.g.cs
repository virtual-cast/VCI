using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_audios_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_audios value)
{
    f.BeginMap();


    if(value.audios!=null&&value.audios.Count>=1){
        f.Key("audios");
        glTF_VCAST_vci_audios_Serializevci_audios(f, value.audios);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_audios_Serializevci_audios(JsonFormatter f, List<AudioJsonObject> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    glTF_VCAST_vci_audios_Serializevci_audios_ITEM(f, item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_audios_Serializevci_audios_ITEM(JsonFormatter f, AudioJsonObject value)
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

    } // class
} // namespace
