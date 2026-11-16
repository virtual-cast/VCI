using System;
using UniGLTF;

namespace VCI
{
    /// <summary>
    /// LOD level info
    /// </summary>
    [Serializable]
    [JsonSchema(Title = "vci.lod_group.lod")]
    public sealed class LodJsonObject
    {
        [JsonSchema(Description = "This level is used while the screen relative height of the LOD group is greater than or equal to this value (0 to 1). Must be smaller than the value of the previous level.")]
        public float screenRelativeTransitionHeight;

        [JsonSchema(Description = "Specify the node indices of the renderers drawn at this level.")]
        [ItemJsonSchema(Minimum = 0)]
        public int[] renderers = {};
    }
}
