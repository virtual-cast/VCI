using System;

namespace VCI
{
    [Serializable]
    public sealed class glTF_VCAST_materials_pbr
    {
        public static string ExtensionName
        {
            get
            {
                return "VCAST_materials_pbr";
            }
        }

        public float[] emissiveFactor;
    }
}
