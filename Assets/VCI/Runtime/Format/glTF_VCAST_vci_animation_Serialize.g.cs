
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_animation_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_animation value)
{
    f.BeginMap();


    if(value.animationReferences!=null&&value.animationReferences.Count>=1){
        f.Key("animationReferences");                
        Serialize_vci_animationReferences(f, value.animationReferences);
    }

    f.EndMap();
}

public static void Serialize_vci_animationReferences(JsonFormatter f, List<glTF_VCAST_vci_animationReference> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    Serialize_vci_animationReferences_ITEM(f, item);

    }
    f.EndList();
}

public static void Serialize_vci_animationReferences_ITEM(JsonFormatter f, glTF_VCAST_vci_animationReference value)
{
    f.BeginMap();


    if(value.animation>=0){
        f.Key("animation");                
        f.Value(value.animation);
    }

    f.EndMap();
}

} // VciSerializer
} // VCI
