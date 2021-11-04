using System.Linq;
using UnityEngine;

namespace VCI
{
    public sealed class LightProbeExporter
    {
        public LightProbeJsonObject[] Export()
        {
            var probes = LightmapSettings.lightProbes;
            if (probes == null || probes.count == 0) return new LightProbeJsonObject[0];

            var array = new LightProbeJsonObject[probes.count];
            for (var idx = 0; idx < probes.count; ++idx)
            {
                var pos = probes.positions[idx];
                var val = probes.bakedProbes[idx];

                array[idx] = new LightProbeJsonObject
                {
                    position = new[] { -pos.x, pos.y, pos.z }, // invert X-axis
                    sphericalHarmonicsCoefficientsRed = Enumerable.Range(0, 9).Select(x => val[0, x]).ToArray(),
                    sphericalHarmonicsCoefficientsGreen = Enumerable.Range(0, 9).Select(x => val[1, x]).ToArray(),
                    sphericalHarmonicsCoefficientsBlue = Enumerable.Range(0, 9).Select(x => val[2, x]).ToArray(),
                };
            }
            return array;
        }
    }
}
