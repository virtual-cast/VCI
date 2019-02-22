using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;


/// <summary>
/// 複数の項目があるが一つのGLTF_Extensionとして扱う。
/// そのため、一つのファイルにまとめた。
/// </summary>
namespace UniGLTF
{
    #region meta
    /// <summary>
    /// gltf.extension 
    /// </summary>
    public partial class glTF_extensions : ExtensionsBase<glTF_extensions>
    {
        public glTF_VCAST_vci_meta VCAST_vci_meta;
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_meta
    {
        public static string ExtensionName
        {
            get { return "VCAST_vci"; }
        }

        public enum LicenseType
        {
            Redistribution_Prohibited,
            CC0,
            CC_BY,
            CC_BY_NC,
            CC_BY_SA,
            CC_BY_NC_SA,
            CC_BY_ND,
            CC_BY_NC_ND,
            Other
        }

        public enum ScriptFormat
        {
            luaText,
            luaBinary,
        }

        // from UniVCI-0.5
        public string exporterVCIVersion;

        public string specVersion;

        [JsonSchema(Description = "Title of VRM model")]
        public string title;

        [JsonSchema(Description = "Version of VRM model")]
        public string version;

        [JsonSchema(Description = "Author of VRM model")]
        public string author;

        [JsonSchema(Description = "Contact Information of VRM model author")]
        public string contactInformation;

        [JsonSchema(Description = "Reference of VRM model")]
        public string reference;

        [JsonSchema(Description = "description")]
        public string description;

        [JsonSchema(Description = "Thumbnail of VRM model")]
        public int thumbnail = -1;

        #region Distribution License
        [JsonSchema(Description = "Model Data License type", EnumSerializationType = EnumSerializationType.AsLowerString)]
        public LicenseType modelDataLicenseType;

        [JsonSchema(Description = "If Other is selected, put the URL link of the license document here.")]
        public string modelDataOtherLicenseUrl;

        [JsonSchema(Description = "Script License type", EnumSerializationType = EnumSerializationType.AsLowerString)]
        public LicenseType scriptLicenseType;

        [JsonSchema(Description = "If Other is selected, put the URL link of the license document here.")]
        public string scriptOtherLicenseUrl;
        #endregion

        #region Script
        [JsonSchema(Description = "Script write protected")]
        public bool scriptWriteProtected;

        [JsonSchema(Description = "Script enable debugging")]
        public bool scriptEnableDebugging;

        [JsonSchema(Description = "Script Format", EnumSerializationType = EnumSerializationType.AsString)]
        public ScriptFormat scriptFormat;
        #endregion
    }
    #endregion

    #region audio
    /// <summary>
    /// gltf.extension
    /// </summary>
    public partial class glTF_extensions : ExtensionsBase<glTF_extensions>
    {
        public glTF_VCAST_vci_audios VCAST_vci_audios;
    }

