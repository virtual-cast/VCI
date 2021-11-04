using System;
using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class AttachableImporter
    {
        public static int Load(VciData data, IReadOnlyList<Transform> unityNodes)
        {
            foreach (var pair in data.AttachableNodes)
            {
                var (nodeIdx, extension) = pair;
                var gameObject = unityNodes[nodeIdx].gameObject;

                var attachable = gameObject.AddComponent<VCIAttachable>();
                attachable.AttachableHumanBodyBones = extension.attachableHumanBodyBones.Select(x =>
                {
                    if (HumanBodyBonesUtil.TryDeserializing(x, out var bone))
                    {
                        return bone;
                    }

                    throw new Exception("unknown AttachableHumanBodyBones: " + x);
                }).ToArray();

                attachable.AttachableDistance = extension.attachableDistance;
                attachable.Scalable = extension.scalable;
                attachable.Offset = extension.offset;
            }
            return data.AttachableNodes.Count;
        }
    }
}