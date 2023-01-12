using System;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public sealed class glTF_VCAST_vci_item
    {
        public static string ExtensionName => "VCAST_vci_item";

        public bool grabbable;
        public bool scalable;
        public bool uniformScaling;
        public bool attractable;
        public float attractableDistance;
        public int groupId;
        public int key;
    }
}
