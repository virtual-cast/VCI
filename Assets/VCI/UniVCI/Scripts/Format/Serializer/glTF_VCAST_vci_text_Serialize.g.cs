using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_text_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_text value)
{
    f.BeginMap();


    if(value.text!=null){
        f.Key("text");
        glTF_VCAST_vci_text_Serializevci_text(f, value.text);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_text_Serializevci_text(JsonFormatter f, TextJsonObject value)
{
    f.BeginMap();


    if(!string.IsNullOrEmpty(value.fontName)){
        f.Key("fontName");
        f.Value(value.fontName);
    }

    if(!string.IsNullOrEmpty(value.text)){
        f.Key("text");
        f.Value(value.text);
    }

    if(true){
        f.Key("richText");
        f.Value(value.richText);
    }

    if(true){
        f.Key("fontSize");
        f.Value(value.fontSize);
    }

    if(true){
        f.Key("autoSize");
        f.Value(value.autoSize);
    }

    if(true){
        f.Key("fontStyle");
        f.Value(value.fontStyle);
    }

    if(value.color!=null&&value.color.Length>=4){
        f.Key("color");
        glTF_VCAST_vci_text_Serializevci_text_color(f, value.color);
    }

    if(true){
        f.Key("enableVertexGradient");
        f.Value(value.enableVertexGradient);
    }

    if(value.topLeftColor!=null&&value.topLeftColor.Length>=4){
        f.Key("topLeftColor");
        glTF_VCAST_vci_text_Serializevci_text_topLeftColor(f, value.topLeftColor);
    }

    if(value.topRightColor!=null&&value.topRightColor.Length>=4){
        f.Key("topRightColor");
        glTF_VCAST_vci_text_Serializevci_text_topRightColor(f, value.topRightColor);
    }

    if(value.bottomLeftColor!=null&&value.bottomLeftColor.Length>=4){
        f.Key("bottomLeftColor");
        glTF_VCAST_vci_text_Serializevci_text_bottomLeftColor(f, value.bottomLeftColor);
    }

    if(value.bottomRightColor!=null&&value.bottomRightColor.Length>=4){
        f.Key("bottomRightColor");
        glTF_VCAST_vci_text_Serializevci_text_bottomRightColor(f, value.bottomRightColor);
    }

    if(true){
        f.Key("characterSpacing");
        f.Value(value.characterSpacing);
    }

    if(true){
        f.Key("wordSpacing");
        f.Value(value.wordSpacing);
    }

    if(true){
        f.Key("lineSpacing");
        f.Value(value.lineSpacing);
    }

    if(true){
        f.Key("paragraphSpacing");
        f.Value(value.paragraphSpacing);
    }

    if(true){
        f.Key("alignment");
        f.Value(value.alignment);
    }

    if(true){
        f.Key("enableWordWrapping");
        f.Value(value.enableWordWrapping);
    }

    if(true){
        f.Key("overflowMode");
        f.Value(value.overflowMode);
    }

    if(true){
        f.Key("enableKerning");
        f.Value(value.enableKerning);
    }

    if(true){
        f.Key("extraPadding");
        f.Value(value.extraPadding);
    }

    if(value.margin!=null&&value.margin.Length>=4){
        f.Key("margin");
        glTF_VCAST_vci_text_Serializevci_text_margin(f, value.margin);
    }

    f.EndMap();
}

public static void glTF_VCAST_vci_text_Serializevci_text_color(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_text_Serializevci_text_topLeftColor(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_text_Serializevci_text_topRightColor(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_text_Serializevci_text_bottomLeftColor(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_text_Serializevci_text_bottomRightColor(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void glTF_VCAST_vci_text_Serializevci_text_margin(JsonFormatter f, Single[] value)
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
