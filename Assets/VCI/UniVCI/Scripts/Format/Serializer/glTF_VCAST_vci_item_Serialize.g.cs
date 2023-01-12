using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_item_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_item value)
{
    f.BeginMap();


    if(true){
        f.Key("grabbable");
        f.Value(value.grabbable);
    }

    if(true){
        f.Key("scalable");
        f.Value(value.scalable);
    }

    if(true){
        f.Key("uniformScaling");
        f.Value(value.uniformScaling);
    }

    if(true){
        f.Key("attractable");
        f.Value(value.attractable);
    }

    if(true){
        f.Key("attractableDistance");
        f.Value(value.attractableDistance);
    }

    if(true){
        f.Key("groupId");
        f.Value(value.groupId);
    }

    if(true){
        f.Key("key");
        f.Value(value.key);
    }

    f.EndMap();
}

    } // class
} // namespace
