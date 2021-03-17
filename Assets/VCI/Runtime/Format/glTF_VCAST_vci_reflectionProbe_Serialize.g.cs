
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_reflectionProbe_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_reflectionProbe value)
{
    f.BeginMap();


    if(value.reflectionProbe!=null){
        f.Key("reflectionProbe");                
        Serialize_vci_reflectionProbe(f, value.reflectionProbe);
    }

    f.EndMap();
}

public static void Serialize_vci_reflectionProbe(JsonFormatter f, glTF_VCAST_vci_ReflectionProbe value)
{
    f.BeginMap();


    if(value.boxOffset!=null&&value.boxOffset.Length>=0){
        f.Key("boxOffset");                
        Serialize_vci_reflectionProbe_boxOffset(f, value.boxOffset);
    }

    if(value.boxSize!=null&&value.boxSize.Length>=0){
        f.Key("boxSize");                
        Serialize_vci_reflectionProbe_boxSize(f, value.boxSize);
    }

    if(true){
        f.Key("intensity");                
        f.Value(value.intensity);
    }

    if(true){
        f.Key("useBoxProjection");                
        f.Value(value.useBoxProjection);
    }

    if(value.cubemap!=null){
        f.Key("cubemap");                
        Serialize_vci_reflectionProbe_cubemap(f, value.cubemap);
    }

    f.EndMap();
}

public static void Serialize_vci_reflectionProbe_boxOffset(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void Serialize_vci_reflectionProbe_boxSize(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void Serialize_vci_reflectionProbe_cubemap(JsonFormatter f, glTFCubemapTexture value)
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
        Serialize_vci_reflectionProbe_cubemap_texture(f, value.texture);
    }

    if(value.mipmapTextures!=null&&value.mipmapTextures.Length>=0){
        f.Key("mipmapTextures");                
        Serialize_vci_reflectionProbe_cubemap_mipmapTextures(f, value.mipmapTextures);
    }

    f.EndMap();
}

public static void Serialize_vci_reflectionProbe_cubemap_texture(JsonFormatter f, glTFCubemapFaceTextureSet value)
{
    f.BeginMap();


    if(value.cubemapPositiveX!=null){
        f.Key("cubemapPositiveX");                
        Serialize_vci_reflectionProbe_cubemap_texture_cubemapPositiveX(f, value.cubemapPositiveX);
    }

    if(value.cubemapNegativeX!=null){
        f.Key("cubemapNegativeX");                
        Serialize_vci_reflectionProbe_cubemap_texture_cubemapNegativeX(f, value.cubemapNegativeX);
    }

    if(value.cubemapPositiveY!=null){
        f.Key("cubemapPositiveY");                
        Serialize_vci_reflectionProbe_cubemap_texture_cubemapPositiveY(f, value.cubemapPositiveY);
    }

    if(value.cubemapNegativeY!=null){
        f.Key("cubemapNegativeY");                
        Serialize_vci_reflectionProbe_cubemap_texture_cubemapNegativeY(f, value.cubemapNegativeY);
    }

    if(value.cubemapPositiveZ!=null){
        f.Key("cubemapPositiveZ");                
        Serialize_vci_reflectionProbe_cubemap_texture_cubemapPositiveZ(f, value.cubemapPositiveZ);
    }

    if(value.cubemapNegativeZ!=null){
        f.Key("cubemapNegativeZ");                
        Serialize_vci_reflectionProbe_cubemap_texture_cubemapNegativeZ(f, value.cubemapNegativeZ);
    }

    f.EndMap();
}

public static void Serialize_vci_reflectionProbe_cubemap_texture_cubemapPositiveX(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_reflectionProbe_cubemap_texture_cubemapNegativeX(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_reflectionProbe_cubemap_texture_cubemapPositiveY(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_reflectionProbe_cubemap_texture_cubemapNegativeY(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_reflectionProbe_cubemap_texture_cubemapPositiveZ(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_reflectionProbe_cubemap_texture_cubemapNegativeZ(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_reflectionProbe_cubemap_mipmapTextures(JsonFormatter f, glTFCubemapFaceTextureSet[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    Serialize_vci_reflectionProbe_cubemap_mipmapTextures_ITEM(f, item);

    }
    f.EndList();
}

public static void Serialize_vci_reflectionProbe_cubemap_mipmapTextures_ITEM(JsonFormatter f, glTFCubemapFaceTextureSet value)
{
    f.BeginMap();


    if(value.cubemapPositiveX!=null){
        f.Key("cubemapPositiveX");                
        Serialize_vci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveX(f, value.cubemapPositiveX);
    }

    if(value.cubemapNegativeX!=null){
        f.Key("cubemapNegativeX");                
        Serialize_vci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeX(f, value.cubemapNegativeX);
    }

    if(value.cubemapPositiveY!=null){
        f.Key("cubemapPositiveY");                
        Serialize_vci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveY(f, value.cubemapPositiveY);
    }

    if(value.cubemapNegativeY!=null){
        f.Key("cubemapNegativeY");                
        Serialize_vci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeY(f, value.cubemapNegativeY);
    }

    if(value.cubemapPositiveZ!=null){
        f.Key("cubemapPositiveZ");                
        Serialize_vci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveZ(f, value.cubemapPositiveZ);
    }

    if(value.cubemapNegativeZ!=null){
        f.Key("cubemapNegativeZ");                
        Serialize_vci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeZ(f, value.cubemapNegativeZ);
    }

    f.EndMap();
}

public static void Serialize_vci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveX(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeX(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveY(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeY(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveZ(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

public static void Serialize_vci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeZ(JsonFormatter f, glTFCubemapFaceTextureInfo value)
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

} // VciSerializer
} // VCI
