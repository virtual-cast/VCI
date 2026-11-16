using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class LodGroupImporter
    {
        // NOTE: ロケーションかアイテムかにかかわらず復元する.
        public static void Load(VciData vciData, IReadOnlyList<Transform> unityNodes)
        {
            foreach (var (nodeIdx, lodGroupExtension) in vciData.LodGroupNodes)
            {
                var lodGroupJson = lodGroupExtension.lodGroup;
                if (lodGroupJson?.lods == null || lodGroupJson.lods.Count == 0)
                {
                    continue;
                }

                var node = unityNodes[nodeIdx];
                if (!TryCreateLods(lodGroupJson.lods, unityNodes, out var lods))
                {
                    Debug.LogWarning($"LODGroup on {node.name} is not restored because its transitions are invalid.");
                    continue;
                }

                var lodGroup = node.gameObject.AddComponent<LODGroup>();
                lodGroup.fadeMode = LODFadeMode.None;
                lodGroup.SetLODs(lods);
                // NOTE: RecalculateBounds() で求め直すとエディタ上で設定された値と変わるため、保存された値を使う。
                lodGroup.localReferencePoint = ToLocalReferencePoint(lodGroupJson.localReferencePoint);
                lodGroup.size = lodGroupJson.size;
            }
        }

        private static bool TryCreateLods(List<LodJsonObject> lodJsons, IReadOnlyList<Transform> unityNodes, out LOD[] lods)
        {
            var result = new List<LOD>();
            var previousTransition = float.PositiveInfinity;
            foreach (var lodJson in lodJsons.Take(LodGroupJsonObject.MaxLodCount))
            {
                if (lodJson == null)
                {
                    lods = null;
                    return false;
                }

                // NOTE: SetLODs は詳細なレベルほど transition が大きいことを要求する.
                var transition = lodJson.screenRelativeTransitionHeight;
                if (!IsTransitionInRange(transition) || transition >= previousTransition)
                {
                    lods = null;
                    return false;
                }
                previousTransition = transition;

                result.Add(new LOD(transition, GetRenderers(lodJson.renderers, unityNodes)));
            }

            lods = result.ToArray();
            return true;

            static bool IsTransitionInRange(float transition)
            {
                return !float.IsNaN(transition) && transition is >= 0.0f and <= 1.0f;
            }
        }

        private static Renderer[] GetRenderers(int[] nodeIndices, IReadOnlyList<Transform> unityNodes)
        {
            if (nodeIndices == null)
            {
                return new Renderer[0];
            }

            // NOTE: Renderer の型は往復で変わりうるため (MeshRenderer -> SkinnedMeshRenderer など) 基底型で取得する.
            return nodeIndices
                .Where(idx => idx >= 0 && idx < unityNodes.Count)
                .Select(idx => unityNodes[idx].GetComponent<Renderer>())
                .Where(renderer => renderer != null)
                .ToArray();
        }

        private static Vector3 ToLocalReferencePoint(float[] localReferencePoint)
        {
            if (localReferencePoint == null || localReferencePoint.Length < 3)
            {
                return Vector3.zero;
            }

            return new Vector3(localReferencePoint[0], localReferencePoint[1], localReferencePoint[2]).ReverseZ();
        }
    }
}
