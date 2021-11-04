using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_embedded_script_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_embedded_script value)
{
    f.BeginMap();


    if(value.scripts!=null&&value.scripts.Count>=1){
        f.Key("scripts");
        glTF_VCAST_vci_embedded_script_Serializevci_scripts(f, value.scripts);
    }

    if(true){
        f.Key("entryPoint");
        f.Value(value.entryPoint);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_embedded_script_Serializevci_scripts(JsonFormatter f, List<EmbeddedScriptJsonObject> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    glTF_VCAST_vci_embedded_script_Serializevci_scripts_ITEM(f, item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_embedded_script_Serializevci_scripts_ITEM(JsonFormatter f, EmbeddedScriptJsonObject value)
{
    f.BeginMap();


    if(!string.IsNullOrEmpty(value.name)){
        f.Key("name");
        f.Value(value.name);
    }

    if(!string.IsNullOrEmpty(value.mimeType)){
        f.Key("mimeType");
        f.Value(value.mimeType);
    }

    if(!string.IsNullOrEmpty(value.targetEngine)){
        f.Key("targetEngine");
        f.Value(value.targetEngine);
    }

    if(value.source>=0){
        f.Key("source");
        f.Value(value.source);
    }

    f.EndMap();
}

    } // class
} // namespace
