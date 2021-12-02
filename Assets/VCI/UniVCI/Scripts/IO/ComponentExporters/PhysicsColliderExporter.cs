using System;
using System.Collections.Generic;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// Collider を Export できる
    /// </summary>
    internal static class PhysicsColliderExporter
    {
        public static glTF_VCAST_vci_colliders ExportColliders(Transform node, ExportingGltfData data, IVciColliderLayerProvider colliderLayerProvider, PhysicsColliderMeshExporter colliderMeshExporter)
        {
            // 各ノードに複数のコライダーがあり得る
            var colliders = node.GetComponents<Collider>();
            if (colliders.Length == 0)
            {
                return null;
            }

            var gltfColliders = new List<ColliderJsonObject>();

            foreach (var collider in colliders)
            {
                var gltfCollider = ExportCollider(collider, colliderLayerProvider, colliderMeshExporter);
                if (gltfCollider == null)
                {
                    Debug.LogWarningFormat("collider is not supported: {0}", collider.GetType().Name);
                    continue;
                }

                gltfColliders.Add(gltfCollider);
            }

            return new glTF_VCAST_vci_colliders
            {
                colliders = gltfColliders,
            };
        }

        private static ColliderJsonObject ExportCollider(Collider collider, IVciColliderLayerProvider colliderLayerProvider, PhysicsColliderMeshExporter colliderMeshExporter)
        {
            switch (collider)
            {
                case BoxCollider boxCollider:
                    return ExportBoxCollider(boxCollider, colliderLayerProvider);
                case CapsuleCollider capsuleCollider:
                    return ExportCapsuleCollider(capsuleCollider, colliderLayerProvider);
                case SphereCollider sphereCollider:
                    return ExportSphereCollider(sphereCollider, colliderLayerProvider);
                case MeshCollider meshCollider:
                    return ExportMeshCollider(meshCollider, colliderLayerProvider, colliderMeshExporter);
                default:
                    return null;
            }
        }

        private static ColliderJsonObject ExportBoxCollider(BoxCollider collider, IVciColliderLayerProvider colliderLayerProvider)
        {
            if (collider == null) return null;

            return new ColliderJsonObject
            {
                type = ColliderJsonObject.BoxColliderName,
                center = collider.center.ReverseZ().ToArray(),
                shape = collider.size.ToArray(),
                mesh = null,
                isTrigger = collider.isTrigger,
                physicMaterial = ExportPhysicMaterial(collider.sharedMaterial),
                layer = ExportLayer(collider.gameObject.layer, colliderLayerProvider),
            };
        }

        private static ColliderJsonObject ExportCapsuleCollider(CapsuleCollider collider, IVciColliderLayerProvider colliderLayerProvider)
        {
            if (collider == null) return null;

            return new ColliderJsonObject
            {
                type = ColliderJsonObject.CapsuleColliderName,
                center = collider.center.ReverseZ().ToArray(),
                shape = new float[3]
                {
                    collider.radius,
                    collider.height,
                    collider.direction // NOTE: 0 = X-Axis, 1 = Y-Axis, 2 = Z-Axis
                },
                mesh = null,
                isTrigger = collider.isTrigger,
                physicMaterial = ExportPhysicMaterial(collider.sharedMaterial),
                layer = ExportLayer(collider.gameObject.layer, colliderLayerProvider),
            };
        }

        private static ColliderJsonObject ExportSphereCollider(SphereCollider collider, IVciColliderLayerProvider colliderLayerProvider)
        {
            if (collider == null) return null;

            return new ColliderJsonObject
            {
                type = ColliderJsonObject.SphereColliderName,
                center = collider.center.ReverseZ().ToArray(),
                shape = new float[1]
                {
                    collider.radius
                },
                mesh = null,
                isTrigger = collider.isTrigger,
                physicMaterial = ExportPhysicMaterial(collider.sharedMaterial),
                layer = ExportLayer(collider.gameObject.layer, colliderLayerProvider),
            };
        }

        private static ColliderJsonObject ExportMeshCollider(MeshCollider collider, IVciColliderLayerProvider colliderLayerProvider, PhysicsColliderMeshExporter colliderMeshExporter)
        {
            if (collider == null || collider.sharedMesh == null) return null;

            return new ColliderJsonObject
            {
                type = ColliderJsonObject.MeshColliderName,
                center = Vector3.zero.ToArray(),
                shape = Array.Empty<float>(),
                mesh = ExportMeshColliderJsonObject(collider, colliderMeshExporter),
                isTrigger = collider.isTrigger,
                physicMaterial = ExportPhysicMaterial(collider.sharedMaterial),
                layer = ExportLayer(collider.gameObject.layer, colliderLayerProvider)
            };
        }

        private static ColliderMeshJsonObject ExportMeshColliderJsonObject(MeshCollider collider, PhysicsColliderMeshExporter colliderMeshExporter)
        {
            if (collider == null || collider.sharedMesh == null) return null;

            var (positionAccessorIndex, indicesAccessorIndices) = colliderMeshExporter.Export(collider.sharedMesh);

            return new ColliderMeshJsonObject
            {
                isConvex = collider.convex,
                position = positionAccessorIndex,
                indices = indicesAccessorIndices,
            };
        }

        private static PhysicMaterialJsonObject ExportPhysicMaterial(PhysicMaterial material)
        {
            if (material == null) return null;

            return new PhysicMaterialJsonObject
            {
                dynamicFriction = material.dynamicFriction,
                staticFriction = material.staticFriction,
                bounciness = material.bounciness,
                frictionCombine = ExportPhysicMaterialCombine(material.frictionCombine),
                bounceCombine = ExportPhysicMaterialCombine(material.bounceCombine)
            };
        }

        private static string ExportPhysicMaterialCombine(PhysicMaterialCombine combine)
        {
            switch (combine)
            {
                case PhysicMaterialCombine.Average:
                    return PhysicMaterialJsonObject.AverageCombineString;
                case PhysicMaterialCombine.Minimum:
                    return PhysicMaterialJsonObject.MinimumCombineString;
                case PhysicMaterialCombine.Maximum:
                    return PhysicMaterialJsonObject.MaximumCombineString;
                case PhysicMaterialCombine.Multiply:
                    return PhysicMaterialJsonObject.MultiplyCombineString;
                default:
                    throw new ArgumentOutOfRangeException(nameof(combine), combine, null);
            }
        }

        private static string ExportLayer(int layerNumber, IVciColliderLayerProvider colliderLayerProvider)
        {
            if (layerNumber == colliderLayerProvider.Default)
            {
                return ColliderJsonObject.DefaultColliderLayerName;
            }
            else if (layerNumber == colliderLayerProvider.Location)
            {
                return ColliderJsonObject.LocationColliderLayerName;
            }
            else if (layerNumber == colliderLayerProvider.PickUp)
            {
                return ColliderJsonObject.PickUpColliderLayerName;
            }
            else if (layerNumber == colliderLayerProvider.Accessory)
            {
                return ColliderJsonObject.AccessoryColliderLayerName;
            }
            else if (layerNumber == colliderLayerProvider.Item)
            {
                return ColliderJsonObject.ItemColliderLayerName;
            }
            else
            {
                // NOTE: Export 時に不明な Layer を指定された場合、警告を出して Default レイヤにフォールバックする.
                Debug.LogWarning($"Fallback the invalid layer {layerNumber}:{LayerMask.LayerToName(layerNumber)} into default layer.");
                return ColliderJsonObject.DefaultColliderLayerName;
            }
        }
    }
}
