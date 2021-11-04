using System;
using System.Collections.Generic;

namespace VCI
{
    [Serializable]
    [UniGLTF.JsonSchema(Title = "vci.material")]
    public class VciMaterialJsonObject
    {
        public string name;
        public string shader;
        public int renderQueue = -1;

        public Dictionary<string, float> floatProperties = new Dictionary<string, float>();
        public Dictionary<string, float[]> vectorProperties = new Dictionary<string, float[]>();
        public Dictionary<string, int> textureProperties = new Dictionary<string, int>();
        public Dictionary<string, bool> keywordMap = new Dictionary<string, bool>();
        public Dictionary<string, string> tagMap = new Dictionary<string, string>();

        public static readonly string VRM_USE_GLTFSHADER = "VRM_USE_GLTFSHADER";
    }
}