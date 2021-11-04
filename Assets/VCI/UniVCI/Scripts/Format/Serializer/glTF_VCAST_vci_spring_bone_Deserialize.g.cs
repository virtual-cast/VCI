using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_spring_bone_Deserializer
    {


public static glTF_VCAST_vci_spring_bone Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_spring_bone();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="springBones"){
            value.springBones = glTF_VCAST_vci_spring_bone_Deserializevci_springBones(kv.Value);
            continue;
        }

    }
    return value;
}

public static List<VCI.SpringBoneJsonObject> glTF_VCAST_vci_spring_bone_Deserializevci_springBones(JsonNode parsed)
{
    var value = new List<SpringBoneJsonObject>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_VCAST_vci_spring_bone_Deserializevci_springBones_ITEM(x));
    }
	return value;
}
public static SpringBoneJsonObject glTF_VCAST_vci_spring_bone_Deserializevci_springBones_ITEM(JsonNode parsed)
{
    var value = new SpringBoneJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="stiffiness"){
            value.stiffiness = kv.Value.GetSingle();
            continue;
        }

        if(key=="gravityPower"){
            value.gravityPower = kv.Value.GetSingle();
            continue;
        }

        if(key=="gravityDir"){
            value.gravityDir = glTF_VCAST_vci_spring_bone_Deserializevci_springBones__gravityDir(kv.Value);
            continue;
        }

        if(key=="dragForce"){
            value.dragForce = kv.Value.GetSingle();
            continue;
        }

        if(key=="center"){
            value.center = kv.Value.GetInt32();
            continue;
        }

        if(key=="hitRadius"){
            value.hitRadius = kv.Value.GetSingle();
            continue;
        }

        if(key=="bones"){
            value.bones = glTF_VCAST_vci_spring_bone_Deserializevci_springBones__bones(kv.Value);
            continue;
        }

        if(key=="colliderIds"){
            value.colliderIds = glTF_VCAST_vci_spring_bone_Deserializevci_springBones__colliderIds(kv.Value);
            continue;
        }

    }
    return value;
}

public static Vector3 glTF_VCAST_vci_spring_bone_Deserializevci_springBones__gravityDir(JsonNode parsed)
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

public static Int32[] glTF_VCAST_vci_spring_bone_Deserializevci_springBones__bones(JsonNode parsed)
{
    var value = new Int32[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetInt32();
    }
	return value;
} 

public static Int32[] glTF_VCAST_vci_spring_bone_Deserializevci_springBones__colliderIds(JsonNode parsed)
{
    var value = new Int32[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetInt32();
    }
	return value;
} 

    } // class
} // namespace
