using System;
using UnityEngine.Rendering;

namespace VCI
{
    [Serializable]
    public sealed class LightProbeJsonObject
    {
        public float[] position;
        public float[] sphericalHarmonicsCoefficientsRed;
        public float[] sphericalHarmonicsCoefficientsGreen;
        public float[] sphericalHarmonicsCoefficientsBlue;
    }
}
