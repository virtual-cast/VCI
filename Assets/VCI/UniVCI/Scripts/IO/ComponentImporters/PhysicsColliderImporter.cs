using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniGLTF;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    internal static class PhysicsColliderImporter
    {
        // NOTE: コライダが増えてくるとかなり重いので、40 個に 1 度待てば 10ms くらいにはなる
        // FIXME あまり賢い実装ではない
        private const int AwaitIntervalCount = 40;

        /// <summary>
        /// NOTE: ロード時点では、物理演算は無効化されている必要がある.
        /// </summary>
        public static async Task<List<Collider>> LoadAsync(
            VciData vciData,
            IReadOnlyList<Transform> unityNodes,
            IVciColliderLayerProvider vciColliderLayer,
            IAxisInverter axisInverter,
            PhysicMaterialFactory factory,
            IAwaitCaller awaitCaller)
        {
            if (vciColliderLayer == null)
            {
                throw new ArgumentNullException(nameof(vciColliderLayer));
            }

            var colliderComponents = new List<Collider>();
            var meshImporter = new PhysicsColliderMeshImporter(vciData.GltfData, axisInverter);

            // Colliders
            var colliderCount = 0;
            foreach (var (nodeIdx, colliderExtension) in vciData.CollidersNodes)
            {
                if (colliderExtension.colliders.Count == 0)
                {
                    continue;
                }

                var go = unityNodes[nodeIdx].gameObject;

                foreach (var jsonCollider in colliderExtension.colliders)
                {
                    var collider = LoadColliderComponent(go, jsonCollider, meshImporter, factory);
                    if (collider == null)
                    {
                        continue;
                    }
                    colliderComponents.Add(collider);

                    // NOTE: ロード中はコライダーが無効であるべき. ロード終了後に EnablePhysicalBehaviour で有効になる.
                    PhysicalBehaviourChanger.DisableCollider(collider);

                    // NOTE: コライダーにレイヤー情報が存在するなら、それで上書きする.
                    go.layer = LoadLayer(jsonCollider.layer, vciColliderLayer);
                }

                colliderCount += 1;
                if (colliderCount % AwaitIntervalCount == 0)
                {
                    await awaitCaller.NextFrame();
                }
            }

            await awaitCaller.NextFrame();

            return colliderComponents;
        }

        private static Collider LoadColliderComponent(GameObject go, ColliderJsonObject jsonCollider, PhysicsColliderMeshImporter meshImporter, PhysicMaterialFactory factory)
        {
            switch (jsonCollider.type)
            {
                case ColliderJsonObject.BoxColliderName:
                    return LoadBoxCollider(go, jsonCollider, factory);
                case ColliderJsonObject.CapsuleColliderName:
                    return LoadCapsuleCollider(go, jsonCollider, factory);
                case ColliderJsonObject.SphereColliderName:
                    return LoadSphereCollider(go, jsonCollider, factory);
                case ColliderJsonObject.MeshColliderName:
                    return LoadMeshCollider(go, jsonCollider, meshImporter, factory);
                default: // NOTE: 未定義の場合、なにもロードしない.
                    return null;
            }
        }

        private static BoxCollider LoadBoxCollider(GameObject go, ColliderJsonObject jsonCollider, PhysicMaterialFactory factory)
        {
            var collider = go.AddComponent<BoxCollider>();
            collider.center = new Vector3(jsonCollider.center[0], jsonCollider.center[1], jsonCollider.center[2]).ReverseZ();
            collider.size = new Vector3(jsonCollider.shape[0], jsonCollider.shape[1], jsonCollider.shape[2]);
            collider.isTrigger = jsonCollider.isTrigger;
            collider.sharedMaterial = LoadPhysicMaterial(jsonCollider.physicMaterial, factory);
            return collider;
        }

        private static CapsuleCollider LoadCapsuleCollider(GameObject go, ColliderJsonObject jsonCollider, PhysicMaterialFactory factory)
        {
            var collider = go.AddComponent<CapsuleCollider>();
            collider.center = new Vector3(jsonCollider.center[0], jsonCollider.center[1], jsonCollider.center[2]).ReverseZ();
            collider.radius = jsonCollider.shape[0];
            collider.height = jsonCollider.shape[1];
            collider.direction = (int) jsonCollider.shape[2];
            collider.isTrigger = jsonCollider.isTrigger;
            collider.sharedMaterial = LoadPhysicMaterial(jsonCollider.physicMaterial, factory);
            return collider;
        }

        private static SphereCollider LoadSphereCollider(GameObject go, ColliderJsonObject jsonCollider, PhysicMaterialFactory factory)
        {
            var collider = go.AddComponent<SphereCollider>();
            collider.center = new Vector3(jsonCollider.center[0], jsonCollider.center[1], jsonCollider.center[2]).ReverseZ();
            collider.radius = jsonCollider.shape[0];
            collider.isTrigger = jsonCollider.isTrigger;
            collider.sharedMaterial = LoadPhysicMaterial(jsonCollider.physicMaterial, factory);
            return collider;
        }

        private static MeshCollider LoadMeshCollider(GameObject go, ColliderJsonObject jsonCollider, PhysicsColliderMeshImporter meshImporter, PhysicMaterialFactory factory)
        {
            if (jsonCollider.mesh == null) return null;

            var collider = go.AddComponent<MeshCollider>();
            collider.convex = jsonCollider.mesh.isConvex;
            collider.isTrigger = jsonCollider.isTrigger;
            collider.sharedMesh = meshImporter.Load(jsonCollider.mesh);
            collider.sharedMaterial = LoadPhysicMaterial(jsonCollider.physicMaterial, factory);
            return collider;
        }

        private static PhysicMaterial LoadPhysicMaterial(PhysicMaterialJsonObject jsonMaterial, PhysicMaterialFactory factory)
        {
            if (jsonMaterial == null) return null;

            return factory.LoadPhysicMaterial(
                jsonMaterial.dynamicFriction,
                jsonMaterial.staticFriction,
                jsonMaterial.bounciness,
                LoadPhysicMaterialCombine(jsonMaterial.frictionCombine),
                LoadPhysicMaterialCombine(jsonMaterial.bounceCombine));
        }

        private static PhysicMaterialCombine LoadPhysicMaterialCombine(string jsonString)
        {
            switch (jsonString)
            {
                case PhysicMaterialJsonObject.AverageCombineString:
                    return PhysicMaterialCombine.Average;
                case PhysicMaterialJsonObject.MinimumCombineString:
                    return PhysicMaterialCombine.Minimum;
                case PhysicMaterialJsonObject.MaximumCombineString:
                    return PhysicMaterialCombine.Maximum;
                case PhysicMaterialJsonObject.MultiplyCombineString:
                    return PhysicMaterialCombine.Multiply;
                case "":
                    return PhysicMaterialCombine.Average;
                case null:
                    return PhysicMaterialCombine.Average;
                default: // NOTE: Import においては、不明値は default 値にフォールバックさせる
                    Debug.LogWarning($"Unexpected PhysicMaterialCombine: {jsonString}");
                    return PhysicMaterialCombine.Average;
            }
        }

        private static int LoadLayer(string jsonString, IVciColliderLayerProvider layerProvider)
        {
            switch (jsonString)
            {
                case ColliderJsonObject.DefaultColliderLayerName:
                    return layerProvider.Default;
                case ColliderJsonObject.LocationColliderLayerName:
                    return layerProvider.Location;
                case ColliderJsonObject.PickUpColliderLayerName:
                    return layerProvider.PickUp;
                case ColliderJsonObject.AccessoryColliderLayerName:
                    return layerProvider.Accessory;
                case ColliderJsonObject.ItemColliderLayerName:
                    return layerProvider.Item;
                case "":
                    return layerProvider.Default;
                case null:
                    return layerProvider.Default;
                default: // NOTE: Import においては、不明値は default 値にフォールバックさせる
                    Debug.LogWarning($"Unexpected VCI Collision Layer: {jsonString}");
                    return layerProvider.Default;
            }
        }
    }
}
