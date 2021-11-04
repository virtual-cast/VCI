using System;
using UnityEngine;

namespace VCI
{
    [Serializable]
    [UniGLTF.JsonSchema(Title = "vci.location_bounds")]
    public class LocationBoundsJsonObject
    {
        public Vector3 bounds_center;
        public Vector3 bounds_size;
    }
}
