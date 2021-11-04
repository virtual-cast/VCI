using UnityEngine;

namespace VCI
{
    public static class LocationBoundsExporter
    {
        public static glTF_VCAST_vci_location_bounds ExportLocationBounds(GameObject root)
        {
            var locationBounds = root.GetComponent<VCILocationBounds>();
            if (locationBounds == null)
            {
                return null;
            }

            return new glTF_VCAST_vci_location_bounds
            {
                LocationBounds = ExportLocationBoundsComponent(locationBounds),
            };
        }

        private static LocationBoundsJsonObject ExportLocationBoundsComponent(VCILocationBounds locationBounds)
        {
            return new LocationBoundsJsonObject
            {
                bounds_center = locationBounds.Bounds.center,
                bounds_size = locationBounds.Bounds.size
            };
        }
    }
}