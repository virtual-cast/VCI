using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_lightmap_Deserializer
    {


public static glTF_VCAST_vci_lightmap Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_lightmap();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="lightmap"){
            value.lightmap = glTF_VCAST_vci_lightmap_Deserializevci_lightmap(kv.Value);
            continue;
        }

    }
    return value;
}

public static LightmapJsonObject glTF_VCAST_vci_lightmap_Deserializevci_lightmap(JsonNode parsed)
{
    var value = new LightmapJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="texture"){
            value.texture = glTF_VCAST_vci_lightmap_Deserializevci_lightmap_texture(kv.Value);
            continue;
        }

        if(key=="offset"){
            value.offset = glTF_VCAST_vci_lightmap_Deserializevci_lightmap_offset(kv.Value);
            continue;
        }

        if(key=="scale"){
            value.scale = glTF_VCAST_vci_lightmap_Deserializevci_lightmap_scale(kv.Value);
            continue;
        }

    }
    return value;
}

public static LightmapTextureInfoJsonObject glTF_VCAST_vci_lightmap_Deserializevci_lightmap_texture(JsonNode parsed)
{
    var value = new LightmapTextureInfoJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="index"){
            value.index = kv.Value.GetInt32();
            continue;
        }

        if(key=="texCoord"){
            value.texCoord = kv.Value.GetInt32();
            continue;
        }

        if(key=="extensions"){
            value.extensions = new glTFExtensionImport(kv.Value);
            continue;
        }

        if(key=="extras"){
            value.extras = new glTFExtensionImport(kv.Value);
            continue;
        }

    }
    return value;
}

public static Single[] glTF_VCAST_vci_lightmap_Deserializevci_lightmap_offset(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_lightmap_Deserializevci_lightmap_scale(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

    } // class
} // namespace
