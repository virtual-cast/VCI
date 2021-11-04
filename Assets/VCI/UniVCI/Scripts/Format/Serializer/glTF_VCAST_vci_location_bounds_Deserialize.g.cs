using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_location_bounds_Deserializer
    {


public static glTF_VCAST_vci_location_bounds Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_location_bounds();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="LocationBounds"){
            value.LocationBounds = glTF_VCAST_vci_location_bounds_Deserializevci_LocationBounds(kv.Value);
            continue;
        }

    }
    return value;
}

public static LocationBoundsJsonObject glTF_VCAST_vci_location_bounds_Deserializevci_LocationBounds(JsonNode parsed)
{
    var value = new LocationBoundsJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="bounds_center"){
            value.bounds_center = glTF_VCAST_vci_location_bounds_Deserializevci_LocationBounds_bounds_center(kv.Value);
            continue;
        }

        if(key=="bounds_size"){
            value.bounds_size = glTF_VCAST_vci_location_bounds_Deserializevci_LocationBounds_bounds_size(kv.Value);
            continue;
        }

    }
    return value;
}

public static Vector3 glTF_VCAST_vci_location_bounds_Deserializevci_LocationBounds_bounds_center(JsonNode parsed)
{
    var value = new Vector3();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="x"){
            value.x = kv.Value.GetSingle();
            continue;
        }

        if(key=="y"){
            value.y = kv.Value.GetSingle();
            continue;
        }

        if(key=="z"){
            value.z = kv.Value.GetSingle();
            continue;
        }

    }
    return value;
}

public static Vector3 glTF_VCAST_vci_location_bounds_Deserializevci_LocationBounds_bounds_size(JsonNode parsed)
{
    var value = new Vector3();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="x"){
            value.x = kv.Value.GetSingle();
            continue;
        }

        if(key=="y"){
            value.y = kv.Value.GetSingle();
            continue;
        }

        if(key=="z"){
            value.z = kv.Value.GetSingle();
            continue;
        }

    }
    return value;
}

    } // class
} // namespace
