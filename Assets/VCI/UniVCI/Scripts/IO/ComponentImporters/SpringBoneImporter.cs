using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VCI
{
    public static class SpringBoneImporter
    {
        public static void Load(VciData vciData, IReadOnlyList<Transform> unityNodes, GameObject unityRoot)
        {
            var springBones = vciData?.SpringBone?.springBones;
            if (springBones == null) return;

            foreach (var bone in springBones)
            {
                var sb = unityRoot.AddComponent<VCISpringBone>();
                if (bone.center >= 0) sb.m_center = unityNodes[bone.center];
                sb.m_dragForce = bone.dragForce;
                sb.m_gravityDir = bone.gravityDir;
                sb.m_gravityPower = bone.gravityPower;
                sb.m_hitRadius = bone.hitRadius;
                sb.m_stiffnessForce = bone.stiffiness;
                if (bone.colliderIds != null && bone.colliderIds.Any())
                    sb.m_colliderObjects = bone.colliderIds.Select(id => unityNodes[id]).ToList();
                sb.RootBones = bone.bones.Select(x => unityNodes[x]).ToList();
            }
        }
    }
}