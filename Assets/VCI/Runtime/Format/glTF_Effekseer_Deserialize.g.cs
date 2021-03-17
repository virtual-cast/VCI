
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_Effekseer_Deserializer
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

public static List<VCI.glTF_Effekseer_effect> glTF_Effekseer_Deserializevci_effects(JsonNode parsed)
{
    var value = new List<glTF_Effekseer_effect>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_Effekseer_Deserializevci_effects_ITEM(x));
    }
	return value;
}
public static glTF_Effekseer_effect glTF_Effekseer_Deserializevci_effects_ITEM(JsonNode parsed)
{
    var value = new glTF_Effekseer_effect();

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

public static glTF_Effekseer_body glTF_Effekseer_Deserializevci_effects__body(JsonNode parsed)
{
    var value = new glTF_Effekseer_body();

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

public static List<VCI.glTF_Effekseer_image> glTF_Effekseer_Deserializevci_effects__images(JsonNode parsed)
{
    var value = new List<glTF_Effekseer_image>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_Effekseer_Deserializevci_effects__images_ITEM(x));
    }
	return value;
}
public static glTF_Effekseer_image glTF_Effekseer_Deserializevci_effects__images_ITEM(JsonNode parsed)
{
    var value = new glTF_Effekseer_image();

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

public static List<VCI.glTF_Effekseer_model> glTF_Effekseer_Deserializevci_effects__models(JsonNode parsed)
{
    var value = new List<glTF_Effekseer_model>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_Effekseer_Deserializevci_effects__models_ITEM(x));
    }
	return value;
}
public static glTF_Effekseer_model glTF_Effekseer_Deserializevci_effects__models_ITEM(JsonNode parsed)
{
    var value = new glTF_Effekseer_model();

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

} // VciDeserializer
} // VCI
