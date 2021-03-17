
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_animation_Deserializer
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

public static List<VCI.glTF_VCAST_vci_animationReference> glTF_VCAST_vci_animation_Deserializevci_animationReferences(JsonNode parsed)
{
    var value = new List<glTF_VCAST_vci_animationReference>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_VCAST_vci_animation_Deserializevci_animationReferences_ITEM(x));
    }
	return value;
}
public static glTF_VCAST_vci_animationReference glTF_VCAST_vci_animation_Deserializevci_animationReferences_ITEM(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_animationReference();

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

} // VciDeserializer
} // VCI
