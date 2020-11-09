using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VCIGLTF;

namespace VCI
{
    public enum VciColliderLayerTypes
    {
        Default,
        Location, // ロケーション
        PickUp, // 持つことができるVCI
        Accessory, //装着VCI
        Item, //VCIのデフォルトレイヤ
    }


    [RequireComponent(typeof(Collider))]
    [DisallowMultipleComponent]
    public class VciColliderSetting : MonoBehaviour
    {
        [SerializeField]
        private VciColliderLayerTypes _layerType;

        public VciColliderLayerTypes LayerType => _layerType;

        public void SetLayer(string layerName)
        {
            if(layerName == "default")
            {
                _layerType = VciColliderLayerTypes.Default;
            }
            else if (layerName == "location")
            {
                _layerType = VciColliderLayerTypes.Location;
            }
            else if (layerName == "pickup")
            {
                _layerType = VciColliderLayerTypes.PickUp;
            }
            else if (layerName == "accessory")
            {
                _layerType = VciColliderLayerTypes.Accessory;
            }
            else if (layerName == "item")
            {
                _layerType = VciColliderLayerTypes.Item;
            }
            else
            {
                Debug.LogWarning("Unknown Layer");
            }
        }
    }
}
