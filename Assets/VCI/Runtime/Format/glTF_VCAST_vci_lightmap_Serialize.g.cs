
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_lightmap_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_lightmap value)
{
    f.BeginMap();


    if(value.lightmap!=null){
        f.Key("lightmap");                
        Serialize_vci_lightmap(f, value.lightmap);
    }

    f.EndMap();
}

public static void Serialize_vci_lightmap(JsonFormatter f, glTF_VCAST_vci_Lightmap value)
{
    f.BeginMap();


    if(value.texture!=null){
        f.Key("texture");                
        Serialize_vci_lightmap_texture(f, value.texture);
    }

    if(value.offset!=null&&value.offset.Length>=0){
        f.Key("offset");                
        Serialize_vci_lightmap_offset(f, value.offset);
    }

    if(value.scale!=null&&value.scale.Length>=0){
        f.Key("scale");                
        Serialize_vci_lightmap_scale(f, value.scale);
    }

    f.EndMap();
}

public static void Serialize_vci_lightmap_texture(JsonFormatter f, glTFLightmapTextureInfo value)
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

public static void Serialize_vci_lightmap_offset(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void Serialize_vci_lightmap_scale(JsonFormatter f, Single[] value)
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
