using System;
using UniGLTF;

namespace VCI
{
    [Serializable]
    public sealed class ColliderMeshJsonObject
    {
        public bool isConvex;

        [JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int position = -1;

        [JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int indices = -1;
    }
}
