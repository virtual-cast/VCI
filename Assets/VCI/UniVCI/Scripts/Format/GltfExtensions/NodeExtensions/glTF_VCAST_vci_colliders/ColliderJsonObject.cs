using System;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// Collider info
    /// </summary>
    [Serializable]
    public sealed class ColliderJsonObject
    {
        public const string BoxColliderName = "box";
        public const string SphereColliderName = "sphere";
        public const string CapsuleColliderName = "capsule";

        public const string DefaultColliderLayerName = "default";
        public const string LocationColliderLayerName = "location";
        public const string PickUpColliderLayerName = "pickup";
        public const string AccessoryColliderLayerName = "accessory";
        public const string ItemColliderLayerName = "item";

        public string type;
        public string layer;

        [UniGLTF.JsonSchema(MinItems = 3, MaxItems = 3)]
        public float[] center;

        [UniGLTF.JsonSchema(MinItems = 1, MaxItems = 3)]
        public float[] shape;

        public bool isTrigger;
        public PhysicMaterialJsonObject physicMaterial;
    }
}
