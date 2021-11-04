using UnityEngine;

namespace VCI
{
    /// <summary>
    /// VCI が提供する、デフォルトのレイヤー名設定.
    /// </summary>
    public class VciDefaultLayerSettings : IVciColliderLayerProvider, IVciDefaultLayerProvider
    {
        public const string DefaultLayerName = "Default";
        public const string LocationLayerName = "Location";
        public const string PickUpLayerName = "VCIPickUp";
        public const string AccessoryLayerName = "VCIAccessory";
        public const string ItemLayerName = "VCIItem";

        public int Default { get; }
        public int Location { get; }
        public int PickUp { get; }
        public int Accessory { get; }
        public int Item { get; }

        public VciDefaultLayerSettings()
        {
            Default = LayerMask.NameToLayer(DefaultLayerName);
            Location = LayerMask.NameToLayer(LocationLayerName);
            PickUp = LayerMask.NameToLayer(PickUpLayerName);
            Accessory = LayerMask.NameToLayer(AccessoryLayerName);
            Item = LayerMask.NameToLayer(ItemLayerName);
        }
    }
}
