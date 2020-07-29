using System;
using UnityEngine;
using VCIGLTF;
using VCIJSON;

namespace VCI
{
    [Serializable]
    [JsonSchema(Title = "vci.location_bounds")]
    public class glTF_VCAST_vci_LocationBounds : JsonSerializableBase
    {
        public Vector3 bounds_center;
        public Vector3 bounds_size;

        protected override void SerializeMembers(GLTFJsonFormatter f)
        {
            f.KeyValue(() => bounds_center);
            f.KeyValue(() => bounds_size);
        }

        public static glTF_VCAST_vci_LocationBounds Create(VCILocationBounds locationBounds)
        {
            return new glTF_VCAST_vci_LocationBounds
            {
                bounds_center = locationBounds.Bounds.center,
                bounds_size = locationBounds.Bounds.size
            };
        }
    }
}
