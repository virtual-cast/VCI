using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_colliders_Deserializer
    {


public static glTF_VCAST_vci_colliders Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_colliders();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="colliders"){
            value.colliders = glTF_VCAST_vci_colliders_Deserializevci_colliders(kv.Value);
            continue;
        }

    }
    return value;
}

public static List<VCI.ColliderJsonObject> glTF_VCAST_vci_colliders_Deserializevci_colliders(JsonNode parsed)
{
    var value = new List<ColliderJsonObject>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_VCAST_vci_colliders_Deserializevci_colliders_ITEM(x));
    }
	return value;
}
public static ColliderJsonObject glTF_VCAST_vci_colliders_Deserializevci_colliders_ITEM(JsonNode parsed)
{
    var value = new ColliderJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="type"){
            value.type = kv.Value.GetString();
            continue;
        }

        if(key=="layer"){
            value.layer = kv.Value.GetString();
            continue;
        }

        if(key=="center"){
            value.center = glTF_VCAST_vci_colliders_Deserializevci_colliders__center(kv.Value);
            continue;
        }

        if(key=="shape"){
            value.shape = glTF_VCAST_vci_colliders_Deserializevci_colliders__shape(kv.Value);
            continue;
        }

        if(key=="mesh"){
            value.mesh = glTF_VCAST_vci_colliders_Deserializevci_colliders__mesh(kv.Value);
            continue;
        }

        if(key=="isTrigger"){
            value.isTrigger = kv.Value.GetBoolean();
            continue;
        }

        if(key=="physicMaterial"){
            value.physicMaterial = glTF_VCAST_vci_colliders_Deserializevci_colliders__physicMaterial(kv.Value);
            continue;
        }

    }
    return value;
}

public static Single[] glTF_VCAST_vci_colliders_Deserializevci_colliders__center(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_colliders_Deserializevci_colliders__shape(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static ColliderMeshJsonObject glTF_VCAST_vci_colliders_Deserializevci_colliders__mesh(JsonNode parsed)
{
    var value = new ColliderMeshJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="isConvex"){
            value.isConvex = kv.Value.GetBoolean();
            continue;
        }

        if(key=="position"){
            value.position = kv.Value.GetInt32();
            continue;
        }

        if(key=="indices"){
            value.indices = kv.Value.GetInt32();
            continue;
        }

    }
    return value;
}

public static PhysicMaterialJsonObject glTF_VCAST_vci_colliders_Deserializevci_colliders__physicMaterial(JsonNode parsed)
{
    var value = new PhysicMaterialJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="dynamicFriction"){
            value.dynamicFriction = kv.Value.GetSingle();
            continue;
        }

        if(key=="staticFriction"){
            value.staticFriction = kv.Value.GetSingle();
            continue;
        }

        if(key=="bounciness"){
            value.bounciness = kv.Value.GetSingle();
            continue;
        }

        if(key=="frictionCombine"){
            value.frictionCombine = kv.Value.GetString();
            continue;
        }

        if(key=="bounceCombine"){
            value.bounceCombine = kv.Value.GetString();
            continue;
        }

    }
    return value;
}

    } // class
} // namespace
