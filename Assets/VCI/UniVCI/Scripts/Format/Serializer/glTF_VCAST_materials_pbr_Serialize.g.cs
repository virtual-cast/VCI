using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_materials_pbr_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_VCAST_materials_pbr value)
{
    f.BeginMap();


    if(value.emissiveFactor!=null&&value.emissiveFactor.Length>=0){
        f.Key("emissiveFactor");
        glTF_VCAST_materials_pbr_Serializevci_emissiveFactor(f, value.emissiveFactor);
    }

    f.EndMap();
}

public static void glTF_VCAST_materials_pbr_Serializevci_emissiveFactor(JsonFormatter f, Single[] value)
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
