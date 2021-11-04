using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_player_spawn_point_restriction_Deserializer
    {


public static glTF_VCAST_vci_player_spawn_point_restriction Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_player_spawn_point_restriction();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="playerSpawnPointRestriction"){
            value.playerSpawnPointRestriction = glTF_VCAST_vci_player_spawn_point_restriction_Deserializevci_playerSpawnPointRestriction(kv.Value);
            continue;
        }

    }
    return value;
}

public static PlayerSpawnPointRestrictionJsonObject glTF_VCAST_vci_player_spawn_point_restriction_Deserializevci_playerSpawnPointRestriction(JsonNode parsed)
{
    var value = new PlayerSpawnPointRestrictionJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="rangeOfMovementRestriction"){
            value.rangeOfMovementRestriction = kv.Value.GetString();
            continue;
        }

        if(key=="limitRadius"){
            value.limitRadius = kv.Value.GetSingle();
            continue;
        }

        if(key=="limitRectLeft"){
            value.limitRectLeft = kv.Value.GetSingle();
            continue;
        }

        if(key=="limitRectRight"){
            value.limitRectRight = kv.Value.GetSingle();
            continue;
        }

        if(key=="limitRectForward"){
            value.limitRectForward = kv.Value.GetSingle();
            continue;
        }

        if(key=="limitRectBackward"){
            value.limitRectBackward = kv.Value.GetSingle();
            continue;
        }

        if(key=="postureRestriction"){
            value.postureRestriction = kv.Value.GetString();
            continue;
        }

        if(key=="seatHeight"){
            value.seatHeight = kv.Value.GetSingle();
            continue;
        }

    }
    return value;
}

    } // class
} // namespace
