using System.Collections.Generic;
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
                // NOTE: UniVCI 0.36 で追加した項目。それ以前の VCI ではデフォルト値に
                item.AttractableDistance = vciData.VciMigrationFlags.IsItemAttractableDistanceUndefined ? VCISubItem.DefaultAttractableDistance : subItemExtension.attractableDistance;
                item.GroupId = subItemExtension.groupId;
                // NOTE: UniVCI 0.37 で追加した項目。それ以前の VCI では NodeIndex を Key とする。
                item.Key = vciData.VciMigrationFlags.IsSubItemKeyUndefined ? nodeIdx : subItemExtension.key;
            }
        }
    }
}
