using UniGLTF;
using UnityEngine;

namespace VCI
{
    public sealed class EditorPhysicsColliderImporter
    {
        private readonly VciData _data;
        private readonly VCIImporter _context;
        private readonly UnityPath _prefabFilePath;

        private UnityPath AssetDir => _prefabFilePath.GetAssetFolder(".ColliderMeshes");

        public EditorPhysicsColliderImporter(VciData vciData, VCIImporter context, UnityPath prefabFilePath)
        {
            _data = vciData;
            _context = context;
            _prefabFilePath = prefabFilePath;
        }

        public void ExtractAssetFiles()
        {
            // NOTE: MeshCollider の Mesh オブジェクトを保存する.
            foreach (var node in _context.Nodes)
            {
                var meshCollider = node.gameObject.GetComponent<MeshCollider>();
                if (meshCollider == null || meshCollider.sharedMesh == null) continue;

                AssetDir.EnsureFolder();
                // NOTE: Collider Mesh の名前が VCI 内で一意であることを前提とする.
                var filePath = AssetDir.Child($"{meshCollider.sharedMesh.name}.asset");
                if (filePath.LoadAsset<Mesh>() == null)
                {
                    filePath.CreateAsset(meshCollider.sharedMesh);
                }
            }
        }

        public void SetupAfterEditorDelayCall()
        {
            // NOTE: MeshCollider の Mesh オブジェクトを、アセット下のものに差し替える.
            foreach (var node in _context.Nodes)
            {
                var meshCollider = node.gameObject.GetComponent<MeshCollider>();
                if (meshCollider == null || meshCollider.sharedMesh == null) continue;

                var filePath = AssetDir.Child($"{meshCollider.sharedMesh.name}.asset");
                var mesh = filePath.LoadAsset<Mesh>();
                meshCollider.sharedMesh = mesh;
            }
        }
    }
}
