using System;
using System.Collections.Generic;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// Rigidbody info
    /// </summary>
    [Serializable]
    public class JointJsonObject
    {
        public const string FixedJointTypeString = "fixed";
        public const string HingeJointTypeString = "hinge";
        public const string SpringJointTypeString = "spring";


        [UniGLTF.JsonSchema(Required = true, EnumValues = new object[]
        {
            FixedJointTypeString,
            HingeJointTypeString,
            SpringJointTypeString
        }, EnumSerializationType = UniGLTF.EnumSerializationType.AsUpperString)]
        public string type;

        public int nodeIndex = -1;

        [UniGLTF.JsonSchema(MinItems = 3, MaxItems = 3)]
        public float[] anchor;

        [UniGLTF.JsonSchema(MinItems = 3, MaxItems = 3)]
        public float[] axis;

        public bool autoConfigureConnectedAnchor = true;

        [UniGLTF.JsonSchema(MinItems = 3, MaxItems = 3)]
        public float[] connectedAnchor;

        public bool enableCollision;

        public bool enablePreprocessing;

        public float massScale = 1.0f;

        public float connectedMassScale = 1.0f;

        public bool useSpring;
        public SpringJsonObject spring = new SpringJsonObject();

        public bool useLimits;
        public LimitsJsonObject limits = new LimitsJsonObject();


        public static Joint AddJointComponent(GameObject go, JointJsonObject joint, IReadOnlyList<Transform> nodes)
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

        public static JointJsonObject GetglTFJointFromUnityJoint(Joint joint, List<Transform> nodes)
        {
            var result = new JointJsonObject();
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
                result.type = FixedJointTypeString;
            }
            else if (joint.GetType() == typeof(HingeJoint))
            {
                var hinge = joint as HingeJoint;
                result.type = HingeJointTypeString;

                // spring
                result.useSpring = hinge.useSpring;
                result.spring = new SpringJsonObject()
                {
                    spring = hinge.spring.spring,
                    damper = hinge.spring.damper,
                    targetPosition = hinge.spring.targetPosition
                };

                // limits
                result.useLimits = hinge.useLimits;
                result.limits = new LimitsJsonObject()
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
                result.type = SpringJointTypeString;
                result.spring = new SpringJsonObject()
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

        private static Joint GetJoint(GameObject go, JointJsonObject joint)
        {
            if (string.IsNullOrEmpty(joint.type)) return go.AddComponent<HingeJoint>();

            switch (joint.type.ToLower())
            {
                case FixedJointTypeString:
                    return go.AddComponent<FixedJoint>();
                case HingeJointTypeString:
                    return go.AddComponent<HingeJoint>();
                case SpringJointTypeString:
                    return go.AddComponent<SpringJoint>();
                default:
                    return go.AddComponent<HingeJoint>();
            }
        }
    }
}
