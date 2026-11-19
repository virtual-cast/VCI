using System;
using System.Collections.Generic;

namespace VCI
{
    /// <summary>
    /// LODGroup info
    /// </summary>
    [Serializable]
    [UniGLTF.JsonSchema(Title = "vci.lod_group.lod_group")]
    public sealed class LodGroupJsonObject
    {
        public const int MaxLodCount = 8;

        [UniGLTF.JsonSchema(Description = "The local space size of the LOD group (Object Size in the Inspector).")]
        public float size = 1.0f;

        [UniGLTF.JsonSchema(MinItems = 3, MaxItems = 3, Description = "The local space reference point of the LOD group.")]
        public float[] localReferencePoint;

        [UniGLTF.JsonSchema(MinItems = 1, MaxItems = MaxLodCount, Description = "The LOD levels, ordered from the most detailed one.")]
        public List<LodJsonObject> lods;
    }
}
