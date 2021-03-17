
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_player_spawn_point_Deserializer
{


public static glTF_VCAST_vci_player_spawn_point Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_player_spawn_point();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="playerSpawnPoint"){
            value.playerSpawnPoint = glTF_VCAST_vci_player_spawn_point_Deserializevci_playerSpawnPoint(kv.Value);
            continue;
        }

    }
    return value;
}

public static glTF_VCAST_vci_PlayerSpawnPoint glTF_VCAST_vci_player_spawn_point_Deserializevci_playerSpawnPoint(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_PlayerSpawnPoint();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="order"){
            value.order = kv.Value.GetInt32();
            continue;
        }

        if(key=="radius"){
            value.radius = kv.Value.GetSingle();
            continue;
        }

    }
    return value;
}

} // VciDeserializer
} // VCI
