using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// Editorに設定されているレイヤー
    /// </summary>
    public class VciColldierEditorSetting : IVciColliderLayerProvider, IVciDefaultLayerProvider
    {
        private readonly int _default = LayerMask.NameToLayer(VciColliderSetting.VciColliderLayers[VciColliderSetting.VciColliderLayerTypes.Default]);
        private readonly int _location = LayerMask.NameToLayer(VciColliderSetting.VciColliderLayers[VciColliderSetting.VciColliderLayerTypes.Location]);
        private readonly int _pickUp = LayerMask.NameToLayer(VciColliderSetting.VciColliderLayers[VciColliderSetting.VciColliderLayerTypes.PickUp]);
        private readonly int _accessory = LayerMask.NameToLayer(VciColliderSetting.VciColliderLayers[VciColliderSetting.VciColliderLayerTypes.Accessory]);
        private readonly int _item = LayerMask.NameToLayer(VciColliderSetting.VciColliderLayers[VciColliderSetting.VciColliderLayerTypes.Item]);


        public int Default => _default;

        public int Location => _location;

        public int PickUp => _pickUp;

        public int Accessory => _accessory;

        public int Item => _item;
    }

    public static class VciColliderSetting
    {
        /// <summary>
        /// レイヤーのタイプ
        /// </summary>
        public enum VciColliderLayerTypes
        {
            Default,
            Location, // ロケーション
            PickUp, // 持つことができるVCI
            Accessory, //装着VCI
            Item, //VCIのデフォルトレイヤ
        }

        /// <summary>
        /// Unity上のレイヤー名
        /// </summary>
        public static readonly Dictionary<VciColliderLayerTypes, string> VciColliderLayers = new Dictionary<VciColliderLayerTypes, string>()
        {
            { VciColliderLayerTypes.Default, "Default" },
            { VciColliderLayerTypes.Location, "Location" },
            { VciColliderLayerTypes.PickUp, "VCIPickUp" },
            { VciColliderLayerTypes.Accessory, "VCIAccessory" },
            { VciColliderLayerTypes.Item, "VCIItem" },
        };

        /// <summary>
        /// VCIファイルに書き出す文字列
        /// </summary>
        public static readonly Dictionary<VciColliderLayerTypes, string> VciColliderLayerLabel = new Dictionary<VciColliderLayerTypes, string>()
        {
            { VciColliderLayerTypes.Default, "default" },
            { VciColliderLayerTypes.Location, "location" },
            { VciColliderLayerTypes.PickUp, "pickup" },
            { VciColliderLayerTypes.Accessory, "accessory" },
            { VciColliderLayerTypes.Item, "item" },
        };

        /// <summary>
        /// レイヤー番号がVCI用のものであれば、ファイル書き込み用の文字列を入手する
        /// </summary>
        /// <param name="layerNumber"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static bool TryGetVciLayerLabel(int layerNumber, out string label)
        {
            var name = LayerMask.LayerToName(layerNumber);
            var layer = VciColliderLayers.FirstOrDefault(x => x.Value == name);
            if (!string.IsNullOrEmpty(layer.Value))
            {
                label = VciColliderLayerLabel[layer.Key];
                return true;
            }
            else
            {
                label = "";
                return false;
            }
        }

        public static int GetLayerNumber(VciColliderLayerTypes type, IVciColliderLayerProvider vciColliderLayer)
        {
            if (type == VciColliderLayerTypes.Default)
            {
                return vciColliderLayer.Default;
            }
            else if (type == VciColliderLayerTypes.Location)
            {
                return vciColliderLayer.Location;
            }
            else if (type == VciColliderLayerTypes.Accessory)
            {
                return vciColliderLayer.Accessory;
            }
            else if (type == VciColliderLayerTypes.PickUp)
            {
                return vciColliderLayer.PickUp;
            }
            else if (type == VciColliderLayerTypes.Item)
            {
                return vciColliderLayer.Item;
            }
            else
            {
                return vciColliderLayer.Default;
            }
        }
    }
}
