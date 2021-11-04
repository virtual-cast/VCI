using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_text_Deserializer
    {


public static glTF_VCAST_vci_text Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_text();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="text"){
            value.text = glTF_VCAST_vci_text_Deserializevci_text(kv.Value);
            continue;
        }

    }
    return value;
}

public static TextJsonObject glTF_VCAST_vci_text_Deserializevci_text(JsonNode parsed)
{
    var value = new TextJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="fontName"){
            value.fontName = kv.Value.GetString();
            continue;
        }

        if(key=="text"){
            value.text = kv.Value.GetString();
            continue;
        }

        if(key=="richText"){
            value.richText = kv.Value.GetBoolean();
            continue;
        }

        if(key=="fontSize"){
            value.fontSize = kv.Value.GetSingle();
            continue;
        }

        if(key=="autoSize"){
            value.autoSize = kv.Value.GetBoolean();
            continue;
        }

        if(key=="fontStyle"){
            value.fontStyle = kv.Value.GetInt32();
            continue;
        }

        if(key=="color"){
            value.color = glTF_VCAST_vci_text_Deserializevci_text_color(kv.Value);
            continue;
        }

        if(key=="enableVertexGradient"){
            value.enableVertexGradient = kv.Value.GetBoolean();
            continue;
        }

        if(key=="topLeftColor"){
            value.topLeftColor = glTF_VCAST_vci_text_Deserializevci_text_topLeftColor(kv.Value);
            continue;
        }

        if(key=="topRightColor"){
            value.topRightColor = glTF_VCAST_vci_text_Deserializevci_text_topRightColor(kv.Value);
            continue;
        }

        if(key=="bottomLeftColor"){
            value.bottomLeftColor = glTF_VCAST_vci_text_Deserializevci_text_bottomLeftColor(kv.Value);
            continue;
        }

        if(key=="bottomRightColor"){
            value.bottomRightColor = glTF_VCAST_vci_text_Deserializevci_text_bottomRightColor(kv.Value);
            continue;
        }

        if(key=="characterSpacing"){
            value.characterSpacing = kv.Value.GetSingle();
            continue;
        }

        if(key=="wordSpacing"){
            value.wordSpacing = kv.Value.GetSingle();
            continue;
        }

        if(key=="lineSpacing"){
            value.lineSpacing = kv.Value.GetSingle();
            continue;
        }

        if(key=="paragraphSpacing"){
            value.paragraphSpacing = kv.Value.GetSingle();
            continue;
        }

        if(key=="alignment"){
            value.alignment = kv.Value.GetInt32();
            continue;
        }

        if(key=="enableWordWrapping"){
            value.enableWordWrapping = kv.Value.GetBoolean();
            continue;
        }

        if(key=="overflowMode"){
            value.overflowMode = kv.Value.GetInt32();
            continue;
        }

        if(key=="enableKerning"){
            value.enableKerning = kv.Value.GetBoolean();
            continue;
        }

        if(key=="extraPadding"){
            value.extraPadding = kv.Value.GetBoolean();
            continue;
        }

        if(key=="margin"){
            value.margin = glTF_VCAST_vci_text_Deserializevci_text_margin(kv.Value);
            continue;
        }

    }
    return value;
}

public static Single[] glTF_VCAST_vci_text_Deserializevci_text_color(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_text_Deserializevci_text_topLeftColor(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_text_Deserializevci_text_topRightColor(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_text_Deserializevci_text_bottomLeftColor(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_text_Deserializevci_text_bottomRightColor(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_text_Deserializevci_text_margin(JsonNode parsed)
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
