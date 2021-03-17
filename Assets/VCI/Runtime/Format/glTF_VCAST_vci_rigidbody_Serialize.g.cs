
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_rigidbody_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_rigidbody value)
{
    f.BeginMap();


    if(value.rigidbodies!=null&&value.rigidbodies.Count>=1){
        f.Key("rigidbodies");                
        Serialize_vci_rigidbodies(f, value.rigidbodies);
    }

    f.EndMap();
}

public static void Serialize_vci_rigidbodies(JsonFormatter f, List<glTF_VCAST_vci_Rigidbody> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    Serialize_vci_rigidbodies_ITEM(f, item);

    }
    f.EndList();
}

public static void Serialize_vci_rigidbodies_ITEM(JsonFormatter f, glTF_VCAST_vci_Rigidbody value)
{
    f.BeginMap();


    if(true){
        f.Key("mass");                
        f.Value(value.mass);
    }

    if(true){
        f.Key("drag");                
        f.Value(value.drag);
    }

    if(true){
        f.Key("angularDrag");                
        f.Value(value.angularDrag);
    }

    if(true){
        f.Key("useGravity");                
        f.Value(value.useGravity);
    }

    if(true){
        f.Key("isKinematic");                
        f.Value(value.isKinematic);
    }

    if(!string.IsNullOrEmpty(value.interpolate)){
        f.Key("interpolate");                
        f.Value(value.interpolate);
    }

    if(!string.IsNullOrEmpty(value.collisionDetection)){
        f.Key("collisionDetection");                
        f.Value(value.collisionDetection);
    }

    if(true){
        f.Key("freezePositionX");                
        f.Value(value.freezePositionX);
    }

    if(true){
        f.Key("freezePositionY");                
        f.Value(value.freezePositionY);
    }

    if(true){
        f.Key("freezePositionZ");                
        f.Value(value.freezePositionZ);
    }

    if(true){
        f.Key("freezeRotationX");                
        f.Value(value.freezeRotationX);
    }

    if(true){
        f.Key("freezeRotationY");                
        f.Value(value.freezeRotationY);
    }

    if(true){
        f.Key("freezeRotationZ");                
        f.Value(value.freezeRotationZ);
    }

    f.EndMap();
}

} // VciSerializer
} // VCI
