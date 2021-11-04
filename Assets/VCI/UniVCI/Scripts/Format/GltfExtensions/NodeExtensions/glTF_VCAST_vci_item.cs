using System;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_item
    {
        public static string ExtensionName => "VCAST_vci_item";

        public bool grabbable;
        public bool IsGrabbable => grabbable;

        public bool scalable;
        public bool IsScalable => scalable;

        public bool uniformScaling;
        public bool IsUniformScaling => uniformScaling;

        public bool attractable;
        public bool IsAttractable => attractable;

        public int groupId;
        public int GroupId => groupId;
    }
}
