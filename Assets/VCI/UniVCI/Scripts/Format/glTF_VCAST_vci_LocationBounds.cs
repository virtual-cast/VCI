using System;
using UnityEngine;

namespace VCI
{
    [Serializable]
    [UniGLTF.JsonSchema(Title = "vci.location_bounds")]
    public class glTF_VCAST_vci_LocationBounds
    {
        public Vector3 bounds_center;
        public Vector3 bounds_size;

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
