
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_embedded_script_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_embedded_script value)
{
    f.BeginMap();


    if(value.scripts!=null&&value.scripts.Count>=1){
        f.Key("scripts");                
        Serialize_vci_scripts(f, value.scripts);
    }

    if(true){
        f.Key("entryPoint");                
        f.Value(value.entryPoint);
    }

    f.EndMap();
}

public static void Serialize_vci_scripts(JsonFormatter f, List<glTF_VCAST_vci_embedded_script_source> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    Serialize_vci_scripts_ITEM(f, item);

    }
    f.EndList();
}

public static void Serialize_vci_scripts_ITEM(JsonFormatter f, glTF_VCAST_vci_embedded_script_source value)
{
    f.BeginMap();


    if(!string.IsNullOrEmpty(value.name)){
        f.Key("name");                
        f.Value(value.name);
    }

    if(true){
        f.Key("mimeType");                
        f.Value(value.mimeType.ToString().ToLower());
    }

    if(true){
        f.Key("targetEngine");                
        f.Value(value.targetEngine.ToString().ToLower());
    }

    if(value.source>=0){
        f.Key("source");                
        f.Value(value.source);
    }

    f.EndMap();
}

} // VciSerializer
} // VCI
