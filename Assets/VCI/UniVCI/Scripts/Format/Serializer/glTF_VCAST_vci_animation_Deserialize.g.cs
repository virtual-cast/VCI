using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_animation_Deserializer
    {


public static glTF_VCAST_vci_animation Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_animation();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="animationReferences"){
            value.animationReferences = glTF_VCAST_vci_animation_Deserializevci_animationReferences(kv.Value);
            continue;
        }

    }
    return value;
}

public static List<VCI.AnimationReferenceJsonObject> glTF_VCAST_vci_animation_Deserializevci_animationReferences(JsonNode parsed)
{
    var value = new List<AnimationReferenceJsonObject>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_VCAST_vci_animation_Deserializevci_animationReferences_ITEM(x));
    }
	return value;
}
public static AnimationReferenceJsonObject glTF_VCAST_vci_animation_Deserializevci_animationReferences_ITEM(JsonNode parsed)
{
    var value = new AnimationReferenceJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="animation"){
            value.animation = kv.Value.GetInt32();
            continue;
        }

    }
    return value;
}

    } // class
} // namespace
