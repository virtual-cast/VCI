using System;
using System.Collections.Generic;
using UnityEngine;
using VCI;
using VCIJSON;


/// <summary>
/// 複数の項目があるが一つのGLTF_Extensionとして扱う。
/// そのため、一つのファイルにまとめた。
/// </summary>
namespace VCIGLTF
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
        public static string ExtensionName => "VCAST_vci";

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

        [JsonSchema(Description = "Title of VCI model")]
        public string title;

        [JsonSchema(Description = "Version of VCI model")]
        public string version;

        [JsonSchema(Description = "Author of VCI model")]
        public string author;

        [JsonSchema(Description = "Contact Information of VCI model author")]
        public string contactInformation;

        [JsonSchema(Description = "Reference of VCI model")]
        public string reference;

        [JsonSchema(Description = "description")]
        public string description;

        [JsonSchema(Description = "Thumbnail of VCI model")]
        public int thumbnail = -1;

        #region Distribution License

        [JsonSchema(Description = "Model Data License type",
            EnumSerializationType = EnumSerializationType.AsLowerString)]
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

    #region Attachable

    public partial class glTFNode_extensions : ExtensionsBase<glTFNode_extensions>
    {
        public glTF_VCAST_vci_attachable VCAST_vci_attachable;
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_attachable
    {
        public List<string> attachableHumanBodyBones = new List<string>();
        public float attachableDistance;
        public bool scalable;
        public Vector3 offset;
    }

    #endregion

    #region RectTransform

    public partial class glTFNode_extensions : ExtensionsBase<glTFNode_extensions>
    {
        public glTF_VCAST_vci_rectTransform VCAST_vci_rectTransform;
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_rectTransform
    {
        public static string ExtensionName => "VCAST_vci_rectTransform";

        public VCI.glTF_VCAST_vci_RectTransform rectTransform;
    }

    #endregion

    #region Text

    public partial class glTFNode_extensions : ExtensionsBase<glTFNode_extensions>
    {
        public glTF_VCAST_vci_text VCAST_vci_text;
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_text
    {
        public static string ExtensionName => "VCAST_vci_text";

        public VCI.glTF_VCAST_vci_Text text;
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
    /// Extension root
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

        [JsonSchema(EnumValues = new object[] {"audio/wav", "audio/mp3"}, EnumSerializationType = EnumSerializationType.AsString)]
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
        public string Value { get; private set; }

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
        [EnumStringValue("x-application/lua")] X_APPLICATION_LUA,
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
        [JsonSchema(Description = "main.lua")] public string name;

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
        /// Same as vci material
        /// </summary>
        public List<VCI.glTF_VCI_Material> materials;
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
            EnumValues = new object[] {BoxColliderName, SphereColliderName, CapsuleColliderName})]
        public string type;

        [JsonSchema(MinItems = 3, MaxItems = 3)]
        public float[] center;

        [JsonSchema(MinItems = 1, MaxItems = 3)]
        public float[] shape;

        [JsonSchema(Description = @"This collider is used for grab")]
        public bool grabable;

        [JsonSchema(Description = @"Use gravity")]
        public bool useGravity;

        [JsonSchema(Description = @"Is Trigger")]
        public bool isTrigger;

        [JsonSchema(Description = @"Physic Material")]
        public glTF_VCAST_vci_PhysicMaterial physicMaterial;

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
                        glTF_VCAST_vci_PhysicMaterial.AddPhysicMaterial(unityCollider, collider.physicMaterial);
                    return unityCollider;
                }
                case CapsuleColliderName:
                {
                    var unityCollider = go.AddComponent<CapsuleCollider>();
                    unityCollider.center = new Vector3(collider.center[0], collider.center[1], collider.center[2])
                        .ReverseZ();
                    unityCollider.radius = collider.shape[0];
                    unityCollider.height = collider.shape[1];
                    unityCollider.direction = (int) collider.shape[2];
                    unityCollider.isTrigger = collider.isTrigger;
                    if (collider.physicMaterial != null)
                        glTF_VCAST_vci_PhysicMaterial.AddPhysicMaterial(unityCollider, collider.physicMaterial);
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
                        glTF_VCAST_vci_PhysicMaterial.AddPhysicMaterial(unityCollider, collider.physicMaterial);
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
                    collider.physicMaterial =
                        glTF_VCAST_vci_PhysicMaterial.GetglTFPhysicMaterial(unityCollider.sharedMaterial);
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
                    collider.physicMaterial =
                        glTF_VCAST_vci_PhysicMaterial.GetglTFPhysicMaterial(unityCollider.sharedMaterial);
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
                    collider.physicMaterial =
                        glTF_VCAST_vci_PhysicMaterial.GetglTFPhysicMaterial(unityCollider.sharedMaterial);
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
            EnumValues = new object[] {AverageCombine, MinimumCombine, MaximumCombine, MultiplyCombine})]
        public string frictionCombine;

        [JsonSchema(Required = true, EnumSerializationType = EnumSerializationType.AsString,
            EnumValues = new object[] {AverageCombine, MinimumCombine, MaximumCombine, MultiplyCombine})]
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
            physicMaterial.frictionCombine = GetCombineFromString(material.frictionCombine);
            physicMaterial.bounceCombine = GetCombineFromString(material.bounceCombine);
            collider.sharedMaterial = physicMaterial;
        }

        public static glTF_VCAST_vci_PhysicMaterial GetglTFPhysicMaterial(PhysicMaterial material)
        {
            var result = new glTF_VCAST_vci_PhysicMaterial();
            result.dynamicFriction = material.dynamicFriction;
            result.staticFriction = material.staticFriction;
            result.bounciness = material.bounciness;
            result.frictionCombine = GetStringFromCombine(material.frictionCombine);
            result.bounceCombine = GetStringFromCombine(material.bounceCombine);
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
        [JsonSchema(MinItems = 1)] public List<glTF_VCAST_vci_Rigidbody> rigidbodies;
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

        private static class InterpolateString
        {
            public const string None = "none";
            public const string Interpolate = "interpolate";
            public const string Extrapolate = "extrapolate";


            public static RigidbodyInterpolation GetInterpolationFromString(string name)
            {
                if (string.IsNullOrEmpty(name)) return RigidbodyInterpolation.None;

                switch (name.ToLower())
                {
                    case None:
                        return RigidbodyInterpolation.None;
                    case Interpolate:
                        return RigidbodyInterpolation.Interpolate;
                    case Extrapolate:
                        return RigidbodyInterpolation.Extrapolate;
                    default:
                        return RigidbodyInterpolation.Interpolate;
                }
            }

            public static string GetStringFromInterpolation(RigidbodyInterpolation mode)
            {
                switch (mode)
                {
                    case RigidbodyInterpolation.None:
                        return None;
                    case RigidbodyInterpolation.Interpolate:
                        return Interpolate;
                    case RigidbodyInterpolation.Extrapolate:
                        return Extrapolate;
                    default:
                        return Interpolate;
                }
            }
        }

        [JsonSchema(Required = true, EnumValues = new object[]
        {
            InterpolateString.None,
            InterpolateString.Interpolate,
            InterpolateString.Extrapolate
        }, EnumSerializationType = EnumSerializationType.AsUpperString)]
        public string interpolate;


        private static class CollisionDetectionModeString
        {
            public const string Discrete = "discrete";
            public const string Continuous = "continuous";
            public const string ContinuousDynamic = "continuousdynamic";

            public static CollisionDetectionMode GetDetectionModeFromString(string name)
            {
                if (string.IsNullOrEmpty(name)) return CollisionDetectionMode.Discrete;

                switch (name.ToLower())
                {
                    case Discrete:
                        return CollisionDetectionMode.Discrete;
                    case Continuous:
                        return CollisionDetectionMode.Continuous;
                    case ContinuousDynamic:
                        return CollisionDetectionMode.ContinuousDynamic;
                    default:
                        return CollisionDetectionMode.Discrete;
                }
            }

            public static string GetStringFromCollisionDetectionMode(CollisionDetectionMode mode)
            {
                switch (mode)
                {
                    case CollisionDetectionMode.Discrete:
                        return Discrete;
                    case CollisionDetectionMode.Continuous:
                        return Continuous;
                    case CollisionDetectionMode.ContinuousDynamic:
                        return ContinuousDynamic;
                    default:
                        return Discrete;
                }
            }
        }

        [JsonSchema(Required = true, EnumValues = new object[]
        {
            CollisionDetectionModeString.Discrete,
            CollisionDetectionModeString.Continuous,
            CollisionDetectionModeString.ContinuousDynamic
        }, EnumSerializationType = EnumSerializationType.AsUpperString)]
        public string collisionDetection;

        public bool freezePositionX = false;
        public bool freezePositionY = false;
        public bool freezePositionZ = false;

        public bool freezeRotationX = false;
        public bool freezeRotationY = false;
        public bool freezeRotationZ = false;


        public static Rigidbody AddRigidbodyComponent(GameObject go, glTF_VCAST_vci_Rigidbody rigidbody)
        {
            var result = go.GetOrAddComponent<Rigidbody>();
            result.mass = rigidbody.mass;
            result.drag = rigidbody.drag;
            result.angularDrag = rigidbody.angularDrag;
            result.useGravity = rigidbody.useGravity;
            result.isKinematic = rigidbody.isKinematic;
            result.interpolation = InterpolateString.GetInterpolationFromString(rigidbody.interpolate);
            result.collisionDetectionMode =
                CollisionDetectionModeString.GetDetectionModeFromString(rigidbody.collisionDetection);
            result.constraints = GetConstraints(rigidbody);
            return result;
        }

        public static glTF_VCAST_vci_Rigidbody GetglTfRigidbodyFromUnityRigidbody(Rigidbody rigidbody)
        {
            var result = new glTF_VCAST_vci_Rigidbody();
            result.mass = rigidbody.mass;
            result.drag = rigidbody.drag;
            result.angularDrag = rigidbody.angularDrag;
            result.useGravity = rigidbody.useGravity;
            result.isKinematic = rigidbody.isKinematic;
            result.interpolate = InterpolateString.GetStringFromInterpolation(rigidbody.interpolation);
            result.collisionDetection =
                CollisionDetectionModeString.GetStringFromCollisionDetectionMode(rigidbody.collisionDetectionMode);
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
                (rigidbody.freezePositionX ? RigidbodyConstraints.FreezePositionX : 0) |
                (rigidbody.freezePositionY ? RigidbodyConstraints.FreezePositionY : 0) |
                (rigidbody.freezePositionZ ? RigidbodyConstraints.FreezePositionZ : 0) |
                (rigidbody.freezeRotationX ? RigidbodyConstraints.FreezeRotationX : 0) |
                (rigidbody.freezeRotationY ? RigidbodyConstraints.FreezeRotationY : 0) |
                (rigidbody.freezeRotationZ ? RigidbodyConstraints.FreezeRotationZ : 0)
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
        [JsonSchema(MinItems = 1)] public List<glTF_VCAST_vci_joint> joints;
    }

    /// <summary>
    /// Rigidbody info
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_joint
    {
        public static class JointString
        {
            public const string Fixed = "fixed";
            public const string Hinge = "hinge";
            public const string Spring = "spring";
        }


        [JsonSchema(Required = true, EnumValues = new object[]
        {
            JointString.Fixed,
            JointString.Hinge,
            JointString.Spring
        }, EnumSerializationType = EnumSerializationType.AsUpperString)]
        public string type;

        public int nodeIndex = -1;

        [JsonSchema(MinItems = 3, MaxItems = 3)]
        public float[] anchor;

        [JsonSchema(MinItems = 3, MaxItems = 3)]
        public float[] axis;

        public bool autoConfigureConnectedAnchor = true;

        [JsonSchema(MinItems = 3, MaxItems = 3)]
        public float[] connectedAnchor;

        public bool enableCollision;

        public bool enablePreprocessing;

        public float massScale = 1.0f;

        public float connectedMassScale = 1.0f;

        // JointSpring
        [Serializable]
        public class Spring
        {
            public float spring;
            public float damper;
            public float minDistance;
            public float maxDistance;
            public float tolerance;
            public float targetPosition;
        }

        public bool useSpring;
        public Spring spring = new Spring();


        // LimitSpring
        [Serializable]
        public class Limits
        {
            public float min;
            public float max;
            public float bounciness;
            public float bounceMinVelocity = 0.2f;
            public float contactDistance;
        }

        public bool useLimits;
        public Limits limits = new Limits();


        public static Joint AddJointComponent(GameObject go, glTF_VCAST_vci_joint joint, List<Transform> nodes)
        {
            var result = GetJoint(go, joint);
            if (joint.nodeIndex != -1)
            {
                var rigidbody = nodes[joint.nodeIndex].GetComponent<Rigidbody>();
                if (rigidbody == null)
                {
                    Debug.LogWarning("AddJointComponent connect RigidBody is not found.");
                    return null;
                }

                result.connectedBody = rigidbody;
            }

            // 共通パラメータ
            result.anchor = new Vector3(joint.anchor[0], joint.anchor[1], joint.anchor[2]).ReverseZ();
            result.axis = new Vector3(joint.axis[0], joint.axis[1], joint.axis[2]).ReverseZ();
            result.autoConfigureConnectedAnchor = joint.autoConfigureConnectedAnchor;
            result.connectedAnchor =
                new Vector3(joint.connectedAnchor[0], joint.connectedAnchor[1], joint.connectedAnchor[2]).ReverseZ();
            result.enableCollision = joint.enableCollision;
            result.enablePreprocessing = joint.enablePreprocessing;
            result.massScale = joint.massScale;
            result.connectedMassScale = joint.connectedMassScale;


            // 個別パラメータ
            if (result.GetType() == typeof(HingeJoint))
            {
                var hinge = result as HingeJoint;

                // spring
                hinge.useSpring = joint.useSpring;
                hinge.spring = new JointSpring()
                {
                    spring = joint.spring.spring,
                    damper = joint.spring.damper,
                    targetPosition = joint.spring.targetPosition
                };

                // limits
                hinge.useLimits = joint.useLimits;
                hinge.limits = new JointLimits()
                {
                    min = joint.limits.min,
                    max = joint.limits.max,
                    bounciness = joint.limits.bounciness,
                    bounceMinVelocity = joint.limits.bounceMinVelocity,
                    contactDistance = joint.limits.contactDistance
                };
            }
            else if (result.GetType() == typeof(SpringJoint))
            {
                var spring = result as SpringJoint;
                spring.spring = joint.spring.spring;
                spring.damper = joint.spring.damper;
                spring.minDistance = joint.spring.minDistance;
                spring.maxDistance = joint.spring.maxDistance;
                spring.tolerance = joint.spring.tolerance;
            }

            return result;
        }

        public static glTF_VCAST_vci_joint GetglTFJointFromUnityJoint(Joint joint, List<Transform> nodes)
        {
            var result = new glTF_VCAST_vci_joint();
            result.nodeIndex = nodes.FindIndex(x => x == joint.connectedBody.gameObject.transform);
            result.anchor = joint.anchor.ReverseZ().ToArray();
            result.axis = joint.axis.ReverseZ().ToArray();
            result.autoConfigureConnectedAnchor = joint.autoConfigureConnectedAnchor;
            result.connectedAnchor = joint.connectedAnchor.ReverseZ().ToArray();
            result.enableCollision = joint.enableCollision;
            result.enablePreprocessing = joint.enablePreprocessing;
            result.massScale = joint.massScale;
            result.connectedMassScale = joint.connectedMassScale;


            if (joint.GetType() == typeof(FixedJoint))
            {
                result.type = JointString.Fixed;
            }
            else if (joint.GetType() == typeof(HingeJoint))
            {
                var hinge = joint as HingeJoint;
                result.type = JointString.Hinge;

                // spring
                result.useSpring = hinge.useSpring;
                result.spring = new Spring()
                {
                    spring = hinge.spring.spring,
                    damper = hinge.spring.damper,
                    targetPosition = hinge.spring.targetPosition
                };

                // limits
                result.useLimits = hinge.useLimits;
                result.limits = new Limits()
                {
                    min = hinge.limits.min,
                    max = hinge.limits.max,
                    bounciness = hinge.limits.bounciness,
                    bounceMinVelocity = hinge.limits.bounceMinVelocity,
                    contactDistance = hinge.limits.contactDistance
                };
            }
            else if (joint.GetType() == typeof(SpringJoint))
            {
                var spring = joint as SpringJoint;
                result.type = JointString.Spring;
                result.spring = new Spring()
                {
                    spring = spring.spring,
                    damper = spring.damper,
                    minDistance = spring.minDistance,
                    maxDistance = spring.maxDistance,
                    tolerance = spring.tolerance,
                };
            }


            return result;
        }

        private static Joint GetJoint(GameObject go, glTF_VCAST_vci_joint joint)
        {
            if (string.IsNullOrEmpty(joint.type)) return go.AddComponent<HingeJoint>();

            switch (joint.type.ToLower())
            {
                case JointString.Fixed:
                    return go.AddComponent<FixedJoint>();
                case JointString.Hinge:
                    return go.AddComponent<HingeJoint>();
                case JointString.Spring:
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
        public bool IsGrabbable => grabbable;

        public bool scalable;
        public bool IsScalable => scalable;

        public bool uniformScaling;
        public bool IsUniformScaling => uniformScaling;

        public int groupId;
        public int GroupId => groupId;
    }

    #endregion

    #region Effekseer
    /// <summary>
    /// gltf.extension
    /// </summary>
    public partial class glTF_extensions : ExtensionsBase<glTF_extensions>
    {
        public glTF_Effekseer Effekseer;
    }

    /// <summary>
    /// Extension root.
    /// </summary>
    [Serializable]
    public class glTF_Effekseer
    {
        public static string ExtensionName => "Effekseer";

        [JsonSchema(Required = true, MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTF_Effekseer_effect> effects;
    }

    /// <summary>
    /// effect body
    /// </summary>
    [Serializable]
    public class glTF_Effekseer_effect
    {
        public int nodeIndex;

        public string nodeName;

        public string effectName;

        public glTF_Effekseer_body body;

        public float scale = 1.0f;

        [JsonSchema(Required = true, MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTF_Effekseer_image> images;

        [JsonSchema(Required = true, MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTF_Effekseer_model> models;
    }

    [Serializable]
    public class glTF_Effekseer_body
    {
        [JsonSchema(Required = true, Minimum = 0)]
        public int bufferView = -1;
    }

    [Serializable]
    public class glTF_Effekseer_image
    {
        public static class MimeTypeString
        {
            public const string Png = "image/png";
            public const string Dds = "image/dds";
        }

        [JsonSchema(Required = true, Minimum = 0)]
        public int bufferView = -1;

        [JsonSchema(EnumValues = new object[] { MimeTypeString.Png, MimeTypeString.Dds }, EnumSerializationType = EnumSerializationType.AsString)]
        public string mimeType;
    }

    [Serializable]
    public class glTF_Effekseer_model
    {
        [JsonSchema(Required = true, Minimum = 0)]
        public int bufferView = -1;
    }

    /// <summary>
    /// node.extension
    /// </summary>
    public partial class glTFNode_extensions : ExtensionsBase<glTFNode_extensions>
    {
        public glTF_Effekseer_emitters Effekseer_emitters;
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_Effekseer_emitters
    {
        public static string ExtensionName => "Effekseer_emitters";

        [JsonSchema(MinItems = 1)]
        public List<glTF_Effekseer_emitter> emitters;
    }

    [Serializable]
    public class glTF_Effekseer_emitter
    {
        [JsonSchema(Required = true, Minimum = 0)]
        public int effectIndex = -1;
        public bool isPlayOnStart;
        public bool isLoop;
    }
    #endregion

    #region Animation

    /// <summary>
    /// node.extension
    /// </summary>
    public partial class glTFNode_extensions : ExtensionsBase<glTFNode_extensions>
    {
        public glTF_VCAST_vci_animation VCAST_vci_animation;
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_animation
    {
        public static string ExtensionName => "VCAST_vci_animation";

        [JsonSchema(MinItems = 1)]
        public List<glTF_VCAST_vci_animationReference> animationReferences;
    }

    [Serializable]
    public class glTF_VCAST_vci_animationReference
    {
        [JsonSchema(Required = true, Minimum = 0)]
        public int animation = -1;
    }
    #endregion

    #region SpringBone

    public partial class glTF_extensions
    {
        public glTF_VCAST_vci_spring_bone VCAST_vci_spring_bone;
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_spring_bone
    {
        public static string ExtensionName => "VCAST_vci_spring_bone";

        [JsonSchema(Required = true, MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTF_VCAST_vci_SpringBone> springBones;
    }

    #endregion

    #region PlayerSpawnPoint

    public partial class glTFNode_extensions
    {
        public glTF_VCAST_vci_player_spawn_point VCAST_vci_player_spawn_point;
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_player_spawn_point
    {
        public static string ExtensionName => "VCAST_vci_player_spawn_point";

        public glTF_VCAST_vci_PlayerSpawnPoint playerSpawnPoint;
    }

    public partial class glTFNode_extensions
    {
        public glTF_VCAST_vci_player_spawn_point_restriction VCAST_vci_player_spawn_point_restriction;
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_player_spawn_point_restriction
    {
        public static string ExtensionName => "VCAST_vci_player_spawn_point_restriction";

        public glTF_VCAST_vci_PlayerSpawnPointRestriction playerSpawnPointRestriction;
    }

    #endregion

    #region LocationBounds

    public partial class glTF_extensions
    {
        public glTF_VCAST_vci_location_bounds VCAST_vci_location_bounds;
    }

    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_location_bounds
    {
        public static string ExtensionName => "VCAST_vci_location_bounds";

        public glTF_VCAST_vci_LocationBounds LocationBounds;
    }

    #endregion
}
