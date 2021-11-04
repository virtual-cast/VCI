using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_Effekseer_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_Effekseer value)
{
    f.BeginMap();


    if(value.effects!=null&&value.effects.Count>=1){
        f.Key("effects");
        glTF_Effekseer_Serializevci_effects(f, value.effects);
    }

    f.EndMap();
}

public static void glTF_Effekseer_Serializevci_effects(JsonFormatter f, List<EffekseerEffectJsonObject> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    glTF_Effekseer_Serializevci_effects_ITEM(f, item);

    }
    f.EndList();
}

public static void glTF_Effekseer_Serializevci_effects_ITEM(JsonFormatter f, EffekseerEffectJsonObject value)
{
    f.BeginMap();


    if(true){
        f.Key("nodeIndex");
        f.Value(value.nodeIndex);
    }

    if(!string.IsNullOrEmpty(value.nodeName)){
        f.Key("nodeName");
        f.Value(value.nodeName);
    }

    if(!string.IsNullOrEmpty(value.effectName)){
        f.Key("effectName");
        f.Value(value.effectName);
    }

    if(value.body!=null){
        f.Key("body");
        glTF_Effekseer_Serializevci_effects__body(f, value.body);
    }

    if(true){
        f.Key("scale");
        f.Value(value.scale);
    }

    if(value.images!=null&&value.images.Count>=1){
        f.Key("images");
        glTF_Effekseer_Serializevci_effects__images(f, value.images);
    }

    if(value.models!=null&&value.models.Count>=1){
        f.Key("models");
        glTF_Effekseer_Serializevci_effects__models(f, value.models);
    }

    f.EndMap();
}

public static void glTF_Effekseer_Serializevci_effects__body(JsonFormatter f, EffekseerBodyJsonObject value)
{
    f.BeginMap();


    if(value.bufferView>=0){
        f.Key("bufferView");
        f.Value(value.bufferView);
    }

    f.EndMap();
}

public static void glTF_Effekseer_Serializevci_effects__images(JsonFormatter f, List<EffekseerImageJsonObject> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    glTF_Effekseer_Serializevci_effects__images_ITEM(f, item);

    }
    f.EndList();
}

public static void glTF_Effekseer_Serializevci_effects__images_ITEM(JsonFormatter f, EffekseerImageJsonObject value)
{
    f.BeginMap();


    if(value.bufferView>=0){
        f.Key("bufferView");
        f.Value(value.bufferView);
    }

    if(!string.IsNullOrEmpty(value.mimeType)){
        f.Key("mimeType");
        f.Value(value.mimeType);
    }

    f.EndMap();
}

public static void glTF_Effekseer_Serializevci_effects__models(JsonFormatter f, List<EffekseerModelJsonObject> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    glTF_Effekseer_Serializevci_effects__models_ITEM(f, item);

    }
    f.EndList();
}

public static void glTF_Effekseer_Serializevci_effects__models_ITEM(JsonFormatter f, EffekseerModelJsonObject value)
{
    f.BeginMap();


    if(value.bufferView>=0){
        f.Key("bufferView");
        f.Value(value.bufferView);
    }

    f.EndMap();
}

    } // class
} // namespace
