using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class glTF_VCAST_vci_joints_Deserializer
    {


public static glTF_VCAST_vci_joints Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_joints();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="joints"){
            value.joints = glTF_VCAST_vci_joints_Deserializevci_joints(kv.Value);
            continue;
        }

    }
    return value;
}

public static List<VCI.JointJsonObject> glTF_VCAST_vci_joints_Deserializevci_joints(JsonNode parsed)
{
    var value = new List<JointJsonObject>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_VCAST_vci_joints_Deserializevci_joints_ITEM(x));
    }
	return value;
}
public static JointJsonObject glTF_VCAST_vci_joints_Deserializevci_joints_ITEM(JsonNode parsed)
{
    var value = new JointJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="type"){
            value.type = kv.Value.GetString();
            continue;
        }

        if(key=="nodeIndex"){
            value.nodeIndex = kv.Value.GetInt32();
            continue;
        }

        if(key=="anchor"){
            value.anchor = glTF_VCAST_vci_joints_Deserializevci_joints__anchor(kv.Value);
            continue;
        }

        if(key=="axis"){
            value.axis = glTF_VCAST_vci_joints_Deserializevci_joints__axis(kv.Value);
            continue;
        }

        if(key=="autoConfigureConnectedAnchor"){
            value.autoConfigureConnectedAnchor = kv.Value.GetBoolean();
            continue;
        }

        if(key=="connectedAnchor"){
            value.connectedAnchor = glTF_VCAST_vci_joints_Deserializevci_joints__connectedAnchor(kv.Value);
            continue;
        }

        if(key=="enableCollision"){
            value.enableCollision = kv.Value.GetBoolean();
            continue;
        }

        if(key=="enablePreprocessing"){
            value.enablePreprocessing = kv.Value.GetBoolean();
            continue;
        }

        if(key=="massScale"){
            value.massScale = kv.Value.GetSingle();
            continue;
        }

        if(key=="connectedMassScale"){
            value.connectedMassScale = kv.Value.GetSingle();
            continue;
        }

        if(key=="useSpring"){
            value.useSpring = kv.Value.GetBoolean();
            continue;
        }

        if(key=="spring"){
            value.spring = glTF_VCAST_vci_joints_Deserializevci_joints__spring(kv.Value);
            continue;
        }

        if(key=="useLimits"){
            value.useLimits = kv.Value.GetBoolean();
            continue;
        }

        if(key=="limits"){
            value.limits = glTF_VCAST_vci_joints_Deserializevci_joints__limits(kv.Value);
            continue;
        }

    }
    return value;
}

public static Single[] glTF_VCAST_vci_joints_Deserializevci_joints__anchor(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_joints_Deserializevci_joints__axis(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static Single[] glTF_VCAST_vci_joints_Deserializevci_joints__connectedAnchor(JsonNode parsed)
{
    var value = new Single[parsed.GetArrayCount()];
    int i=0;
    foreach(var x in parsed.ArrayItems())
    {
        value[i++] = x.GetSingle();
    }
	return value;
} 

public static SpringJsonObject glTF_VCAST_vci_joints_Deserializevci_joints__spring(JsonNode parsed)
{
    var value = new SpringJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="spring"){
            value.spring = kv.Value.GetSingle();
            continue;
        }

        if(key=="damper"){
            value.damper = kv.Value.GetSingle();
            continue;
        }

        if(key=="minDistance"){
            value.minDistance = kv.Value.GetSingle();
            continue;
        }

        if(key=="maxDistance"){
            value.maxDistance = kv.Value.GetSingle();
            continue;
        }

        if(key=="tolerance"){
            value.tolerance = kv.Value.GetSingle();
            continue;
        }

        if(key=="targetPosition"){
            value.targetPosition = kv.Value.GetSingle();
            continue;
        }

    }
    return value;
}

public static LimitsJsonObject glTF_VCAST_vci_joints_Deserializevci_joints__limits(JsonNode parsed)
{
    var value = new LimitsJsonObject();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="min"){
            value.min = kv.Value.GetSingle();
            continue;
        }

        if(key=="max"){
            value.max = kv.Value.GetSingle();
            continue;
        }

        if(key=="bounciness"){
            value.bounciness = kv.Value.GetSingle();
            continue;
        }

        if(key=="bounceMinVelocity"){
            value.bounceMinVelocity = kv.Value.GetSingle();
            continue;
        }

        if(key=="contactDistance"){
            value.contactDistance = kv.Value.GetSingle();
            continue;
        }

    }
    return value;
}

    } // class
} // namespace
