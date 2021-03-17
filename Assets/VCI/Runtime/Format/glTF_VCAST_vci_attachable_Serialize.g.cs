
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_attachable_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_attachable value)
{
    f.BeginMap();


    if(value.attachableHumanBodyBones!=null&&value.attachableHumanBodyBones.Count>=0){
        f.Key("attachableHumanBodyBones");                
        Serialize_vci_attachableHumanBodyBones(f, value.attachableHumanBodyBones);
    }

    if(true){
        f.Key("attachableDistance");                
        f.Value(value.attachableDistance);
    }

    if(true){
        f.Key("scalable");                
        f.Value(value.scalable);
    }

    if(value.offset!=null){
        f.Key("offset");                
        Serialize_vci_offset(f, value.offset);
    }

    f.EndMap();
}

public static void Serialize_vci_attachableHumanBodyBones(JsonFormatter f, List<String> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void Serialize_vci_offset(JsonFormatter f, Vector3 value)
{
    f.BeginMap();


    if(true){
        f.Key("x");                
        f.Value(value.x);
    }

    if(true){
        f.Key("y");                
        f.Value(value.y);
    }

    if(true){
        f.Key("z");                
        f.Value(value.z);
    }

    f.EndMap();
}

} // VciSerializer
} // VCI
