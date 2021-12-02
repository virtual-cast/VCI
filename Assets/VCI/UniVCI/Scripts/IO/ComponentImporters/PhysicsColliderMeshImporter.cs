using System;
using System.Collections.Generic;
using UniGLTF;
using UnityEngine;
using UnityEngine.Rendering;

namespace VCI
{
    internal sealed class PhysicsColliderMeshImporter
    {
        private readonly GltfData _data;
        private readonly IAxisInverter _axisInverter;
        private readonly Dictionary<PhysicsColliderMeshIdentifier, Mesh> _cache = new Dictionary<PhysicsColliderMeshIdentifier, Mesh>();

        public PhysicsColliderMeshImporter(GltfData data, IAxisInverter axisInverter)
        {
            _data = data;
            _axisInverter = axisInverter;
        }

        public Mesh Load(ColliderMeshJsonObject jsonMesh)
        {
            var positionAccessorIndex = jsonMesh.position;
            var indicesAccessorIndex = jsonMesh.indices;

            if (positionAccessorIndex == -1 || indicesAccessorIndex == -1)
            {
                return null;
            }

            var id = new PhysicsColliderMeshIdentifier(positionAccessorIndex, indicesAccessorIndex);
            if (_cache.ContainsKey(id))
            {
                return _cache[id];
            }

            // NOTE: VCI の仕様により Z 軸反転する.
            var positions= _data.GetArrayFromAccessor<Vector3>(positionAccessorIndex);
            for (var idx = 0; idx < positions.Length; ++idx)
            {
                positions[idx] = _axisInverter.InvertVector3(positions[idx]);
            }

            // NOTE: GltfData.GetIndices は内部で glTF -> Unity 変換を行うため、ここでは無変換.
            var indices = _data.GetIndices(indicesAccessorIndex);

            var mesh = new Mesh();
            mesh.name = GenerateUniqueMeshName(id);
            if (positions.Length > UInt16.MaxValue)
            {
                mesh.indexFormat = IndexFormat.UInt32;
            }
            mesh.vertices = positions;
            mesh.triangles = indices;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            _cache.Add(id, mesh);

            return mesh;
        }

        internal static string GenerateUniqueMeshName(PhysicsColliderMeshIdentifier id)
        {
            return $"__COLLIDER__{id.PositionAccessorIndex:D}_{id.IndicesAccessorIndex:D}";
        }
    }
}
