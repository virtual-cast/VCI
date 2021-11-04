using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_attachable_Deserializer
    {


public static glTF_VCAST_vci_attachable Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_attachable();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="attachableHumanBodyBones"){
            value.attachableHumanBodyBones = glTF_VCAST_vci_attachable_Deserializevci_attachableHumanBodyBones(kv.Value);
            continue;
        }

        if(key=="attachableDistance"){
            value.attachableDistance = kv.Value.GetSingle();
            continue;
        }

        if(key=="scalable"){
            value.scalable = kv.Value.GetBoolean();
            continue;
        }

        if(key=="offset"){
            value.offset = glTF_VCAST_vci_attachable_Deserializevci_offset(kv.Value);
            continue;
        }

    }
    return value;
}

public static List<System.String> glTF_VCAST_vci_attachable_Deserializevci_attachableHumanBodyBones(JsonNode parsed)
{
    var value = new List<String>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(x.GetString());
    }
	return value;
}
public static Vector3 glTF_VCAST_vci_attachable_Deserializevci_offset(JsonNode parsed)
{
    var value = new Vector3();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="x"){
            value.x = kv.Value.GetSingle();
            continue;
        }

        if(key=="y"){
            value.y = kv.Value.GetSingle();
            continue;
        }

        if(key=="z"){
            value.z = kv.Value.GetSingle();
            continue;
        }

    }
    return value;
}

    } // class
} // namespace
