using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_materials_pbr_Deserializer
    {


public static glTF_VCAST_materials_pbr Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_materials_pbr();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="emissiveFactor"){
            value.emissiveFactor = glTF_VCAST_materials_pbr_Deserializevci_emissiveFactor(kv.Value);
            continue;
        }

    }
    return value;
}

public static Single[] glTF_VCAST_materials_pbr_Deserializevci_emissiveFactor(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

    } // class
} // namespace
