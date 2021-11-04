using System;

namespace VCI
{
    /// <summary>
    /// UniVCI v0.32.1 以下で使用されていた拡張.
    /// GLTF の規格上 PBR の emissive は 1.0 を超えて格納できないため、それを格納する拡張.
    ///
    /// Importer で解釈する必要はある.
    /// Exporter で参照する必要はない.
    /// </summary>
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
