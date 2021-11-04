using System.Collections.Generic;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// Joint を Export できる
    /// </summary>
    public static class PhysicsJointExporter
    {
        public static glTF_VCAST_vci_joints ExportJoints(Transform node, List<Transform> nodes)
        {
            var joints = node.GetComponents<Joint>();
            if (joints.Length == 0)
            {
                return null;
            }

            var jointsExtension = new glTF_VCAST_vci_joints();
            jointsExtension.joints = new List<JointJsonObject>();

            foreach (var joint in joints)
            {
                jointsExtension.joints.Add(JointJsonObject.GetglTFJointFromUnityJoint(joint, nodes));
            }

            return jointsExtension;
        }
    }
}
