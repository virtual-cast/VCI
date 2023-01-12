using UnityEngine;

namespace VCI
{
    public static class LocationBoundsImporter
    {
        public static void Load(VciData vciData, GameObject unityRoot)
        {
            if (vciData.LocationBounds != null)
            {
                var locationBounds = unityRoot.AddComponent<VCILocationBounds>();
                var values = vciData.LocationBounds.LocationBounds;
                // NOTE: Unity 座標系での値になってしまっているので、変換は行わない.
                locationBounds.Bounds = new Bounds(values.bounds_center, values.bounds_size);
            }
        }
    }
}