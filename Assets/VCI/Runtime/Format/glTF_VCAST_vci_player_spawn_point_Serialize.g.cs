
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_player_spawn_point_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_player_spawn_point value)
{
    f.BeginMap();


    if(value.playerSpawnPoint!=null){
        f.Key("playerSpawnPoint");                
        Serialize_vci_playerSpawnPoint(f, value.playerSpawnPoint);
    }

    f.EndMap();
}

public static void Serialize_vci_playerSpawnPoint(JsonFormatter f, glTF_VCAST_vci_PlayerSpawnPoint value)
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

} // VciSerializer
} // VCI
