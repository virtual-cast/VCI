using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_Effekseer_Deserializer
    {


public static glTF_Effekseer Deserialize(JsonNode parsed)
{
    var value = new glTF_Effekseer();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="effects"){
            value.effects = glTF_Effekseer_Deserializevci_effects(kv.Value);
            continue;
        }

    }
    return value;
}

public static List<VCI.EffekseerEffectJsonObject> glTF_Effekseer_Deserializevci_effects(JsonNode parsed)
{
    var value = new List<EffekseerEffectJsonObject>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_Effekseer_Deserializevci_effects_ITEM(x));
    }
	return value;
}
public static EffekseerEffectJsonObject glTF_Effekseer_Deserializevci_effects_ITEM(JsonNode parsed)
{
    var value = new EffekseerEffectJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="nodeIndex"){
            value.nodeIndex = kv.Value.GetInt32();
            continue;
        }

        if(key=="nodeName"){
            value.nodeName = kv.Value.GetString();
            continue;
        }

        if(key=="effectName"){
            value.effectName = kv.Value.GetString();
            continue;
        }

        if(key=="body"){
            value.body = glTF_Effekseer_Deserializevci_effects__body(kv.Value);
            continue;
        }

        if(key=="scale"){
            value.scale = kv.Value.GetSingle();
            continue;
        }

        if(key=="images"){
            value.images = glTF_Effekseer_Deserializevci_effects__images(kv.Value);
            continue;
        }

        if(key=="models"){
            value.models = glTF_Effekseer_Deserializevci_effects__models(kv.Value);
            continue;
        }

    }
    return value;
}

public static EffekseerBodyJsonObject glTF_Effekseer_Deserializevci_effects__body(JsonNode parsed)
{
    var value = new EffekseerBodyJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="bufferView"){
            value.bufferView = kv.Value.GetInt32();
            continue;
        }

    }
    return value;
}

public static List<VCI.EffekseerImageJsonObject> glTF_Effekseer_Deserializevci_effects__images(JsonNode parsed)
{
    var value = new List<EffekseerImageJsonObject>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_Effekseer_Deserializevci_effects__images_ITEM(x));
    }
	return value;
}
public static EffekseerImageJsonObject glTF_Effekseer_Deserializevci_effects__images_ITEM(JsonNode parsed)
{
    var value = new EffekseerImageJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="bufferView"){
            value.bufferView = kv.Value.GetInt32();
            continue;
        }

        if(key=="mimeType"){
            value.mimeType = kv.Value.GetString();
            continue;
        }

    }
    return value;
}

public static List<VCI.EffekseerModelJsonObject> glTF_Effekseer_Deserializevci_effects__models(JsonNode parsed)
{
    var value = new List<EffekseerModelJsonObject>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_Effekseer_Deserializevci_effects__models_ITEM(x));
    }
	return value;
}
public static EffekseerModelJsonObject glTF_Effekseer_Deserializevci_effects__models_ITEM(JsonNode parsed)
{
    var value = new EffekseerModelJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="bufferView"){
            value.bufferView = kv.Value.GetInt32();
            continue;
        }

    }
    return value;
}

    } // class
} // namespace
