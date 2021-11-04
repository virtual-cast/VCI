using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_rectTransform_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_rectTransform value)
{
    f.BeginMap();


    if(value.rectTransform!=null){
        f.Key("rectTransform");
        glTF_VCAST_vci_rectTransform_Serializevci_rectTransform(f, value.rectTransform);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_rectTransform_Serializevci_rectTransform(JsonFormatter f, RectTransformJsonObject value)
{
    f.BeginMap();


    if(value.anchorMin!=null&&value.anchorMin.Length>=2){
        f.Key("anchorMin");
        glTF_VCAST_vci_rectTransform_Serializevci_rectTransform_anchorMin(f, value.anchorMin);
    }

    if(value.anchorMax!=null&&value.anchorMax.Length>=2){
        f.Key("anchorMax");
        glTF_VCAST_vci_rectTransform_Serializevci_rectTransform_anchorMax(f, value.anchorMax);
    }

    if(value.anchoredPosition!=null&&value.anchoredPosition.Length>=2){
        f.Key("anchoredPosition");
        glTF_VCAST_vci_rectTransform_Serializevci_rectTransform_anchoredPosition(f, value.anchoredPosition);
    }

    if(value.sizeDelta!=null&&value.sizeDelta.Length>=2){
        f.Key("sizeDelta");
        glTF_VCAST_vci_rectTransform_Serializevci_rectTransform_sizeDelta(f, value.sizeDelta);
    }

    if(value.pivot!=null&&value.pivot.Length>=2){
        f.Key("pivot");
        glTF_VCAST_vci_rectTransform_Serializevci_rectTransform_pivot(f, value.pivot);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_rectTransform_Serializevci_rectTransform_anchorMin(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_rectTransform_Serializevci_rectTransform_anchorMax(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_rectTransform_Serializevci_rectTransform_anchoredPosition(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_rectTransform_Serializevci_rectTransform_sizeDelta(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_rectTransform_Serializevci_rectTransform_pivot(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

    } // class
} // namespace
