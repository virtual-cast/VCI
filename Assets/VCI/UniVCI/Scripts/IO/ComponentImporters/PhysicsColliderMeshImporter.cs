using System;
using UniGLTF;
using Unity.Collections;
using UnityEngine;

namespace VCI
{
    internal sealed class PhysicsColliderMeshImporter
    {
        private readonly GltfData _data;
        private readonly IAxisInverter _axisInverter;
        private readonly ColliderMeshFactory _factory;

        public PhysicsColliderMeshImporter(GltfData data, IAxisInverter axisInverter, ColliderMeshFactory factory)
        {
            _data = data;
            _axisInverter = axisInverter;
            _factory = factory;
        }

        /// <summary>
        /// VCI ファイルのパラメタを glTF から Unity に変換して、Factory に渡す.
        /// 実際の生成は Factory が行う.
        /// </summary>
        public Mesh Load(ColliderMeshJsonObject jsonMesh)
        {
            var positionAccessorIndex = jsonMesh.position;
            var indicesAccessorIndex = jsonMesh.indices;

            if (positionAccessorIndex == -1 || indicesAccessorIndex == -1)
            {
                return null;
            }

            var id = new ColliderMeshIdentifier(positionAccessorIndex, indicesAccessorIndex);
            if (_factory.TryGetLoadedColliderMesh(id, out var loadedMesh))
            {
                return loadedMesh;
            }

            var gltfPositions= _data.GetArrayFromAccessor<Vector3>(positionAccessorIndex);
            var gltfIndicesAccessor = _data.GetIndicesFromAccessorIndex(indicesAccessorIndex);

            using (var positionsBuffer = new NativeArray<Vector3>(gltfPositions.Length, Allocator.Persistent))
            using (var indicesBuffer = new NativeArray<int>(gltfIndicesAccessor.Count, Allocator.Persistent))
            {
                // NOTE: C# の struct の制約により CS1654 が発生するため、その回避
                var unityPositions = positionsBuffer;

                // NOTE: VCI の仕様により Z 軸反転する.
                for (var idx = 0; idx < gltfPositions.Length; ++idx)
                {
                    unityPositions[idx] = _axisInverter.InvertVector3(gltfPositions[idx]);
                }

                SetUnityIndices(gltfIndicesAccessor, indicesBuffer);

                return _factory.LoadColliderMesh(id, unityPositions, indicesBuffer);
            }
        }

        /// <summary>
        /// MeshContext.cs からロジックをコピー.
        /// glTF -> Unity の仕様上、面を反転させながらコピーする.
        /// </summary>
        private static void SetUnityIndices(BufferAccessor accessor, NativeArray<int> array)
        {
            switch (accessor.ComponentType)
            {
                case AccessorValueType.UNSIGNED_BYTE:
                    {
                        var indices = accessor.Bytes;
                        for (var i = 0; i < accessor.Count - 2; i += 3)
                        {
                            array[i + 0] = indices[i + 2];
                            array[i + 1] = indices[i + 1];
                            array[i + 2] = indices[i + 0];
                        }
                    }
                    break;
                case AccessorValueType.UNSIGNED_SHORT:
                    {
                        var indices = accessor.Bytes.Reinterpret<ushort>(1);
                        for (var i = 0; i < accessor.Count - 2; i += 3)
                        {
                            array[i + 0] = indices[i + 2];
                            array[i + 1] = indices[i + 1];
                            array[i + 2] = indices[i + 0];
                        }
                    }
                    break;
                case AccessorValueType.UNSIGNED_INT:
                    {
                        // NOTE: 値が型の範囲外になる可能性があるが、Unity の仕様上仕方ないのでそのままロードする.
                        var indices = accessor.Bytes.Reinterpret<int>(1);
                        for (var i = 0; i < accessor.Count - 2; i += 3)
                        {
                            array[i + 0] = indices[i + 2];
                            array[i + 1] = indices[i + 1];
                            array[i + 2] = indices[i + 0];
                        }
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
