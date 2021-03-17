
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_location_bounds_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_location_bounds value)
{
    f.BeginMap();


    if(value.LocationBounds!=null){
        f.Key("LocationBounds");                
        Serialize_vci_LocationBounds(f, value.LocationBounds);
    }

    f.EndMap();
}

public static void Serialize_vci_LocationBounds(JsonFormatter f, glTF_VCAST_vci_LocationBounds value)
{
    f.BeginMap();


    if(value.bounds_center!=null){
        f.Key("bounds_center");                
        Serialize_vci_LocationBounds_bounds_center(f, value.bounds_center);
    }

    if(value.bounds_size!=null){
        f.Key("bounds_size");                
        Serialize_vci_LocationBounds_bounds_size(f, value.bounds_size);
    }

    f.EndMap();
}

public static void Serialize_vci_LocationBounds_bounds_center(JsonFormatter f, Vector3 value)
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

public static void Serialize_vci_LocationBounds_bounds_size(JsonFormatter f, Vector3 value)
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
