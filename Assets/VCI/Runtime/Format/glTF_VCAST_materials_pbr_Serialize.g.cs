
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_materials_pbr_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_materials_pbr value)
{
    f.BeginMap();


    if(value.emissiveFactor!=null&&value.emissiveFactor.Length>=0){
        f.Key("emissiveFactor");                
        Serialize_vci_emissiveFactor(f, value.emissiveFactor);
    }

    f.EndMap();
}

public static void Serialize_vci_emissiveFactor(JsonFormatter f, Single[] value)
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
