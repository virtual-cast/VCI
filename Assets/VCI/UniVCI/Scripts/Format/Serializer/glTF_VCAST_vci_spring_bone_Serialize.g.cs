using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_spring_bone_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_spring_bone value)
{
    f.BeginMap();


    if(value.springBones!=null&&value.springBones.Count>=1){
        f.Key("springBones");
        glTF_VCAST_vci_spring_bone_Serializevci_springBones(f, value.springBones);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_spring_bone_Serializevci_springBones(JsonFormatter f, List<SpringBoneJsonObject> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    glTF_VCAST_vci_spring_bone_Serializevci_springBones_ITEM(f, item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_spring_bone_Serializevci_springBones_ITEM(JsonFormatter f, SpringBoneJsonObject value)
{
    f.BeginMap();


    if(true){
        f.Key("stiffiness");
        f.Value(value.stiffiness);
    }

    if(true){
        f.Key("gravityPower");
        f.Value(value.gravityPower);
    }

    if(value.gravityDir!=null){
        f.Key("gravityDir");
        glTF_VCAST_vci_spring_bone_Serializevci_springBones__gravityDir(f, value.gravityDir);
    }

    if(true){
        f.Key("dragForce");
        f.Value(value.dragForce);
    }

    if(true){
        f.Key("center");
        f.Value(value.center);
    }

    if(true){
        f.Key("hitRadius");
        f.Value(value.hitRadius);
    }

    if(value.bones!=null&&value.bones.Length>=0){
        f.Key("bones");
        glTF_VCAST_vci_spring_bone_Serializevci_springBones__bones(f, value.bones);
    }

    if(value.colliderIds!=null&&value.colliderIds.Length>=0){
        f.Key("colliderIds");
        glTF_VCAST_vci_spring_bone_Serializevci_springBones__colliderIds(f, value.colliderIds);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_spring_bone_Serializevci_springBones__gravityDir(JsonFormatter f, Vector3 value)
{
    f.BeginMap();


    if(true){
        f.Key("x");
        f.Value(value.x);
    }

    if(true){
        f.Key("y");
        f.Value(value.y);
    }

    if(true){
        f.Key("z");
        f.Value(value.z);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_spring_bone_Serializevci_springBones__bones(JsonFormatter f, Int32[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_spring_bone_Serializevci_springBones__colliderIds(JsonFormatter f, Int32[] value)
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