    /// <summary>
    /// Extension root.
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_audios
    {
        [JsonSchema(Required = true, MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTF_VCAST_vci_audio> audios;
    }

    /// <summary>
    /// sound item
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_audio
    {
        public string name;

        [JsonSchema(Required = true, Minimum = 0)]
        public int bufferView = -1;

        [JsonSchema(EnumValues = new object[] { "audio/wav" }, EnumSerializationType = EnumSerializationType.AsString)]
        public string mimeType;
    }
    #endregion

    #region script
    /// <summary>
    /// gltf.extension 
    /// </summary>
    public partial class glTF_extensions : ExtensionsBase<glTF_extensions>
    {
        public glTF_VCAST_vci_embedded_script VCAST_vci_embedded_script;
    }

    public class EnumStringValueAttribute : Attribute
    {
        public string Value
        {
            get;
            private set;
        }
        public EnumStringValueAttribute(string value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_embedded_script
    {
        [JsonSchema(Required = true, MinItems = 1)]
        public List<glTF_VCAST_vci_embedded_script_source> scripts = new List<glTF_VCAST_vci_embedded_script_source>();

        public int entryPoint;
    }

    public enum ScriptMimeType
    {
        [EnumStringValue("x-application/lua")]
        X_APPLICATION_LUA,
    }

    public enum TargetEngine
    {
        MoonSharp,
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_embedded_script_source
    {
        [JsonSchema(Description = "main.lua")]
        public string name;

        [JsonSchema(EnumSerializationType = EnumSerializationType.AsLowerString)]
        public ScriptMimeType mimeType; // "x-application/lua",

        [JsonSchema(Description = "moonsharp", EnumSerializationType = EnumSerializationType.AsLowerString)]
        public TargetEngine targetEngine;

        [JsonSchema(Required = true, Minimum = 0)]
        public int source = -1;
    }
    #endregion

    #region material
    /// <summary>
    /// gltf.extension
    /// </summary>
    public partial class glTF_extensions : ExtensionsBase<glTF_extensions>
    {
        public glTF_VCAST_vci_material_unity VCAST_vci_material_unity;
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_material_unity
    {
        /// <summary>
        /// Same as vrm material
        /// </summary>
        public List<VRM.glTF_VRM_Material> materials;
    }
    #endregion

    #region collider
    /// <summary>
    /// node.extension
    /// </summary>
    public partial class glTFNode_extensions : ExtensionsBase<glTFNode_extensions>
    {
        public glTF_VCAST_vci_colliders VCAST_vci_collider;
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_colliders
    {
        [JsonSchema(MinItems = 1)] public List<glTF_VCAST_vci_Collider> colliders;
    }

    /// <summary>
    /// Collider info
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_Collider
    {
        private const string BoxColliderName = "box";
        private const string SphereColliderName = "sphere";
        private const string CapsuleColliderName = "capsule";

        [JsonSchema(Required = true, EnumSerializationType = EnumSerializationType.AsString,
            EnumValues = new object[] { BoxColliderName, SphereColliderName, CapsuleColliderName })]
        public string type;

        [JsonSchema(MinItems = 3, MaxItems = 3)] public float[] center;

        [JsonSchema(MinItems = 1, MaxItems = 3)] public float[] shape;

        [JsonSchema(Description = @"This collider is used for grab")] public bool grabable;

        [JsonSchema(Description = @"Use gravity")] public bool useGravity;

        [JsonSchema(Description = @"Is Trigger")] public bool isTrigger;

        [JsonSchema(Description = @"Physic Material")] public glTF_VCAST_vci_PhysicMaterial physicMaterial;

        public static Collider AddColliderComponent(GameObject go, glTF_VCAST_vci_Collider collider)
        {
            switch (collider.type)
            {
                case BoxColliderName:
                    {
                        var unityCollider = go.AddComponent<BoxCollider>();
                        unityCollider.center = new Vector3(collider.center[0], collider.center[1], collider.center[2])
                            .ReverseZ();
                        unityCollider.size = new Vector3(collider.shape[0], collider.shape[1], collider.shape[2]);
                        unityCollider.isTrigger = collider.isTrigger;
                        if (collider.physicMaterial != null)
                        {
                            glTF_VCAST_vci_PhysicMaterial.AddPhysicMaterial(unityCollider, collider.physicMaterial);
                        }
                        return unityCollider;
                    }
                case CapsuleColliderName:
                    {
                        var unityCollider = go.AddComponent<CapsuleCollider>();
                        unityCollider.center = new Vector3(collider.center[0], collider.center[1], collider.center[2])
                            .ReverseZ();
                        unityCollider.radius = collider.shape[0];
                        unityCollider.height = collider.shape[1];
                        unityCollider.direction = (int)collider.shape[2];
                        unityCollider.isTrigger = collider.isTrigger;
                        if (collider.physicMaterial != null)
                        {
                            glTF_VCAST_vci_PhysicMaterial.AddPhysicMaterial(unityCollider, collider.physicMaterial);
                        }
                        return unityCollider;
                    }
                default:
                    {
                        var unityCollider = go.AddComponent<SphereCollider>();
                        unityCollider.center = new Vector3(collider.center[0], collider.center[1], collider.center[2])
                            .ReverseZ();
                        unityCollider.radius = collider.shape[0];
                        unityCollider.isTrigger = collider.isTrigger;
                        if (collider.physicMaterial != null)
                        {
                            glTF_VCAST_vci_PhysicMaterial.AddPhysicMaterial(unityCollider, collider.physicMaterial);
                        }
                        return unityCollider;
                    }
            }
        }

        public static glTF_VCAST_vci_Collider GetglTfColliderFromUnityCollider(Collider unityCollider)
        {
            var type = unityCollider.GetType();
            var collider = new glTF_VCAST_vci_Collider();
            if (type == typeof(BoxCollider))
            {
                var box = unityCollider as BoxCollider;
                collider.type = BoxColliderName;
                collider.center = box.center.ReverseZ().ToArray();
                collider.shape = box.size.ToArray();
                collider.isTrigger = box.isTrigger;
                if (unityCollider.sharedMaterial != null)
                {
                    collider.physicMaterial = glTF_VCAST_vci_PhysicMaterial.GetglTFPhysicMaterial(unityCollider.sharedMaterial);
                }
                return collider;
            }
            else if (type == typeof(CapsuleCollider))
            {
                var capsule = unityCollider as CapsuleCollider;
                collider.type = CapsuleColliderName;
                collider.center = capsule.center.ReverseZ().ToArray();
                collider.shape = new float[3];
                collider.shape[0] = capsule.radius;
                collider.shape[1] = capsule.height;
                collider.shape[2] = capsule.direction;
                collider.isTrigger = capsule.isTrigger;
                if (unityCollider.sharedMaterial != null)
                {
                    collider.physicMaterial = glTF_VCAST_vci_PhysicMaterial.GetglTFPhysicMaterial(unityCollider.sharedMaterial);
                }
                return collider;
            }
            else if (type == typeof(SphereCollider))
            {
                var sphere = unityCollider as SphereCollider;
                collider.type = SphereColliderName;
                collider.center = sphere.center.ReverseZ().ToArray();
                collider.shape = new float[1];
                collider.shape[0] = sphere.radius;
                collider.isTrigger = sphere.isTrigger;
                if (unityCollider.sharedMaterial != null)
                {
                    collider.physicMaterial = glTF_VCAST_vci_PhysicMaterial.GetglTFPhysicMaterial(unityCollider.sharedMaterial);
                }
                return collider;
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// PhysicMaterial info
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_PhysicMaterial
    {
        private const string AverageCombine = "average";
        private const string MinimumCombine = "minimum";
        private const string MaximumCombine = "maximum";
        private const string MultiplyCombine = "multiply";

        public float dynamicFriction;
        public float staticFriction;
        public float bounciness;

        [JsonSchema(Required = true, EnumSerializationType = EnumSerializationType.AsString,
            EnumValues = new object[] { AverageCombine, MinimumCombine, MaximumCombine, MultiplyCombine })]
        public string frictionCombine;
        [JsonSchema(Required = true, EnumSerializationType = EnumSerializationType.AsString,
            EnumValues = new object[] { AverageCombine, MinimumCombine, MaximumCombine, MultiplyCombine })]
        public string bounceCombine;

        public static PhysicMaterialCombine GetCombineFromString(string combine)
        {
            switch (combine)
            {
                case AverageCombine:
                    return PhysicMaterialCombine.Average;
                case MinimumCombine:
                    return PhysicMaterialCombine.Minimum;
                case MaximumCombine:
                    return PhysicMaterialCombine.Maximum;
                case MultiplyCombine:
                    return PhysicMaterialCombine.Multiply;
                default:
                    return PhysicMaterialCombine.Average;
            }
        }

        public static string GetStringFromCombine(PhysicMaterialCombine combine)
        {
            switch (combine)
            {
                case PhysicMaterialCombine.Average:
                    return AverageCombine;
                case PhysicMaterialCombine.Minimum:
                    return MinimumCombine;
                case PhysicMaterialCombine.Maximum:
                    return MaximumCombine;
                case PhysicMaterialCombine.Multiply:
                    return MultiplyCombine;
                default:
                    return AverageCombine;
            }
        }

        public static void AddPhysicMaterial(Collider collider, glTF_VCAST_vci_PhysicMaterial material)
        {
            var physicMaterial = new PhysicMaterial();
            physicMaterial.dynamicFriction = material.dynamicFriction;
            physicMaterial.staticFriction = material.staticFriction;
            physicMaterial.bounciness = material.bounciness;
            physicMaterial.frictionCombine = glTF_VCAST_vci_PhysicMaterial.GetCombineFromString(material.frictionCombine);
            physicMaterial.bounceCombine = glTF_VCAST_vci_PhysicMaterial.GetCombineFromString(material.bounceCombine);
            collider.sharedMaterial = physicMaterial;
        }

        public static glTF_VCAST_vci_PhysicMaterial GetglTFPhysicMaterial(PhysicMaterial material)
        {
            var result = new glTF_VCAST_vci_PhysicMaterial();
            result.dynamicFriction = material.dynamicFriction;
            result.staticFriction = material.staticFriction;
            result.bounciness = material.bounciness;
            result.frictionCombine = glTF_VCAST_vci_PhysicMaterial.GetStringFromCombine(material.frictionCombine);
            result.bounceCombine = glTF_VCAST_vci_PhysicMaterial.GetStringFromCombine(material.bounceCombine);
            return result;
        }
    }
    #endregion

    #region rigidbody
    /// <summary>
    /// node.extension
    /// </summary>
    public partial class glTFNode_extensions : ExtensionsBase<glTFNode_extensions>
    {
        public glTF_VCAST_vci_rigidbody VCAST_vci_rigidbody;
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_rigidbody
    {
        public static string ExtensionName
        {
            get { return "VRM_rigidbody"; }
        }

        [JsonSchema(MinItems = 1)]
        public List<glTF_VCAST_vci_Rigidbody> rigidbodies;
    }
    /// <summary>
    /// Rigidbody info
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_Rigidbody
    {
        public float mass = 1.0f;
        public float drag = 0.0f;
        public float angularDrag = 0.05f;
        public bool useGravity = true;
        public bool isKinematic = false;

        public bool freezePositionX = false;
        public bool freezePositionY = false;
        public bool freezePositionZ = false;

        public bool freezeRotationX = false;
        public bool freezeRotationY = false;
        public bool freezeRotationZ = false;



        public static Rigidbody AddRigidbodyComponent(GameObject go, glTF_VCAST_vci_Rigidbody rigidbody)
        {
            Rigidbody result = go.GetOrAddComponent<Rigidbody>();
            result.mass = rigidbody.mass;
            result.drag = rigidbody.drag;
            result.angularDrag = rigidbody.angularDrag;
            result.useGravity = rigidbody.useGravity;
            result.isKinematic = rigidbody.isKinematic;
            result.constraints = GetConstraints(rigidbody);
            return result;
        }

        public static glTF_VCAST_vci_Rigidbody GetglTfRigidbodyFromUnityRigidbody(Rigidbody rigidbody)
        {
            glTF_VCAST_vci_Rigidbody result = new glTF_VCAST_vci_Rigidbody();
            result.mass = rigidbody.mass;
            result.drag = rigidbody.drag;
            result.angularDrag = rigidbody.angularDrag;
            result.useGravity = rigidbody.useGravity;
            result.isKinematic = rigidbody.isKinematic;
            result.freezePositionX = rigidbody.constraints.HasFlag(RigidbodyConstraints.FreezePositionX);
            result.freezePositionY = rigidbody.constraints.HasFlag(RigidbodyConstraints.FreezePositionY);
            result.freezePositionZ = rigidbody.constraints.HasFlag(RigidbodyConstraints.FreezePositionZ);
            result.freezeRotationX = rigidbody.constraints.HasFlag(RigidbodyConstraints.FreezeRotationX);
            result.freezeRotationY = rigidbody.constraints.HasFlag(RigidbodyConstraints.FreezeRotationY);
            result.freezeRotationZ = rigidbody.constraints.HasFlag(RigidbodyConstraints.FreezeRotationZ);
            return result;
        }

        private static RigidbodyConstraints GetConstraints(glTF_VCAST_vci_Rigidbody rigidbody)
        {
            return 
            ((rigidbody.freezePositionX)?RigidbodyConstraints.FreezePositionX:0) |
            ((rigidbody.freezePositionY)?RigidbodyConstraints.FreezePositionY:0) |
            ((rigidbody.freezePositionZ)?RigidbodyConstraints.FreezePositionZ:0) |
            ((rigidbody.freezeRotationX)?RigidbodyConstraints.FreezeRotationX:0) |
            ((rigidbody.freezeRotationY)?RigidbodyConstraints.FreezeRotationY:0) |
            ((rigidbody.freezeRotationZ)?RigidbodyConstraints.FreezeRotationZ:0) 
             ;
            
        }

    }
    #endregion

    #region joint
    /// <summary>
    /// node.extension
    /// </summary>
    public partial class glTFNode_extensions : ExtensionsBase<glTFNode_extensions>
    {
        public glTF_VCAST_vci_joints VCAST_vci_joints;
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_joints
    {
        [JsonSchema(MinItems = 1)]
        public List<glTF_VCAST_vci_joint> joints;
    }

    /// <summary>
    /// Rigidbody info
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_joint
    {
        private const string HingeJointName = "hinge";
        private const string SpringJointName = "spring";

        [JsonSchema(Required = true, EnumValues = new object[] { HingeJointName, SpringJointName }, EnumSerializationType = EnumSerializationType.AsUpperString)]
        public string type;

        public int nodeIndex = -1;

        [JsonSchema(MinItems = 3, MaxItems = 3)]
        public float[] anchor;

        [JsonSchema(MinItems = 3, MaxItems = 3)]
        public float[] axis;

        public bool autoConfigureConnectedAnchor = true;

        [JsonSchema(MinItems = 3, MaxItems = 3)]
        public float[] connectedAnchor;


        public float spring;
        public float damper;
        public float tolerance;

        public static Joint AddJointComponent(GameObject go, glTF_VCAST_vci_joint joint, List<Transform> nodes)
        {
            Joint result = GetJoint(go, joint);
            if (joint.nodeIndex != -1)
            {
                result.connectedBody = nodes[joint.nodeIndex].GetComponent<Rigidbody>();
            }
            result.anchor = (new Vector3(joint.anchor[0], joint.anchor[1], joint.anchor[2])).ReverseZ();
            result.axis = (new Vector3(joint.axis[0], joint.axis[1], joint.axis[2])).ReverseZ();
            result.autoConfigureConnectedAnchor = joint.autoConfigureConnectedAnchor;
            result.connectedAnchor = (new Vector3(joint.connectedAnchor[0], joint.connectedAnchor[1], joint.connectedAnchor[2])).ReverseZ();

            if (result.GetType() == typeof(SpringJoint))
            {
                var spring = result as SpringJoint;
                spring.spring = joint.spring;
                spring.damper = joint.damper;
                spring.tolerance = joint.tolerance;
            }
            return result;
        }

        public static glTF_VCAST_vci_joint GetglTFJointFromUnityJoint(Joint joint, List<Transform> nodes)
        {
            glTF_VCAST_vci_joint result = new glTF_VCAST_vci_joint();
            result.nodeIndex = nodes.FindIndex(x => x == joint.connectedBody.gameObject.transform);
            result.anchor = joint.anchor.ReverseZ().ToArray();
            result.axis = joint.axis.ReverseZ().ToArray();
            result.autoConfigureConnectedAnchor = joint.autoConfigureConnectedAnchor;
            result.connectedAnchor = joint.connectedAnchor.ReverseZ().ToArray();

            if (joint.GetType() == typeof(SpringJoint))
            {
                var spring = joint as SpringJoint;
                result.type = SpringJointName;
                result.spring = spring.spring;
                result.damper = spring.damper;
                result.tolerance = spring.tolerance;
            }

            return result;
        }

        private static Joint GetJoint(GameObject go, glTF_VCAST_vci_joint joint)
        {
            switch (joint.type)
            {
                case HingeJointName:
                    return go.AddComponent<HingeJoint>();
                case SpringJointName:
                    return go.AddComponent<SpringJoint>();
                default:
                    return go.AddComponent<HingeJoint>();
            }
        }
    }
    #endregion

    #region item
    /// <summary>
    /// node.extension
    /// </summary>
    public partial class glTFNode_extensions : ExtensionsBase<glTFNode_extensions>
    {
        public glTF_VCAST_vci_item VCAST_vci_item = null;
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_item
    {
        public bool grabbable;
        public bool IsGrabbable
        {
            get
            {
                return grabbable;
            }
        }

        public bool scalable;
        public bool IsScalable
        {
            get
            {
                return scalable;
            }
        }

        public bool uniformScaling;
        public bool IsUniformScaling
        {
            get
            {
                return uniformScaling;
            }
        }

        public int groupId;
        public int GroupId
        {
            get
            {
                return groupId;
            }
        }
    }
    #endregion
}
