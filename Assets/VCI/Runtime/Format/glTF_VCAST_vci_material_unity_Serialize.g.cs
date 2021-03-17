
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_material_unity_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_material_unity value)
{
    f.BeginMap();


    if(value.materials!=null&&value.materials.Count>=0){
        f.Key("materials");                
        Serialize_vci_materials(f, value.materials);
    }

    f.EndMap();
}

public static void Serialize_vci_materials(JsonFormatter f, List<glTF_VCI_Material> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    Serialize_vci_materials_ITEM(f, item);

    }
    f.EndList();
}

public static void Serialize_vci_materials_ITEM(JsonFormatter f, glTF_VCI_Material value)
{
    f.BeginMap();


    if(!string.IsNullOrEmpty(value.name)){
        f.Key("name");                
        f.Value(value.name);
    }

    if(!string.IsNullOrEmpty(value.shader)){
        f.Key("shader");                
        f.Value(value.shader);
    }

    if(true){
        f.Key("renderQueue");                
        f.Value(value.renderQueue);
    }

    if(value.floatProperties!=null){
        f.Key("floatProperties");                
        Serialize_vci_materials__floatProperties(f, value.floatProperties);
    }

    if(value.vectorProperties!=null){
        f.Key("vectorProperties");                
        Serialize_vci_materials__vectorProperties(f, value.vectorProperties);
    }

    if(value.textureProperties!=null){
        f.Key("textureProperties");                
        Serialize_vci_materials__textureProperties(f, value.textureProperties);
    }

    if(value.keywordMap!=null){
        f.Key("keywordMap");                
        Serialize_vci_materials__keywordMap(f, value.keywordMap);
    }

    if(value.tagMap!=null){
        f.Key("tagMap");                
        Serialize_vci_materials__tagMap(f, value.tagMap);
    }

    f.EndMap();
}

public static void Serialize_vci_materials__floatProperties(JsonFormatter f, Dictionary<string, Single> value)
{
    f.BeginMap();
    foreach(var kv in value)
    {
        f.Key(kv.Key);
        f.Value(kv.Value);
    }
    f.EndMap();
}

public static void Serialize_vci_materials__vectorProperties(JsonFormatter f, Dictionary<string, Single[]> value)
{
    f.BeginMap();
    foreach(var kv in value)
    {
        f.Key(kv.Key);
        Serialize_vci_materials__vectorProperties_ITEM(f, kv.Value);
    }
    f.EndMap();
}

public static void Serialize_vci_materials__vectorProperties_ITEM(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void Serialize_vci_materials__textureProperties(JsonFormatter f, Dictionary<string, Int32> value)
{
    f.BeginMap();
    foreach(var kv in value)
    {
        f.Key(kv.Key);
        f.Value(kv.Value);
    }
    f.EndMap();
}

public static void Serialize_vci_materials__keywordMap(JsonFormatter f, Dictionary<string, Boolean> value)
{
    f.BeginMap();
    foreach(var kv in value)
    {
        f.Key(kv.Key);
        f.Value(kv.Value);
    }
    f.EndMap();
}

public static void Serialize_vci_materials__tagMap(JsonFormatter f, Dictionary<string, String> value)
{
    f.BeginMap();
    foreach(var kv in value)
    {
        f.Key(kv.Key);
        f.Value(kv.Value);
    }
    f.EndMap();
}

} // VciSerializer
} // VCI
