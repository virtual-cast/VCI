using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_reflectionProbe_Deserializer
    {


public static glTF_VCAST_vci_reflectionProbe Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_reflectionProbe();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="reflectionProbe"){
            value.reflectionProbe = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe(kv.Value);
            continue;
        }

    }
    return value;
}

public static ReflectionProbeJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe(JsonNode parsed)
{
    var value = new ReflectionProbeJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="boxOffset"){
            value.boxOffset = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_boxOffset(kv.Value);
            continue;
        }

        if(key=="boxSize"){
            value.boxSize = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_boxSize(kv.Value);
            continue;
        }

        if(key=="intensity"){
            value.intensity = kv.Value.GetSingle();
            continue;
        }

        if(key=="useBoxProjection"){
            value.useBoxProjection = kv.Value.GetBoolean();
            continue;
        }

        if(key=="cubemap"){
            value.cubemap = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap(kv.Value);
            continue;
        }

    }
    return value;
}

public static Single[] glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_boxOffset(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_boxSize(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static CubemapTextureJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap(JsonNode parsed)
{
    var value = new CubemapTextureJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="compressionMode"){
            value.compressionMode = kv.Value.GetString();
            continue;
        }

        if(key=="mipmapCount"){
            value.mipmapCount = kv.Value.GetInt32();
            continue;
        }

        if(key=="texture"){
            value.texture = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_texture(kv.Value);
            continue;
        }

        if(key=="mipmapTextures"){
            value.mipmapTextures = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures(kv.Value);
            continue;
        }

    }
    return value;
}

public static CubemapFaceTextureSetJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_texture(JsonNode parsed)
{
    var value = new CubemapFaceTextureSetJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="cubemapPositiveX"){
            value.cubemapPositiveX = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_texture_cubemapPositiveX(kv.Value);
            continue;
        }

        if(key=="cubemapNegativeX"){
            value.cubemapNegativeX = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_texture_cubemapNegativeX(kv.Value);
            continue;
        }

        if(key=="cubemapPositiveY"){
            value.cubemapPositiveY = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_texture_cubemapPositiveY(kv.Value);
            continue;
        }

        if(key=="cubemapNegativeY"){
            value.cubemapNegativeY = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_texture_cubemapNegativeY(kv.Value);
            continue;
        }

        if(key=="cubemapPositiveZ"){
            value.cubemapPositiveZ = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_texture_cubemapPositiveZ(kv.Value);
            continue;
        }

        if(key=="cubemapNegativeZ"){
            value.cubemapNegativeZ = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_texture_cubemapNegativeZ(kv.Value);
            continue;
        }

    }
    return value;
}

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_texture_cubemapPositiveX(JsonNode parsed)
{
    var value = new CubemapFaceTextureInfoJsonObject();

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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_texture_cubemapNegativeX(JsonNode parsed)
{
    var value = new CubemapFaceTextureInfoJsonObject();

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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_texture_cubemapPositiveY(JsonNode parsed)
{
    var value = new CubemapFaceTextureInfoJsonObject();

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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_texture_cubemapNegativeY(JsonNode parsed)
{
    var value = new CubemapFaceTextureInfoJsonObject();

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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_texture_cubemapPositiveZ(JsonNode parsed)
{
    var value = new CubemapFaceTextureInfoJsonObject();

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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_texture_cubemapNegativeZ(JsonNode parsed)
{
    var value = new CubemapFaceTextureInfoJsonObject();

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

public static CubemapFaceTextureSetJsonObject[] glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures(JsonNode parsed)
{
    var value = new CubemapFaceTextureSetJsonObject[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures_ITEM(x);
    }
	return value;
} 

public static CubemapFaceTextureSetJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures_ITEM(JsonNode parsed)
{
    var value = new CubemapFaceTextureSetJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="cubemapPositiveX"){
            value.cubemapPositiveX = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveX(kv.Value);
            continue;
        }

        if(key=="cubemapNegativeX"){
            value.cubemapNegativeX = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeX(kv.Value);
            continue;
        }

        if(key=="cubemapPositiveY"){
            value.cubemapPositiveY = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveY(kv.Value);
            continue;
        }

        if(key=="cubemapNegativeY"){
            value.cubemapNegativeY = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeY(kv.Value);
            continue;
        }

        if(key=="cubemapPositiveZ"){
            value.cubemapPositiveZ = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveZ(kv.Value);
            continue;
        }

        if(key=="cubemapNegativeZ"){
            value.cubemapNegativeZ = glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeZ(kv.Value);
            continue;
        }

    }
    return value;
}

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveX(JsonNode parsed)
{
    var value = new CubemapFaceTextureInfoJsonObject();

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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeX(JsonNode parsed)
{
    var value = new CubemapFaceTextureInfoJsonObject();

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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveY(JsonNode parsed)
{
    var value = new CubemapFaceTextureInfoJsonObject();

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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeY(JsonNode parsed)
{
    var value = new CubemapFaceTextureInfoJsonObject();

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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveZ(JsonNode parsed)
{
    var value = new CubemapFaceTextureInfoJsonObject();

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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_reflectionProbe_Deserializevci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeZ(JsonNode parsed)
{
    var value = new CubemapFaceTextureInfoJsonObject();

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

    } // class
} // namespace
