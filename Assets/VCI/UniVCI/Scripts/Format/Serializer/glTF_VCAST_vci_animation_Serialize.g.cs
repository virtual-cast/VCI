using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_animation_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_animation value)
{
    f.BeginMap();


    if(value.animationReferences!=null&&value.animationReferences.Count>=1){
        f.Key("animationReferences");
        glTF_VCAST_vci_animation_Serializevci_animationReferences(f, value.animationReferences);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_animation_Serializevci_animationReferences(JsonFormatter f, List<AnimationReferenceJsonObject> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    glTF_VCAST_vci_animation_Serializevci_animationReferences_ITEM(f, item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_animation_Serializevci_animationReferences_ITEM(JsonFormatter f, AnimationReferenceJsonObject value)
{
    f.BeginMap();


    if(value.animation>=0){
        f.Key("animation");
        f.Value(value.animation);
    }

    f.EndMap();
}

    } // class
} // namespace
