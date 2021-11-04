using System.Collections.Generic;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class LayerImporter
    {
        public static void Load(
            glTF gltf,
            IReadOnlyList<Transform> unityNodes,
            IVciDefaultLayerProvider vciDefaultLayerProvider,
            bool isLocation)
        {
            for (var i = 0; i < gltf.nodes.Count; i++)
            {
                var node = gltf.nodes[i];
                var go = unityNodes[i].gameObject;

                if (isLocation)
                {
                    go.layer = vciDefaultLayerProvider.Location;
                }
                else
                {
                    go.layer = vciDefaultLayerProvider.Item;
                }
            }
        }
    }
}