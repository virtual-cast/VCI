using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_rigidbody_Deserializer
    {


public static glTF_VCAST_vci_rigidbody Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_rigidbody();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="rigidbodies"){
            value.rigidbodies = glTF_VCAST_vci_rigidbody_Deserializevci_rigidbodies(kv.Value);
            continue;
        }

    }
    return value;
}

public static List<VCI.RigidbodyJsonObject> glTF_VCAST_vci_rigidbody_Deserializevci_rigidbodies(JsonNode parsed)
{
    var value = new List<RigidbodyJsonObject>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_VCAST_vci_rigidbody_Deserializevci_rigidbodies_ITEM(x));
    }
	return value;
}
public static RigidbodyJsonObject glTF_VCAST_vci_rigidbody_Deserializevci_rigidbodies_ITEM(JsonNode parsed)
{
    var value = new RigidbodyJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="mass"){
            value.mass = kv.Value.GetSingle();
            continue;
        }

        if(key=="drag"){
            value.drag = kv.Value.GetSingle();
            continue;
        }

        if(key=="angularDrag"){
            value.angularDrag = kv.Value.GetSingle();
            continue;
        }

        if(key=="useGravity"){
            value.useGravity = kv.Value.GetBoolean();
            continue;
        }

        if(key=="isKinematic"){
            value.isKinematic = kv.Value.GetBoolean();
            continue;
        }

        if(key=="interpolate"){
            value.interpolate = kv.Value.GetString();
            continue;
        }

        if(key=="collisionDetection"){
            value.collisionDetection = kv.Value.GetString();
            continue;
        }

        if(key=="freezePositionX"){
            value.freezePositionX = kv.Value.GetBoolean();
            continue;
        }

        if(key=="freezePositionY"){
            value.freezePositionY = kv.Value.GetBoolean();
            continue;
        }

        if(key=="freezePositionZ"){
            value.freezePositionZ = kv.Value.GetBoolean();
            continue;
        }

        if(key=="freezeRotationX"){
            value.freezeRotationX = kv.Value.GetBoolean();
            continue;
        }

        if(key=="freezeRotationY"){
            value.freezeRotationY = kv.Value.GetBoolean();
            continue;
        }

        if(key=="freezeRotationZ"){
            value.freezeRotationZ = kv.Value.GetBoolean();
            continue;
        }

    }
    return value;
}

    } // class
} // namespace
