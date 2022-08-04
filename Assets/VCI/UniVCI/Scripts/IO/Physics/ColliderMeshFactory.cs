using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using VRMShaders;

namespace VCI
{
    internal sealed class ColliderMeshFactory : IResponsibilityForDestroyObjects
    {
        private readonly IReadOnlyDictionary<SubAssetKey, Mesh> _externalMeshes;
        private readonly Dictionary<SubAssetKey, Mesh> _runtimeGeneratedMeshes = new Dictionary<SubAssetKey, Mesh>();

        public ColliderMeshFactory(IReadOnlyDictionary<SubAssetKey, Mesh> externalMeshes)
        {
            _externalMeshes = externalMeshes;
        }

        public void Dispose()
        {
            foreach (var kv in _runtimeGeneratedMeshes)
            {
                UnityObjectDestoyer.DestroyRuntimeOrEditor(kv.Value);
            }
            _runtimeGeneratedMeshes.Clear();
        }

        public void TransferOwnership(TakeResponsibilityForDestroyObjectFunc take)
        {
            foreach (var kv in _runtimeGeneratedMeshes.ToArray())
            {
                take(kv.Key, kv.Value);
                _runtimeGeneratedMeshes.Remove(kv.Key);
            }
        }

        public Mesh LoadColliderMesh(ColliderMeshIdentifier id, NativeArray<Vector3> positions, int[] indices)
        {
            if (TryGetLoadedColliderMesh(id, out var loadedMesh))
            {
                return loadedMesh;
            }

            var key = GenerateKey(id);

            var mesh = new Mesh();
            mesh.name = key.Name;
            if (positions.Length > UInt16.MaxValue)
            {
                mesh.indexFormat = IndexFormat.UInt32;
            }
            mesh.SetVertices(positions);
            mesh.SetTriangles(indices, 0);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            _runtimeGeneratedMeshes.Add(key, mesh);

            return mesh;
        }

        public bool TryGetLoadedColliderMesh(ColliderMeshIdentifier id, out Mesh mesh)
        {
            var key = GenerateKey(id);

            if (_externalMeshes.ContainsKey(key))
            {
                mesh = _externalMeshes[key];
                return true;
            }
            if (_runtimeGeneratedMeshes.ContainsKey(key))
            {
                mesh = _runtimeGeneratedMeshes[key];
                return true;
            }

            mesh = default;
            return false;
        }

        private static SubAssetKey GenerateKey(ColliderMeshIdentifier id)
        {
            return new SubAssetKey(typeof(Mesh), GenerateUniqueMeshName(id));
        }

        private static string GenerateUniqueMeshName(ColliderMeshIdentifier id)
        {
            return $"__VCI__COLLIDER__{id.PositionAccessorIndex:D}_{id.IndicesAccessorIndex:D}";
        }
    }
}
