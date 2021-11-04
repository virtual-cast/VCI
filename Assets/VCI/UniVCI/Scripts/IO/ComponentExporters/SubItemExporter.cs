using UnityEngine;

namespace VCI
{
    /// <summary>
    /// SubItem を Export できる
    /// </summary>
    public static class SubItemExporter
    {
        public static glTF_VCAST_vci_item ExportSubItem(Transform node)
        {
            var item = node.GetComponent<VCISubItem>();
            if (item == null)
            {
                return null;
            }

            var warning = item.ExportWarning;
            if (!string.IsNullOrEmpty(warning)) throw new System.Exception(warning);

            return new glTF_VCAST_vci_item
            {
                grabbable = item.Grabbable,
                scalable = item.Scalable,
                uniformScaling = item.UniformScaling,
                attractable = item.Attractable,
                groupId = item.GroupId,
            };
        }
    }
}