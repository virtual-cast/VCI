using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_rectTransform_Deserializer
    {


public static glTF_VCAST_vci_rectTransform Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_rectTransform();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="rectTransform"){
            value.rectTransform = glTF_VCAST_vci_rectTransform_Deserializevci_rectTransform(kv.Value);
            continue;
        }

    }
    return value;
}

public static RectTransformJsonObject glTF_VCAST_vci_rectTransform_Deserializevci_rectTransform(JsonNode parsed)
{
    var value = new RectTransformJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="anchorMin"){
            value.anchorMin = glTF_VCAST_vci_rectTransform_Deserializevci_rectTransform_anchorMin(kv.Value);
            continue;
        }

        if(key=="anchorMax"){
            value.anchorMax = glTF_VCAST_vci_rectTransform_Deserializevci_rectTransform_anchorMax(kv.Value);
            continue;
        }

        if(key=="anchoredPosition"){
            value.anchoredPosition = glTF_VCAST_vci_rectTransform_Deserializevci_rectTransform_anchoredPosition(kv.Value);
            continue;
        }

        if(key=="sizeDelta"){
            value.sizeDelta = glTF_VCAST_vci_rectTransform_Deserializevci_rectTransform_sizeDelta(kv.Value);
            continue;
        }

        if(key=="pivot"){
            value.pivot = glTF_VCAST_vci_rectTransform_Deserializevci_rectTransform_pivot(kv.Value);
            continue;
        }

    }
    return value;
}

public static Single[] glTF_VCAST_vci_rectTransform_Deserializevci_rectTransform_anchorMin(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_rectTransform_Deserializevci_rectTransform_anchorMax(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_rectTransform_Deserializevci_rectTransform_anchoredPosition(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_rectTransform_Deserializevci_rectTransform_sizeDelta(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_rectTransform_Deserializevci_rectTransform_pivot(JsonNode parsed)
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
