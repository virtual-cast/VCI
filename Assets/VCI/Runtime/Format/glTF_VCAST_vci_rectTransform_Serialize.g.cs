
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_rectTransform_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_rectTransform value)
{
    f.BeginMap();


    if(value.rectTransform!=null){
        f.Key("rectTransform");                
        Serialize_vci_rectTransform(f, value.rectTransform);
    }

    f.EndMap();
}

public static void Serialize_vci_rectTransform(JsonFormatter f, glTF_VCAST_vci_RectTransform value)
{
    f.BeginMap();


    if(value.anchorMin!=null&&value.anchorMin.Length>=2){
        f.Key("anchorMin");                
        Serialize_vci_rectTransform_anchorMin(f, value.anchorMin);
    }

    if(value.anchorMax!=null&&value.anchorMax.Length>=2){
        f.Key("anchorMax");                
        Serialize_vci_rectTransform_anchorMax(f, value.anchorMax);
    }

    if(value.anchoredPosition!=null&&value.anchoredPosition.Length>=2){
        f.Key("anchoredPosition");                
        Serialize_vci_rectTransform_anchoredPosition(f, value.anchoredPosition);
    }

    if(value.sizeDelta!=null&&value.sizeDelta.Length>=2){
        f.Key("sizeDelta");                
        Serialize_vci_rectTransform_sizeDelta(f, value.sizeDelta);
    }

    if(value.pivot!=null&&value.pivot.Length>=2){
        f.Key("pivot");                
        Serialize_vci_rectTransform_pivot(f, value.pivot);
    }

    f.EndMap();
}

public static void Serialize_vci_rectTransform_anchorMin(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void Serialize_vci_rectTransform_anchorMax(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void Serialize_vci_rectTransform_anchoredPosition(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void Serialize_vci_rectTransform_sizeDelta(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void Serialize_vci_rectTransform_pivot(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

} // VciSerializer
} // VCI
