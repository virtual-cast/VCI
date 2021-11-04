using System.Collections.Generic;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class SubItemImporter
    {
        public static void Load(VciData vciData, IReadOnlyList<Transform> unityNodes)
        {
            foreach (var (nodeIdx, subItemExtension) in vciData.SubItemNodes)
            {
                var gameObject = unityNodes[nodeIdx].gameObject;
                var item = gameObject.AddComponent<VCISubItem>();
                item.NodeIndex = nodeIdx;
                item.Grabbable = subItemExtension.grabbable;
                item.Scalable = subItemExtension.scalable;
                item.UniformScaling = subItemExtension.uniformScaling;
                // NOTE: UniVCI0.30で追加したフラグ。それ以前のVCIではGrabbable=trueならすべてTrueに
                item.Attractable = subItemExtension.grabbable &&
                                   (vciData.VciMigrationFlags.IsItemAttractableFlagUndefined || subItemExtension.attractable);
                item.GroupId = subItemExtension.groupId;
            }
        }
    }
}