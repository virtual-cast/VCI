using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_meta_Serializer
    {


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_meta value)
{
    f.BeginMap();


    if(!string.IsNullOrEmpty(value.exporterVCIVersion)){
        f.Key("exporterVCIVersion");
        f.Value(value.exporterVCIVersion);
    }

    if(!string.IsNullOrEmpty(value.specVersion)){
        f.Key("specVersion");
        f.Value(value.specVersion);
    }

    if(!string.IsNullOrEmpty(value.title)){
        f.Key("title");
        f.Value(value.title);
    }

    if(!string.IsNullOrEmpty(value.version)){
        f.Key("version");
        f.Value(value.version);
    }

    if(!string.IsNullOrEmpty(value.author)){
        f.Key("author");
        f.Value(value.author);
    }

    if(!string.IsNullOrEmpty(value.contactInformation)){
        f.Key("contactInformation");
        f.Value(value.contactInformation);
    }

    if(!string.IsNullOrEmpty(value.reference)){
        f.Key("reference");
        f.Value(value.reference);
    }

    if(!string.IsNullOrEmpty(value.description)){
        f.Key("description");
        f.Value(value.description);
    }

    if(true){
        f.Key("thumbnail");
        f.Value(value.thumbnail);
    }

    if(!string.IsNullOrEmpty(value.modelDataLicenseType)){
        f.Key("modelDataLicenseType");
        f.Value(value.modelDataLicenseType);
    }

    if(!string.IsNullOrEmpty(value.modelDataOtherLicenseUrl)){
        f.Key("modelDataOtherLicenseUrl");
        f.Value(value.modelDataOtherLicenseUrl);
    }

    if(!string.IsNullOrEmpty(value.scriptLicenseType)){
        f.Key("scriptLicenseType");
        f.Value(value.scriptLicenseType);
    }

    if(!string.IsNullOrEmpty(value.scriptOtherLicenseUrl)){
        f.Key("scriptOtherLicenseUrl");
        f.Value(value.scriptOtherLicenseUrl);
    }

    if(true){
        f.Key("scriptWriteProtected");
        f.Value(value.scriptWriteProtected);
    }

    if(true){
        f.Key("scriptEnableDebugging");
        f.Value(value.scriptEnableDebugging);
    }

    f.EndMap();
}

    } // class
} // namespace
