using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_item_Deserializer
    {


public static glTF_VCAST_vci_item Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_item();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="grabbable"){
            value.grabbable = kv.Value.GetBoolean();
            continue;
        }

        if(key=="scalable"){
            value.scalable = kv.Value.GetBoolean();
            continue;
        }

        if(key=="uniformScaling"){
            value.uniformScaling = kv.Value.GetBoolean();
            continue;
        }

        if(key=="attractable"){
            value.attractable = kv.Value.GetBoolean();
            continue;
        }

        if(key=="groupId"){
            value.groupId = kv.Value.GetInt32();
            continue;
        }

    }
    return value;
}

    } // class
} // namespace
