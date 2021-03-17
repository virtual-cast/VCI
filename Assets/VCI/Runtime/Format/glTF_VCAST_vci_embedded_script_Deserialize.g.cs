
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_embedded_script_Deserializer
{


public static glTF_VCAST_vci_embedded_script Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_embedded_script();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="scripts"){
            value.scripts = glTF_VCAST_vci_embedded_script_Deserializevci_scripts(kv.Value);
            continue;
        }

        if(key=="entryPoint"){
            value.entryPoint = kv.Value.GetInt32();
            continue;
        }

    }
    return value;
}

public static List<VCI.glTF_VCAST_vci_embedded_script_source> glTF_VCAST_vci_embedded_script_Deserializevci_scripts(JsonNode parsed)
{
    var value = new List<glTF_VCAST_vci_embedded_script_source>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_VCAST_vci_embedded_script_Deserializevci_scripts_ITEM(x));
    }
	return value;
}
public static glTF_VCAST_vci_embedded_script_source glTF_VCAST_vci_embedded_script_Deserializevci_scripts_ITEM(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_embedded_script_source();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="name"){
            value.name = kv.Value.GetString();
            continue;
        }

        if(key=="mimeType"){
            value.mimeType = (ScriptMimeType)Enum.Parse(typeof(ScriptMimeType), kv.Value.GetString(), true);
            continue;
        }

        if(key=="targetEngine"){
            value.targetEngine = (TargetEngine)Enum.Parse(typeof(TargetEngine), kv.Value.GetString(), true);
            continue;
        }

        if(key=="source"){
            value.source = kv.Value.GetInt32();
            continue;
        }

    }
    return value;
}

} // VciDeserializer
} // VCI
