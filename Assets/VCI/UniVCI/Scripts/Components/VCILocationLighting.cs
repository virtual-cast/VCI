using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace VCI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(VCIObject))]
    public sealed class VCILocationLighting : MonoBehaviour
    {
        public LightmapData[] LightmapDataArray = new LightmapData[0];
        public LightmapsMode LightmapMode = LightmapsMode.NonDirectional;
        public Material Skybox;
        public Vector3[] LightProbePositions = new Vector3[0];
        public SphericalHarmonicsL2[] LightProbeCoefficients = new SphericalHarmonicsL2[0];

        private void OnDestroy()
        {
            Destroy(Skybox);
        }
    }
}
