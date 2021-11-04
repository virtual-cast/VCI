using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_meta_Deserializer
    {


public static glTF_VCAST_vci_meta Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_meta();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="exporterVCIVersion"){
            value.exporterVCIVersion = kv.Value.GetString();
            continue;
        }

        if(key=="specVersion"){
            value.specVersion = kv.Value.GetString();
            continue;
        }

        if(key=="title"){
            value.title = kv.Value.GetString();
            continue;
        }

        if(key=="version"){
            value.version = kv.Value.GetString();
            continue;
        }

        if(key=="author"){
            value.author = kv.Value.GetString();
            continue;
        }

        if(key=="contactInformation"){
            value.contactInformation = kv.Value.GetString();
            continue;
        }

        if(key=="reference"){
            value.reference = kv.Value.GetString();
            continue;
        }

        if(key=="description"){
            value.description = kv.Value.GetString();
            continue;
        }

        if(key=="thumbnail"){
            value.thumbnail = kv.Value.GetInt32();
            continue;
        }

        if(key=="modelDataLicenseType"){
            value.modelDataLicenseType = kv.Value.GetString();
            continue;
        }

        if(key=="modelDataOtherLicenseUrl"){
            value.modelDataOtherLicenseUrl = kv.Value.GetString();
            continue;
        }

        if(key=="scriptLicenseType"){
            value.scriptLicenseType = kv.Value.GetString();
            continue;
        }

        if(key=="scriptOtherLicenseUrl"){
            value.scriptOtherLicenseUrl = kv.Value.GetString();
            continue;
        }

        if(key=="scriptWriteProtected"){
            value.scriptWriteProtected = kv.Value.GetBoolean();
            continue;
        }

        if(key=="scriptEnableDebugging"){
            value.scriptEnableDebugging = kv.Value.GetBoolean();
            continue;
        }

    }
    return value;
}

    } // class
} // namespace
