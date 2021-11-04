using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_player_spawn_point_Deserializer
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

public static PlayerSpawnPointJsonObject glTF_VCAST_vci_player_spawn_point_Deserializevci_playerSpawnPoint(JsonNode parsed)
{
    var value = new PlayerSpawnPointJsonObject();

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

    } // class
} // namespace
