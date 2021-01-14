using System;
using UnityEngine.Rendering;

namespace VCI
{
    [Serializable]
    public sealed class glTF_VCAST_vci_LocationLighting_LightProbe
    {
        public float[] position;
        public float[] sphericalHarmonicsCoefficientsRed;
        public float[] sphericalHarmonicsCoefficientsGreen;
        public float[] sphericalHarmonicsCoefficientsBlue;
    }
}
