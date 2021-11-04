using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VCI
{
    public static class SpringBoneExporter
    {
        public static glTF_VCAST_vci_spring_bone ExportSpringBones(GameObject root, List<Transform> nodes)
        {
            var springBones = root.GetComponents<VCISpringBone>();
            if (springBones.Length == 0)
            {
                return null;
            }

            var springBonesExtension = new glTF_VCAST_vci_spring_bone();
            springBonesExtension.springBones = new List<SpringBoneJsonObject>();
            foreach (var sb in springBones)
            {
                springBonesExtension.springBones.Add(new SpringBoneJsonObject()
                {
                    center = nodes.IndexOf(sb.m_center),
                    dragForce = sb.m_dragForce,
                    gravityDir = sb.m_gravityDir,
                    gravityPower = sb.m_gravityPower,
                    stiffiness = sb.m_stiffnessForce,
                    hitRadius = sb.m_hitRadius,
                    colliderIds = sb.m_colliderObjects
                        .Where(x => x != null)
                        .Select(x => nodes.IndexOf(x))
                        .ToArray(),
                    bones = sb.RootBones.Where(x => x != null).Select(x => nodes.IndexOf(x.transform)).ToArray()
                });
            }

            return springBonesExtension;
        }
    }
}