using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_location_lighting_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_location_lighting value)
{
    f.BeginMap();


    if(value.locationLighting!=null){
        f.Key("locationLighting");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting(f, value.locationLighting);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting(JsonFormatter f, LocationLightingJsonObject value)
{
    f.BeginMap();


    if(!string.IsNullOrEmpty(value.lightmapDirectionalMode)){
        f.Key("lightmapDirectionalMode");
        f.Value(value.lightmapDirectionalMode);
    }

    if(!string.IsNullOrEmpty(value.lightmapCompressionMode)){
        f.Key("lightmapCompressionMode");
        f.Value(value.lightmapCompressionMode);
    }

    if(value.lightmapTextures!=null&&value.lightmapTextures.Length>=0){
        f.Key("lightmapTextures");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightmapTextures(f, value.lightmapTextures);
    }

    if(value.skyboxCubemap!=null){
        f.Key("skyboxCubemap");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap(f, value.skyboxCubemap);
    }

    if(value.lightProbes!=null&&value.lightProbes.Length>=0){
        f.Key("lightProbes");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightProbes(f, value.lightProbes);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightmapTextures(JsonFormatter f, LightmapTextureInfoJsonObject[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightmapTextures_ITEM(f, item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightmapTextures_ITEM(JsonFormatter f, LightmapTextureInfoJsonObject value)
{
    f.BeginMap();


    if(value.index>=0){
        f.Key("index");
        f.Value(value.index);
    }

    if(value.texCoord>=0){
        f.Key("texCoord");
        f.Value(value.texCoord);
    }

    if(value.extensions!=null){
        f.Key("extensions");
        value.extensions.Serialize(f);
    }

    if(value.extras!=null){
        f.Key("extras");
        value.extras.Serialize(f);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap(JsonFormatter f, CubemapTextureJsonObject value)
{
    f.BeginMap();


    if(!string.IsNullOrEmpty(value.compressionMode)){
        f.Key("compressionMode");
        f.Value(value.compressionMode);
    }

    if(true){
        f.Key("mipmapCount");
        f.Value(value.mipmapCount);
    }

    if(value.texture!=null){
        f.Key("texture");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_texture(f, value.texture);
    }

    if(value.mipmapTextures!=null&&value.mipmapTextures.Length>=0){
        f.Key("mipmapTextures");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures(f, value.mipmapTextures);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_texture(JsonFormatter f, CubemapFaceTextureSetJsonObject value)
{
    f.BeginMap();


    if(value.cubemapPositiveX!=null){
        f.Key("cubemapPositiveX");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_texture_cubemapPositiveX(f, value.cubemapPositiveX);
    }

    if(value.cubemapNegativeX!=null){
        f.Key("cubemapNegativeX");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_texture_cubemapNegativeX(f, value.cubemapNegativeX);
    }

    if(value.cubemapPositiveY!=null){
        f.Key("cubemapPositiveY");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_texture_cubemapPositiveY(f, value.cubemapPositiveY);
    }

    if(value.cubemapNegativeY!=null){
        f.Key("cubemapNegativeY");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_texture_cubemapNegativeY(f, value.cubemapNegativeY);
    }

    if(value.cubemapPositiveZ!=null){
        f.Key("cubemapPositiveZ");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_texture_cubemapPositiveZ(f, value.cubemapPositiveZ);
    }

    if(value.cubemapNegativeZ!=null){
        f.Key("cubemapNegativeZ");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_texture_cubemapNegativeZ(f, value.cubemapNegativeZ);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_texture_cubemapPositiveX(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
{
    f.BeginMap();


    if(value.index>=0){
        f.Key("index");
        f.Value(value.index);
    }

    if(value.texCoord>=0){
        f.Key("texCoord");
        f.Value(value.texCoord);
    }

    if(value.extensions!=null){
        f.Key("extensions");
        value.extensions.Serialize(f);
    }

    if(value.extras!=null){
        f.Key("extras");
        value.extras.Serialize(f);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_texture_cubemapNegativeX(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
{
    f.BeginMap();


    if(value.index>=0){
        f.Key("index");
        f.Value(value.index);
    }

    if(value.texCoord>=0){
        f.Key("texCoord");
        f.Value(value.texCoord);
    }

    if(value.extensions!=null){
        f.Key("extensions");
        value.extensions.Serialize(f);
    }

    if(value.extras!=null){
        f.Key("extras");
        value.extras.Serialize(f);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_texture_cubemapPositiveY(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
{
    f.BeginMap();


    if(value.index>=0){
        f.Key("index");
        f.Value(value.index);
    }

    if(value.texCoord>=0){
        f.Key("texCoord");
        f.Value(value.texCoord);
    }

    if(value.extensions!=null){
        f.Key("extensions");
        value.extensions.Serialize(f);
    }

    if(value.extras!=null){
        f.Key("extras");
        value.extras.Serialize(f);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_texture_cubemapNegativeY(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
{
    f.BeginMap();


    if(value.index>=0){
        f.Key("index");
        f.Value(value.index);
    }

    if(value.texCoord>=0){
        f.Key("texCoord");
        f.Value(value.texCoord);
    }

    if(value.extensions!=null){
        f.Key("extensions");
        value.extensions.Serialize(f);
    }

    if(value.extras!=null){
        f.Key("extras");
        value.extras.Serialize(f);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_texture_cubemapPositiveZ(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
{
    f.BeginMap();


    if(value.index>=0){
        f.Key("index");
        f.Value(value.index);
    }

    if(value.texCoord>=0){
        f.Key("texCoord");
        f.Value(value.texCoord);
    }

    if(value.extensions!=null){
        f.Key("extensions");
        value.extensions.Serialize(f);
    }

    if(value.extras!=null){
        f.Key("extras");
        value.extras.Serialize(f);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_texture_cubemapNegativeZ(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
{
    f.BeginMap();


    if(value.index>=0){
        f.Key("index");
        f.Value(value.index);
    }

    if(value.texCoord>=0){
        f.Key("texCoord");
        f.Value(value.texCoord);
    }

    if(value.extensions!=null){
        f.Key("extensions");
        value.extensions.Serialize(f);
    }

    if(value.extras!=null){
        f.Key("extras");
        value.extras.Serialize(f);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures(JsonFormatter f, CubemapFaceTextureSetJsonObject[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures_ITEM(f, item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures_ITEM(JsonFormatter f, CubemapFaceTextureSetJsonObject value)
{
    f.BeginMap();


    if(value.cubemapPositiveX!=null){
        f.Key("cubemapPositiveX");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveX(f, value.cubemapPositiveX);
    }

    if(value.cubemapNegativeX!=null){
        f.Key("cubemapNegativeX");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeX(f, value.cubemapNegativeX);
    }

    if(value.cubemapPositiveY!=null){
        f.Key("cubemapPositiveY");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveY(f, value.cubemapPositiveY);
    }

    if(value.cubemapNegativeY!=null){
        f.Key("cubemapNegativeY");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeY(f, value.cubemapNegativeY);
    }

    if(value.cubemapPositiveZ!=null){
        f.Key("cubemapPositiveZ");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveZ(f, value.cubemapPositiveZ);
    }

    if(value.cubemapNegativeZ!=null){
        f.Key("cubemapNegativeZ");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeZ(f, value.cubemapNegativeZ);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveX(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
{
    f.BeginMap();


    if(value.index>=0){
        f.Key("index");
        f.Value(value.index);
    }

    if(value.texCoord>=0){
        f.Key("texCoord");
        f.Value(value.texCoord);
    }

    if(value.extensions!=null){
        f.Key("extensions");
        value.extensions.Serialize(f);
    }

    if(value.extras!=null){
        f.Key("extras");
        value.extras.Serialize(f);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeX(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
{
    f.BeginMap();


    if(value.index>=0){
        f.Key("index");
        f.Value(value.index);
    }

    if(value.texCoord>=0){
        f.Key("texCoord");
        f.Value(value.texCoord);
    }

    if(value.extensions!=null){
        f.Key("extensions");
        value.extensions.Serialize(f);
    }

    if(value.extras!=null){
        f.Key("extras");
        value.extras.Serialize(f);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveY(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
{
    f.BeginMap();


    if(value.index>=0){
        f.Key("index");
        f.Value(value.index);
    }

    if(value.texCoord>=0){
        f.Key("texCoord");
        f.Value(value.texCoord);
    }

    if(value.extensions!=null){
        f.Key("extensions");
        value.extensions.Serialize(f);
    }

    if(value.extras!=null){
        f.Key("extras");
        value.extras.Serialize(f);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeY(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
{
    f.BeginMap();


    if(value.index>=0){
        f.Key("index");
        f.Value(value.index);
    }

    if(value.texCoord>=0){
        f.Key("texCoord");
        f.Value(value.texCoord);
    }

    if(value.extensions!=null){
        f.Key("extensions");
        value.extensions.Serialize(f);
    }

    if(value.extras!=null){
        f.Key("extras");
        value.extras.Serialize(f);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveZ(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
{
    f.BeginMap();


    if(value.index>=0){
        f.Key("index");
        f.Value(value.index);
    }

    if(value.texCoord>=0){
        f.Key("texCoord");
        f.Value(value.texCoord);
    }

    if(value.extensions!=null){
        f.Key("extensions");
        value.extensions.Serialize(f);
    }

    if(value.extras!=null){
        f.Key("extras");
        value.extras.Serialize(f);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeZ(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
{
    f.BeginMap();


    if(value.index>=0){
        f.Key("index");
        f.Value(value.index);
    }

    if(value.texCoord>=0){
        f.Key("texCoord");
        f.Value(value.texCoord);
    }

    if(value.extensions!=null){
        f.Key("extensions");
        value.extensions.Serialize(f);
    }

    if(value.extras!=null){
        f.Key("extras");
        value.extras.Serialize(f);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightProbes(JsonFormatter f, LightProbeJsonObject[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightProbes_ITEM(f, item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightProbes_ITEM(JsonFormatter f, LightProbeJsonObject value)
{
    f.BeginMap();


    if(value.position!=null&&value.position.Length>=0){
        f.Key("position");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightProbes__position(f, value.position);
    }

    if(value.sphericalHarmonicsCoefficientsRed!=null&&value.sphericalHarmonicsCoefficientsRed.Length>=0){
        f.Key("sphericalHarmonicsCoefficientsRed");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsRed(f, value.sphericalHarmonicsCoefficientsRed);
    }

    if(value.sphericalHarmonicsCoefficientsGreen!=null&&value.sphericalHarmonicsCoefficientsGreen.Length>=0){
        f.Key("sphericalHarmonicsCoefficientsGreen");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsGreen(f, value.sphericalHarmonicsCoefficientsGreen);
    }

    if(value.sphericalHarmonicsCoefficientsBlue!=null&&value.sphericalHarmonicsCoefficientsBlue.Length>=0){
        f.Key("sphericalHarmonicsCoefficientsBlue");
        glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsBlue(f, value.sphericalHarmonicsCoefficientsBlue);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightProbes__position(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsRed(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsGreen(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_location_lighting_Serializevci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsBlue(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

    } // class
} // namespace
