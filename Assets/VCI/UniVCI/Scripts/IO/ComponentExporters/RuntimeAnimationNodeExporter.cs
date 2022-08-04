using System.Collections.Generic;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public sealed class RuntimeAnimationNodeExporter : IVciAnimationNodeExporter
    {
        public IAnimationExporter GltfRootAnimationExporter { get; } = null;

        public List<(glTFNode, glTF_VCAST_vci_animation)> ExportAnimations(ExportingGltfData exportingData, GameObject root, List<Transform> nodes)
        {
            // No support at Runtime
            return new List<(glTFNode, glTF_VCAST_vci_animation)>();
        }
    }
}