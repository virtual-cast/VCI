using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_reflectionProbe_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_reflectionProbe value)
{
    f.BeginMap();


    if(value.reflectionProbe!=null){
        f.Key("reflectionProbe");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe(f, value.reflectionProbe);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe(JsonFormatter f, ReflectionProbeJsonObject value)
{
    f.BeginMap();


    if(value.boxOffset!=null&&value.boxOffset.Length>=0){
        f.Key("boxOffset");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_boxOffset(f, value.boxOffset);
    }

    if(value.boxSize!=null&&value.boxSize.Length>=0){
        f.Key("boxSize");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_boxSize(f, value.boxSize);
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
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap(f, value.cubemap);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_boxOffset(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_boxSize(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap(JsonFormatter f, CubemapTextureJsonObject value)
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
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_texture(f, value.texture);
    }

    if(value.mipmapTextures!=null&&value.mipmapTextures.Length>=0){
        f.Key("mipmapTextures");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures(f, value.mipmapTextures);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_texture(JsonFormatter f, CubemapFaceTextureSetJsonObject value)
{
    f.BeginMap();


    if(value.cubemapPositiveX!=null){
        f.Key("cubemapPositiveX");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_texture_cubemapPositiveX(f, value.cubemapPositiveX);
    }

    if(value.cubemapNegativeX!=null){
        f.Key("cubemapNegativeX");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_texture_cubemapNegativeX(f, value.cubemapNegativeX);
    }

    if(value.cubemapPositiveY!=null){
        f.Key("cubemapPositiveY");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_texture_cubemapPositiveY(f, value.cubemapPositiveY);
    }

    if(value.cubemapNegativeY!=null){
        f.Key("cubemapNegativeY");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_texture_cubemapNegativeY(f, value.cubemapNegativeY);
    }

    if(value.cubemapPositiveZ!=null){
        f.Key("cubemapPositiveZ");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_texture_cubemapPositiveZ(f, value.cubemapPositiveZ);
    }

    if(value.cubemapNegativeZ!=null){
        f.Key("cubemapNegativeZ");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_texture_cubemapNegativeZ(f, value.cubemapNegativeZ);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_texture_cubemapPositiveX(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
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

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_texture_cubemapNegativeX(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
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

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_texture_cubemapPositiveY(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
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

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_texture_cubemapNegativeY(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
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

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_texture_cubemapPositiveZ(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
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

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_texture_cubemapNegativeZ(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
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

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures(JsonFormatter f, CubemapFaceTextureSetJsonObject[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures_ITEM(f, item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures_ITEM(JsonFormatter f, CubemapFaceTextureSetJsonObject value)
{
    f.BeginMap();


    if(value.cubemapPositiveX!=null){
        f.Key("cubemapPositiveX");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveX(f, value.cubemapPositiveX);
    }

    if(value.cubemapNegativeX!=null){
        f.Key("cubemapNegativeX");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeX(f, value.cubemapNegativeX);
    }

    if(value.cubemapPositiveY!=null){
        f.Key("cubemapPositiveY");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveY(f, value.cubemapPositiveY);
    }

    if(value.cubemapNegativeY!=null){
        f.Key("cubemapNegativeY");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeY(f, value.cubemapNegativeY);
    }

    if(value.cubemapPositiveZ!=null){
        f.Key("cubemapPositiveZ");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveZ(f, value.cubemapPositiveZ);
    }

    if(value.cubemapNegativeZ!=null){
        f.Key("cubemapNegativeZ");
        glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeZ(f, value.cubemapNegativeZ);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveX(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
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

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeX(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
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

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveY(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
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

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeY(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
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

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures__cubemapPositiveZ(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
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

public static void glTF_VCAST_vci_reflectionProbe_Serializevci_reflectionProbe_cubemap_mipmapTextures__cubemapNegativeZ(JsonFormatter f, CubemapFaceTextureInfoJsonObject value)
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

    } // class
} // namespace
