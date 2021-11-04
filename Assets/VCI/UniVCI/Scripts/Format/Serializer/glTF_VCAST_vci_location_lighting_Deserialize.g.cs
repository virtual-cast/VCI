using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_location_lighting_Deserializer
    {


public static glTF_VCAST_vci_location_lighting Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_location_lighting();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="locationLighting"){
            value.locationLighting = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting(kv.Value);
            continue;
        }

    }
    return value;
}

public static LocationLightingJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting(JsonNode parsed)
{
    var value = new LocationLightingJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="lightmapDirectionalMode"){
            value.lightmapDirectionalMode = kv.Value.GetString();
            continue;
        }

        if(key=="lightmapCompressionMode"){
            value.lightmapCompressionMode = kv.Value.GetString();
            continue;
        }

        if(key=="lightmapTextures"){
            value.lightmapTextures = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightmapTextures(kv.Value);
            continue;
        }

        if(key=="skyboxCubemap"){
            value.skyboxCubemap = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap(kv.Value);
            continue;
        }

        if(key=="lightProbes"){
            value.lightProbes = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightProbes(kv.Value);
            continue;
        }

    }
    return value;
}

public static LightmapTextureInfoJsonObject[] glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightmapTextures(JsonNode parsed)
{
    var value = new LightmapTextureInfoJsonObject[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightmapTextures_ITEM(x);
    }
	return value;
} 

public static LightmapTextureInfoJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightmapTextures_ITEM(JsonNode parsed)
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

public static CubemapTextureJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap(JsonNode parsed)
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
            value.texture = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_texture(kv.Value);
            continue;
        }

        if(key=="mipmapTextures"){
            value.mipmapTextures = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures(kv.Value);
            continue;
        }

    }
    return value;
}

public static CubemapFaceTextureSetJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_texture(JsonNode parsed)
{
    var value = new CubemapFaceTextureSetJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="cubemapPositiveX"){
            value.cubemapPositiveX = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_texture_cubemapPositiveX(kv.Value);
            continue;
        }

        if(key=="cubemapNegativeX"){
            value.cubemapNegativeX = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_texture_cubemapNegativeX(kv.Value);
            continue;
        }

        if(key=="cubemapPositiveY"){
            value.cubemapPositiveY = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_texture_cubemapPositiveY(kv.Value);
            continue;
        }

        if(key=="cubemapNegativeY"){
            value.cubemapNegativeY = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_texture_cubemapNegativeY(kv.Value);
            continue;
        }

        if(key=="cubemapPositiveZ"){
            value.cubemapPositiveZ = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_texture_cubemapPositiveZ(kv.Value);
            continue;
        }

        if(key=="cubemapNegativeZ"){
            value.cubemapNegativeZ = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_texture_cubemapNegativeZ(kv.Value);
            continue;
        }

    }
    return value;
}

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_texture_cubemapPositiveX(JsonNode parsed)
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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_texture_cubemapNegativeX(JsonNode parsed)
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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_texture_cubemapPositiveY(JsonNode parsed)
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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_texture_cubemapNegativeY(JsonNode parsed)
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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_texture_cubemapPositiveZ(JsonNode parsed)
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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_texture_cubemapNegativeZ(JsonNode parsed)
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

public static CubemapFaceTextureSetJsonObject[] glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures(JsonNode parsed)
{
    var value = new CubemapFaceTextureSetJsonObject[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures_ITEM(x);
    }
	return value;
} 

public static CubemapFaceTextureSetJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures_ITEM(JsonNode parsed)
{
    var value = new CubemapFaceTextureSetJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="cubemapPositiveX"){
            value.cubemapPositiveX = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveX(kv.Value);
            continue;
        }

        if(key=="cubemapNegativeX"){
            value.cubemapNegativeX = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeX(kv.Value);
            continue;
        }

        if(key=="cubemapPositiveY"){
            value.cubemapPositiveY = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveY(kv.Value);
            continue;
        }

        if(key=="cubemapNegativeY"){
            value.cubemapNegativeY = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeY(kv.Value);
            continue;
        }

        if(key=="cubemapPositiveZ"){
            value.cubemapPositiveZ = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveZ(kv.Value);
            continue;
        }

        if(key=="cubemapNegativeZ"){
            value.cubemapNegativeZ = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeZ(kv.Value);
            continue;
        }

    }
    return value;
}

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveX(JsonNode parsed)
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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeX(JsonNode parsed)
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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveY(JsonNode parsed)
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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeY(JsonNode parsed)
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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveZ(JsonNode parsed)
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

public static CubemapFaceTextureInfoJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeZ(JsonNode parsed)
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

public static LightProbeJsonObject[] glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightProbes(JsonNode parsed)
{
    var value = new LightProbeJsonObject[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightProbes_ITEM(x);
    }
	return value;
} 

public static LightProbeJsonObject glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightProbes_ITEM(JsonNode parsed)
{
    var value = new LightProbeJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="position"){
            value.position = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightProbes__position(kv.Value);
            continue;
        }

        if(key=="sphericalHarmonicsCoefficientsRed"){
            value.sphericalHarmonicsCoefficientsRed = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsRed(kv.Value);
            continue;
        }

        if(key=="sphericalHarmonicsCoefficientsGreen"){
            value.sphericalHarmonicsCoefficientsGreen = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsGreen(kv.Value);
            continue;
        }

        if(key=="sphericalHarmonicsCoefficientsBlue"){
            value.sphericalHarmonicsCoefficientsBlue = glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsBlue(kv.Value);
            continue;
        }

    }
    return value;
}

public static Single[] glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightProbes__position(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsRed(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsGreen(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_location_lighting_Deserializevci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsBlue(JsonNode parsed)
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
