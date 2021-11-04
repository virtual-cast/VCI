using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_Effekseer_emitters_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_Effekseer_emitters value)
{
    f.BeginMap();


    if(value.emitters!=null&&value.emitters.Count>=1){
        f.Key("emitters");
        glTF_Effekseer_emitters_Serializevci_emitters(f, value.emitters);
    }

    f.EndMap();
}

public static void glTF_Effekseer_emitters_Serializevci_emitters(JsonFormatter f, List<EffekseerEmitterJsonObject> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    glTF_Effekseer_emitters_Serializevci_emitters_ITEM(f, item);

    }
    f.EndList();
}

public static void glTF_Effekseer_emitters_Serializevci_emitters_ITEM(JsonFormatter f, EffekseerEmitterJsonObject value)
{
    f.BeginMap();


    if(value.effectIndex>=0){
        f.Key("effectIndex");
        f.Value(value.effectIndex);
    }

    if(true){
        f.Key("isPlayOnStart");
        f.Value(value.isPlayOnStart);
    }

    if(true){
        f.Key("isLoop");
        f.Value(value.isLoop);
    }

    f.EndMap();
}

    } // class
} // namespace
