using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_colliders_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_colliders value)
{
    f.BeginMap();


    if(value.colliders!=null&&value.colliders.Count>=1){
        f.Key("colliders");
        glTF_VCAST_vci_colliders_Serializevci_colliders(f, value.colliders);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_colliders_Serializevci_colliders(JsonFormatter f, List<ColliderJsonObject> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    glTF_VCAST_vci_colliders_Serializevci_colliders_ITEM(f, item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_colliders_Serializevci_colliders_ITEM(JsonFormatter f, ColliderJsonObject value)
{
    f.BeginMap();


    if(!string.IsNullOrEmpty(value.type)){
        f.Key("type");
        f.Value(value.type);
    }

    if(!string.IsNullOrEmpty(value.layer)){
        f.Key("layer");
        f.Value(value.layer);
    }

    if(value.center!=null&&value.center.Length>=3){
        f.Key("center");
        glTF_VCAST_vci_colliders_Serializevci_colliders__center(f, value.center);
    }

    if(value.shape!=null&&value.shape.Length>=0){
        f.Key("shape");
        glTF_VCAST_vci_colliders_Serializevci_colliders__shape(f, value.shape);
    }

    if(value.mesh!=null){
        f.Key("mesh");
        glTF_VCAST_vci_colliders_Serializevci_colliders__mesh(f, value.mesh);
    }

    if(true){
        f.Key("isTrigger");
        f.Value(value.isTrigger);
    }

    if(value.physicMaterial!=null){
        f.Key("physicMaterial");
        glTF_VCAST_vci_colliders_Serializevci_colliders__physicMaterial(f, value.physicMaterial);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_colliders_Serializevci_colliders__center(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_colliders_Serializevci_colliders__shape(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_colliders_Serializevci_colliders__mesh(JsonFormatter f, ColliderMeshJsonObject value)
{
    f.BeginMap();


    if(true){
        f.Key("isConvex");
        f.Value(value.isConvex);
    }

    if(value.position>=0){
        f.Key("position");
        f.Value(value.position);
    }

    if(value.indices>=0){
        f.Key("indices");
        f.Value(value.indices);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_colliders_Serializevci_colliders__physicMaterial(JsonFormatter f, PhysicMaterialJsonObject value)
{
    f.BeginMap();


    if(true){
        f.Key("dynamicFriction");
        f.Value(value.dynamicFriction);
    }

    if(true){
        f.Key("staticFriction");
        f.Value(value.staticFriction);
    }

    if(true){
        f.Key("bounciness");
        f.Value(value.bounciness);
    }

    if(!string.IsNullOrEmpty(value.frictionCombine)){
        f.Key("frictionCombine");
        f.Value(value.frictionCombine);
    }

    if(!string.IsNullOrEmpty(value.bounceCombine)){
        f.Key("bounceCombine");
        f.Value(value.bounceCombine);
    }

    f.EndMap();
}

    } // class
} // namespace
