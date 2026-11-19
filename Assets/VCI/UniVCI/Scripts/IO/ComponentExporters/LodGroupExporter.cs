using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class LodGroupExporter
    {
        public static glTF_VCAST_vci_lod_group ExportLodGroup(Transform node, List<Transform> nodes, glTF gltf)
        {
            var lodGroup = node.GetComponent<LODGroup>();
            if (!IsAvailable(lodGroup))
            {
                return null;
            }

            var lods = lodGroup.GetLODs();
            if (lods.Length == 0)
            {
                return null;
            }

            return new glTF_VCAST_vci_lod_group
            {
                lodGroup = new LodGroupJsonObject
                {
                    size = lodGroup.size,
                    localReferencePoint = lodGroup.localReferencePoint.ReverseZ().ToArray(),
                    lods = lods
                        .Take(LodGroupJsonObject.MaxLodCount)
                        .Select(lod => new LodJsonObject
                        {
                            screenRelativeTransitionHeight = lod.screenRelativeTransitionHeight,
                            renderers = ExportRenderers(lod.renderers, nodes, gltf),
                        })
                        .ToList(),
                }
            };
        }

        private static bool IsAvailable(LODGroup lodGroup)
        {
            // 無効な LODGroup はレベルの切り替えを行わず全レベルを描画する。出力しないことで見た目を一致させる。
            return lodGroup != null && lodGroup.enabled && lodGroup.gameObject.activeInHierarchy;
        }

        private static int[] ExportRenderers(Renderer[] renderers, List<Transform> nodes, glTF gltf)
        {
            var indices = new List<int>();
            foreach (var renderer in renderers)
            {
                if (GetRendererIndexOrNull(renderer) is { } i)
                {
                    indices.Add(i);
                }
                else
                {
                    // 読み込み側で復元できないものは参照しない。
                    var rendererName = renderer != null ? renderer.name : "(missing)";
                    Debug.LogWarning($"LODGroup refers to a renderer that is not exported: {rendererName}");
                }
            }

            return indices.ToArray();

            int? GetRendererIndexOrNull(Renderer renderer)
            {
                if (renderer == null)
                {
                    return null;
                }

                var index = nodes.IndexOf(renderer.transform);

                // 指定したノードが VCI の階層外であるか、メッシュが出力されなかった場合
                if (index < 0 || gltf.nodes[index].mesh < 0)
                {
                    return null;
                }

                return index;
            }
        }
    }
}
