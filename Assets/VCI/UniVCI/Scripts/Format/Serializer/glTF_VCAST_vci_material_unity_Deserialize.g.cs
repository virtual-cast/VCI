using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_material_unity_Deserializer
    {


public static glTF_VCAST_vci_material_unity Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_material_unity();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="materials"){
            value.materials = glTF_VCAST_vci_material_unity_Deserializevci_materials(kv.Value);
            continue;
        }

    }
    return value;
}

public static List<VCI.VciMaterialJsonObject> glTF_VCAST_vci_material_unity_Deserializevci_materials(JsonNode parsed)
{
    var value = new List<VciMaterialJsonObject>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_VCAST_vci_material_unity_Deserializevci_materials_ITEM(x));
    }
	return value;
}
public static VciMaterialJsonObject glTF_VCAST_vci_material_unity_Deserializevci_materials_ITEM(JsonNode parsed)
{
    var value = new VciMaterialJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="name"){
            value.name = kv.Value.GetString();
            continue;
        }

        if(key=="shader"){
            value.shader = kv.Value.GetString();
            continue;
        }

        if(key=="renderQueue"){
            value.renderQueue = kv.Value.GetInt32();
            continue;
        }

        if(key=="floatProperties"){
            value.floatProperties = glTF_VCAST_vci_material_unity_Deserializevci_materials__floatProperties(kv.Value);
            continue;
        }

        if(key=="vectorProperties"){
            value.vectorProperties = glTF_VCAST_vci_material_unity_Deserializevci_materials__vectorProperties(kv.Value);
            continue;
        }

        if(key=="textureProperties"){
            value.textureProperties = glTF_VCAST_vci_material_unity_Deserializevci_materials__textureProperties(kv.Value);
            continue;
        }

        if(key=="keywordMap"){
            value.keywordMap = glTF_VCAST_vci_material_unity_Deserializevci_materials__keywordMap(kv.Value);
            continue;
        }

        if(key=="tagMap"){
            value.tagMap = glTF_VCAST_vci_material_unity_Deserializevci_materials__tagMap(kv.Value);
            continue;
        }

    }
    return value;
}

 
public static Dictionary<String, Single> glTF_VCAST_vci_material_unity_Deserializevci_materials__floatProperties(JsonNode parsed)
{
    var value = new Dictionary<string, Single>();
    foreach(var kv in parsed.ObjectItems())
    {
        value.Add(kv.Key.GetString(), kv.Value.GetSingle());
    }
	return value;
}

 
public static Dictionary<String, Single[]> glTF_VCAST_vci_material_unity_Deserializevci_materials__vectorProperties(JsonNode parsed)
{
    var value = new Dictionary<string, Single[]>();
    foreach(var kv in parsed.ObjectItems())
    {
        value.Add(kv.Key.GetString(), glTF_VCAST_vci_material_unity_Deserializevci_materials__vectorProperties_ITEM(kv.Value));
    }
	return value;
}

public static Single[] glTF_VCAST_vci_material_unity_Deserializevci_materials__vectorProperties_ITEM(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

 
public static Dictionary<String, Int32> glTF_VCAST_vci_material_unity_Deserializevci_materials__textureProperties(JsonNode parsed)
{
    var value = new Dictionary<string, Int32>();
    foreach(var kv in parsed.ObjectItems())
    {
        value.Add(kv.Key.GetString(), kv.Value.GetInt32());
    }
	return value;
}

 
public static Dictionary<String, Boolean> glTF_VCAST_vci_material_unity_Deserializevci_materials__keywordMap(JsonNode parsed)
{
    var value = new Dictionary<string, Boolean>();
    foreach(var kv in parsed.ObjectItems())
    {
        value.Add(kv.Key.GetString(), kv.Value.GetBoolean());
    }
	return value;
}

 
public static Dictionary<String, String> glTF_VCAST_vci_material_unity_Deserializevci_materials__tagMap(JsonNode parsed)
{
    var value = new Dictionary<string, String>();
    foreach(var kv in parsed.ObjectItems())
    {
        value.Add(kv.Key.GetString(), kv.Value.GetString());
    }
	return value;
}

    } // class
} // namespace
