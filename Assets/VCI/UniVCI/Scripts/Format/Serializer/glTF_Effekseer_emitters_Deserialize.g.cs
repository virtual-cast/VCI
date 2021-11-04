using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_Effekseer_emitters_Deserializer
    {


public static glTF_Effekseer_emitters Deserialize(JsonNode parsed)
{
    var value = new glTF_Effekseer_emitters();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="emitters"){
            value.emitters = glTF_Effekseer_emitters_Deserializevci_emitters(kv.Value);
            continue;
        }

    }
    return value;
}

public static List<VCI.EffekseerEmitterJsonObject> glTF_Effekseer_emitters_Deserializevci_emitters(JsonNode parsed)
{
    var value = new List<EffekseerEmitterJsonObject>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_Effekseer_emitters_Deserializevci_emitters_ITEM(x));
    }
	return value;
}
public static EffekseerEmitterJsonObject glTF_Effekseer_emitters_Deserializevci_emitters_ITEM(JsonNode parsed)
{
    var value = new EffekseerEmitterJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="effectIndex"){
            value.effectIndex = kv.Value.GetInt32();
            continue;
        }

        if(key=="isPlayOnStart"){
            value.isPlayOnStart = kv.Value.GetBoolean();
            continue;
        }

        if(key=="isLoop"){
            value.isLoop = kv.Value.GetBoolean();
            continue;
        }

    }
    return value;
}

    } // class
} // namespace
