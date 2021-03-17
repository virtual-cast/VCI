
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_Effekseer_emitters_Deserializer
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

public static List<VCI.glTF_Effekseer_emitter> glTF_Effekseer_emitters_Deserializevci_emitters(JsonNode parsed)
{
    var value = new List<glTF_Effekseer_emitter>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_Effekseer_emitters_Deserializevci_emitters_ITEM(x));
    }
	return value;
}
public static glTF_Effekseer_emitter glTF_Effekseer_emitters_Deserializevci_emitters_ITEM(JsonNode parsed)
{
    var value = new glTF_Effekseer_emitter();

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

} // VciDeserializer
} // VCI
