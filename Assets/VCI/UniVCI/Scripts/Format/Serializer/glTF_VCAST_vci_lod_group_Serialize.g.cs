using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_lod_group_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_lod_group value)
{
    f.BeginMap();


    if(value.lodGroup!=null){
        f.Key("lodGroup");
        glTF_VCAST_vci_lod_group_Serializevci_lodGroup(f, value.lodGroup);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_lod_group_Serializevci_lodGroup(JsonFormatter f, LodGroupJsonObject value)
{
    f.BeginMap();


    if(true){
        f.Key("size");
        f.Value(value.size);
    }

    if(value.localReferencePoint!=null&&value.localReferencePoint.Length>=3){
        f.Key("localReferencePoint");
        glTF_VCAST_vci_lod_group_Serializevci_lodGroup_localReferencePoint(f, value.localReferencePoint);
    }

    if(value.lods!=null&&value.lods.Count>=1){
        f.Key("lods");
        glTF_VCAST_vci_lod_group_Serializevci_lodGroup_lods(f, value.lods);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_lod_group_Serializevci_lodGroup_localReferencePoint(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_lod_group_Serializevci_lodGroup_lods(JsonFormatter f, List<LodJsonObject> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    glTF_VCAST_vci_lod_group_Serializevci_lodGroup_lods_ITEM(f, item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_lod_group_Serializevci_lodGroup_lods_ITEM(JsonFormatter f, LodJsonObject value)
{
    f.BeginMap();


    if(true){
        f.Key("screenRelativeTransitionHeight");
        f.Value(value.screenRelativeTransitionHeight);
    }

    if(value.renderers!=null&&value.renderers.Length>=0){
        f.Key("renderers");
        glTF_VCAST_vci_lod_group_Serializevci_lodGroup_lods__renderers(f, value.renderers);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_lod_group_Serializevci_lodGroup_lods__renderers(JsonFormatter f, Int32[] value)
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
