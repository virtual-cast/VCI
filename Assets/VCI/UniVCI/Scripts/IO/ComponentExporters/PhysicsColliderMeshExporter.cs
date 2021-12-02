using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    internal sealed class PhysicsColliderMeshExporter
    {
        private readonly ExportingGltfData _data;
        private readonly IAxisInverter _axisInverter;
        private readonly Dictionary<Mesh, (int, int)> _cache = new Dictionary<Mesh, (int, int)>();

        public PhysicsColliderMeshExporter(ExportingGltfData data, IAxisInverter axisInverter)
        {
            _data = data;
            _axisInverter = axisInverter;
        }

        public (int positionAccessorIndex, int indicesAccessorIndex) Export(Mesh mesh)
        {
            if (_cache.ContainsKey(mesh))
            {
                return _cache[mesh];
            }

            // NOTE: glTF のフォーマットおよび VCI の仕様にあわせて Z 軸反転させる.
            var vertices = mesh.vertices
                .Select(x => _axisInverter.InvertVector3(x))
                .ToArray();
            var indices = new List<uint>();
            for (var subMeshIdx = 0; subMeshIdx < mesh.subMeshCount; ++subMeshIdx)
            {
                var subMeshIndices = mesh.GetTriangles(subMeshIdx);
                for (var idx = 0; idx < subMeshIndices.Length; idx += 3)
                {
                    // NOTE: glTF のフォーマットに合わせて反転させる.
                    indices.Add((uint)subMeshIndices[idx + 2]);
                    indices.Add((uint)subMeshIndices[idx + 1]);
                    indices.Add((uint)subMeshIndices[idx + 0]);
                }
            }

            var positionAccessorIndex = _data.ExtendBufferAndGetAccessorIndex<Vector3>(vertices, glBufferTarget.ARRAY_BUFFER);
            var indicesAccessorIndex = _data.ExtendBufferAndGetAccessorIndex<uint>(indices.ToArray(), glBufferTarget.ELEMENT_ARRAY_BUFFER);
            _cache.Add(mesh, (positionAccessorIndex, indicesAccessorIndex));

            return (positionAccessorIndex, indicesAccessorIndex);
        }
    }
}
