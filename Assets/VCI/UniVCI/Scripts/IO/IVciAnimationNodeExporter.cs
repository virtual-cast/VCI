using System.Collections.Generic;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public interface IVciAnimationNodeExporter
    {
        IAnimationExporter GltfRootAnimationExporter { get; }
        List<(glTFNode, glTF_VCAST_vci_animation)> ExportAnimations(ExportingGltfData exportingData, GameObject root, List<Transform> nodes);
    }
}
