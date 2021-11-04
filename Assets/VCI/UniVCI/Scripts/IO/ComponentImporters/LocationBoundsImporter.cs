using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class LocationBoundsImporter
    {
        public static void Load(VciData vciData, GameObject unityRoot)
        {
            foreach (var (nodeIdx, locationBoundsExtension) in vciData.LocationBoundsNodes)
            {
                var locationBounds = unityRoot.AddComponent<VCILocationBounds>();
                var values = locationBoundsExtension.LocationBounds;
                locationBounds.Bounds = new Bounds(values.bounds_center, values.bounds_size);
            }
        }
    }
}