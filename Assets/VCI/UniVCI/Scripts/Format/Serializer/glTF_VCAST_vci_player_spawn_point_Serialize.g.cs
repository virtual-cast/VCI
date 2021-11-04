using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_player_spawn_point_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_player_spawn_point value)
{
    f.BeginMap();


    if(value.playerSpawnPoint!=null){
        f.Key("playerSpawnPoint");
        glTF_VCAST_vci_player_spawn_point_Serializevci_playerSpawnPoint(f, value.playerSpawnPoint);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_player_spawn_point_Serializevci_playerSpawnPoint(JsonFormatter f, PlayerSpawnPointJsonObject value)
{
    f.BeginMap();


    if(true){
        f.Key("order");
        f.Value(value.order);
    }

    if(true){
        f.Key("radius");
        f.Value(value.radius);
    }

    f.EndMap();
}

    } // class
} // namespace
