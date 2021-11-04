using System.Linq;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// Attachable (装着) を Export できる
    /// </summary>
    public static class AttachableExporter
    {
        public static glTF_VCAST_vci_attachable ExportAttachable(Transform node)
        {
            var vciAttachable = node.GetComponent<VCIAttachable>();
            if (vciAttachable == null)
            {
                return null;
            }

            if (vciAttachable.AttachableHumanBodyBones == null ||
                vciAttachable.AttachableHumanBodyBones.Length == 0)
            {
                Debug.LogWarning($"VCIAttachable at \"{node.name}\" has no attachable bones.");
                return null;
            }

            return new glTF_VCAST_vci_attachable
            {
                attachableHumanBodyBones = vciAttachable.AttachableHumanBodyBones
                    .Select(x => x.ToString())
                    .ToList(),
                attachableDistance = vciAttachable.AttachableDistance,
                scalable = vciAttachable.Scalable,
                offset = vciAttachable.Offset
            };
        }
    }
}