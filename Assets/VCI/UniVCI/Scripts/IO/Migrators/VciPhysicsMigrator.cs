using System;
using System.Collections.Generic;
using System.Linq;
using UniGLTF;

namespace VCI
{
    internal static class VciPhysicsMigrator
    {
        public static void Migrate(
            GltfData gltfData,
            List<(int gltfNodeIdx, glTF_VCAST_vci_colliders extension)> collidersNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_rigidbody extension)> rigidbodyNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_item extension)> subItemNodes)
        {
            MigrateSubItemsWithoutRigidbody(rigidbodyNodes, subItemNodes);
            MigrateNonConvexMeshCollidersInRigidbody(gltfData, collidersNodes, rigidbodyNodes);
        }

        /// <summary>
        /// RigidbodyがないSubItemにRigidbodyを追加する。
        /// Rigidbodyを外してエクスポートできてしまった時期があるため必要。
        /// </summary>
        private static void MigrateSubItemsWithoutRigidbody(
            List<(int gltfNodeIdx, glTF_VCAST_vci_rigidbody extension)> rigidbodyNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_item extension)> subItemNodes)
        {
            var rigidbodyNodeIndices = new HashSet<int>(rigidbodyNodes.Select(x => x.gltfNodeIdx));

            foreach (var (nodeIdx, _) in subItemNodes)
            {
                if (rigidbodyNodeIndices.Contains(nodeIdx)) { continue; }

                var rigidbodyExtension = new glTF_VCAST_vci_rigidbody
                {
                    // IsKinematic = true, UseGravity = falseとして、他はデフォルト値のRigidbodyを追加
                    rigidbodies = new List<RigidbodyJsonObject> { new()
                    {
                        useGravity = false,
                        isKinematic = true,
                    }}
                };

                rigidbodyNodes.Add((nodeIdx, rigidbodyExtension));
                rigidbodyNodeIndices.Add(nodeIdx);
            }
        }

        /// <summary>
        /// Rigidbody以下のMeshColliderをConvexにする。
        /// SubItemのRigidbodyを外すことでバリデーションをすり抜けられてしまった時期があるため必要。
        /// </summary>
        private static void MigrateNonConvexMeshCollidersInRigidbody(
            GltfData gltfData,
            List<(int gltfNodeIdx, glTF_VCAST_vci_colliders extension)> collidersNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_rigidbody extension)> rigidbodyNodes)
        {
            // 子ノードから親ノードを参照できるようにする
            var parentNodeIndices = new int[gltfData.GLTF.nodes.Count].AsSpan();
            parentNodeIndices.Fill(-1);
            for (var nodeIdx = 0; nodeIdx < gltfData.GLTF.nodes.Count; nodeIdx++)
            {
                if (gltfData.GLTF.nodes[nodeIdx].children is null) { continue; }

                foreach (var childIdx in gltfData.GLTF.nodes[nodeIdx].children)
                {
                    if (0 <= childIdx && childIdx < parentNodeIndices.Length)
                    {
                        parentNodeIndices[childIdx] = nodeIdx;
                    }
                }
            }

            // Colliders拡張の値を修正
            var rigidbodyNodeIndices = new HashSet<int>(rigidbodyNodes.Select(x => x.gltfNodeIdx));
            for (var listIdx = 0; listIdx < collidersNodes.Count; listIdx++)
            {
                var colliderExtension = collidersNodes[listIdx].extension;
                var updated = false;
                foreach (var collider in colliderExtension.colliders)
                {
                    if (collider.type != ColliderJsonObject.MeshColliderName ||
                        collider.mesh is null ||
                        collider.mesh.isConvex) { continue; }

                    // 祖先のRigidbodyを探索
                    var currentNodeIdx = collidersNodes[listIdx].gltfNodeIdx;
                    while (currentNodeIdx >= 0)
                    {
                        if (rigidbodyNodeIndices.Contains(currentNodeIdx))
                        {
                            collider.mesh.isConvex = true;
                            updated = true;
                            break;
                        }
                        currentNodeIdx = parentNodeIndices[currentNodeIdx];
                    }
                }

                if (updated)
                {
                    collidersNodes[listIdx] = (collidersNodes[listIdx].gltfNodeIdx, colliderExtension);
                }
            }
        }
    }
}
