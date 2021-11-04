using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace VCI
{
    public sealed class LightProbeImporter
    {
        private const int CoefCount = 9;

        public (Vector3[], SphericalHarmonicsL2[]) Import(LightProbeJsonObject[] lightProbes)
        {
            var positions = new List<Vector3>();
            var coefficients = new List<SphericalHarmonicsL2>();

            for (var idx = 0; idx < lightProbes.Length; ++idx)
            {
                var lightProbe = lightProbes[idx];
                if (lightProbe == null) continue;

                var pos = lightProbe.position;
                if (pos == null || pos.Length != 3) continue;

                var coefR = lightProbe.sphericalHarmonicsCoefficientsRed;
                var coefG = lightProbe.sphericalHarmonicsCoefficientsGreen;
                var coefB = lightProbe.sphericalHarmonicsCoefficientsBlue;
                if (coefR == null || coefG == null || coefB == null) continue;
                if (coefR.Length != CoefCount || coefG.Length != CoefCount || coefB.Length != CoefCount) continue;

                var unityPos = new Vector3(-pos[0], pos[1], pos[2]); // invert X-axis
                var coef = new SphericalHarmonicsL2();
                for (var lap = 0; lap < 9; ++lap)
                {
                    coef[0, lap] = coefR[lap];
                    coef[1, lap] = coefG[lap];
                    coef[2, lap] = coefB[lap];
                }

                positions.Add(unityPos);
                coefficients.Add(coef);
            }

            return (positions.ToArray(), coefficients.ToArray());
        }
    }
}
