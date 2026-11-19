using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_lod_group_Deserializer
    {


public static glTF_VCAST_vci_lod_group Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_lod_group();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="lodGroup"){
            value.lodGroup = glTF_VCAST_vci_lod_group_Deserializevci_lodGroup(kv.Value);
            continue;
        }

    }
    return value;
}

public static LodGroupJsonObject glTF_VCAST_vci_lod_group_Deserializevci_lodGroup(JsonNode parsed)
{
    var value = new LodGroupJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="size"){
            value.size = kv.Value.GetSingle();
            continue;
        }

        if(key=="localReferencePoint"){
            value.localReferencePoint = glTF_VCAST_vci_lod_group_Deserializevci_lodGroup_localReferencePoint(kv.Value);
            continue;
        }

        if(key=="lods"){
            value.lods = glTF_VCAST_vci_lod_group_Deserializevci_lodGroup_lods(kv.Value);
            continue;
        }

    }
    return value;
}

public static Single[] glTF_VCAST_vci_lod_group_Deserializevci_lodGroup_localReferencePoint(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static List<VCI.LodJsonObject> glTF_VCAST_vci_lod_group_Deserializevci_lodGroup_lods(JsonNode parsed)
{
    var value = new List<LodJsonObject>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_VCAST_vci_lod_group_Deserializevci_lodGroup_lods_ITEM(x));
    }
	return value;
}
public static LodJsonObject glTF_VCAST_vci_lod_group_Deserializevci_lodGroup_lods_ITEM(JsonNode parsed)
{
    var value = new LodJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="screenRelativeTransitionHeight"){
            value.screenRelativeTransitionHeight = kv.Value.GetSingle();
            continue;
        }

        if(key=="renderers"){
            value.renderers = glTF_VCAST_vci_lod_group_Deserializevci_lodGroup_lods__renderers(kv.Value);
            continue;
        }

    }
    return value;
}

public static Int32[] glTF_VCAST_vci_lod_group_Deserializevci_lodGroup_lods__renderers(JsonNode parsed)
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
