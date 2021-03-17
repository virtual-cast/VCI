
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_location_lighting_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_location_lighting value)
{
    f.BeginMap();


    if(value.locationLighting!=null){
        f.Key("locationLighting");                
        Serialize_vci_locationLighting(f, value.locationLighting);
    }

    f.EndMap();
}

public static void Serialize_vci_locationLighting(JsonFormatter f, glTF_VCAST_vci_LocationLighting value)
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
        Serialize_vci_locationLighting_lightmapTextures(f, value.lightmapTextures);
    }

    if(value.skyboxCubemap!=null){
        f.Key("skyboxCubemap");                
        Serialize_vci_locationLighting_skyboxCubemap(f, value.skyboxCubemap);
    }

    if(value.lightProbes!=null&&value.lightProbes.Length>=0){
        f.Key("lightProbes");                
        Serialize_vci_locationLighting_lightProbes(f, value.lightProbes);
    }

    f.EndMap();
}

public static void Serialize_vci_locationLighting_lightmapTextures(JsonFormatter f, glTFLightmapTextureInfo[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    Serialize_vci_locationLighting_lightmapTextures_ITEM(f, item);

    }
    f.EndList();
}

public static void Serialize_vci_locationLighting_lightmapTextures_ITEM(JsonFormatter f, glTFLightmapTextureInfo value)
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

public static void Serialize_vci_locationLighting_skyboxCubemap(JsonFormatter f, glTFCubemapTexture value)
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
        Serialize_vci_locationLighting_skyboxCubemap_texture(f, value.texture);
    }

    if(value.mipmapTextures!=null&&value.mipmapTextures.Length>=0){
        f.Key("mipmapTextures");                
        Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures(f, value.mipmapTextures);
    }

    f.EndMap();
}

public static void Serialize_vci_locationLighting_skyboxCubemap_texture(JsonFormatter f, glTFCubemapFaceTextureSet value)
{
    f.BeginMap();


    if(value.cubemapPositiveX!=null){
        f.Key("cubemapPositiveX");                
        Serialize_vci_locationLighting_skyboxCubemap_texture_cubemapPositiveX(f, value.cubemapPositiveX);
    }

    if(value.cubemapNegativeX!=null){
        f.Key("cubemapNegativeX");                
        Serialize_vci_locationLighting_skyboxCubemap_texture_cubemapNegativeX(f, value.cubemapNegativeX);
    }

    if(value.cubemapPositiveY!=null){
        f.Key("cubemapPositiveY");                
        Serialize_vci_locationLighting_skyboxCubemap_texture_cubemapPositiveY(f, value.cubemapPositiveY);
    }

    if(value.cubemapNegativeY!=null){
        f.Key("cubemapNegativeY");                
        Serialize_vci_locationLighting_skyboxCubemap_texture_cubemapNegativeY(f, value.cubemapNegativeY);
    }

    if(value.cubemapPositiveZ!=null){
        f.Key("cubemapPositiveZ");                
        Serialize_vci_locationLighting_skyboxCubemap_texture_cubemapPositiveZ(f, value.cubemapPositiveZ);
    }

    if(value.cubemapNegativeZ!=null){
        f.Key("cubemapNegativeZ");                
        Serialize_vci_locationLighting_skyboxCubemap_texture_cubemapNegativeZ(f, value.cubemapNegativeZ);
    }

    f.EndMap();
}

public static void Serialize_vci_locationLighting_skyboxCubemap_texture_cubemapPositiveX(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_locationLighting_skyboxCubemap_texture_cubemapNegativeX(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_locationLighting_skyboxCubemap_texture_cubemapPositiveY(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_locationLighting_skyboxCubemap_texture_cubemapNegativeY(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_locationLighting_skyboxCubemap_texture_cubemapPositiveZ(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_locationLighting_skyboxCubemap_texture_cubemapNegativeZ(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures(JsonFormatter f, glTFCubemapFaceTextureSet[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures_ITEM(f, item);

    }
    f.EndList();
}

public static void Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures_ITEM(JsonFormatter f, glTFCubemapFaceTextureSet value)
{
    f.BeginMap();


    if(value.cubemapPositiveX!=null){
        f.Key("cubemapPositiveX");                
        Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveX(f, value.cubemapPositiveX);
    }

    if(value.cubemapNegativeX!=null){
        f.Key("cubemapNegativeX");                
        Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeX(f, value.cubemapNegativeX);
    }

    if(value.cubemapPositiveY!=null){
        f.Key("cubemapPositiveY");                
        Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveY(f, value.cubemapPositiveY);
    }

    if(value.cubemapNegativeY!=null){
        f.Key("cubemapNegativeY");                
        Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeY(f, value.cubemapNegativeY);
    }

    if(value.cubemapPositiveZ!=null){
        f.Key("cubemapPositiveZ");                
        Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveZ(f, value.cubemapPositiveZ);
    }

    if(value.cubemapNegativeZ!=null){
        f.Key("cubemapNegativeZ");                
        Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeZ(f, value.cubemapNegativeZ);
    }

    f.EndMap();
}

public static void Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveX(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeX(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveY(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeY(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures__cubemapPositiveZ(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_locationLighting_skyboxCubemap_mipmapTextures__cubemapNegativeZ(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_locationLighting_lightProbes(JsonFormatter f, glTF_VCAST_vci_LocationLighting_LightProbe[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    Serialize_vci_locationLighting_lightProbes_ITEM(f, item);

    }
    f.EndList();
}

public static void Serialize_vci_locationLighting_lightProbes_ITEM(JsonFormatter f, glTF_VCAST_vci_LocationLighting_LightProbe value)
{
    f.BeginMap();


    if(value.position!=null&&value.position.Length>=0){
        f.Key("position");                
        Serialize_vci_locationLighting_lightProbes__position(f, value.position);
    }

    if(value.sphericalHarmonicsCoefficientsRed!=null&&value.sphericalHarmonicsCoefficientsRed.Length>=0){
        f.Key("sphericalHarmonicsCoefficientsRed");                
        Serialize_vci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsRed(f, value.sphericalHarmonicsCoefficientsRed);
    }

    if(value.sphericalHarmonicsCoefficientsGreen!=null&&value.sphericalHarmonicsCoefficientsGreen.Length>=0){
        f.Key("sphericalHarmonicsCoefficientsGreen");                
        Serialize_vci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsGreen(f, value.sphericalHarmonicsCoefficientsGreen);
    }

    if(value.sphericalHarmonicsCoefficientsBlue!=null&&value.sphericalHarmonicsCoefficientsBlue.Length>=0){
        f.Key("sphericalHarmonicsCoefficientsBlue");                
        Serialize_vci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsBlue(f, value.sphericalHarmonicsCoefficientsBlue);
    }

    f.EndMap();
}

public static void Serialize_vci_locationLighting_lightProbes__position(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void Serialize_vci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsRed(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void Serialize_vci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsGreen(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void Serialize_vci_locationLighting_lightProbes__sphericalHarmonicsCoefficientsBlue(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

} // VciSerializer
} // VCI
