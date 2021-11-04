using System;
using System.Collections.Generic;

namespace VCI
{
    /// <summary>
    /// effect body
    /// </summary>
    [Serializable]
    public class EffekseerEffectJsonObject
    {
        public int nodeIndex;

        public string nodeName;

        public string effectName;

        public EffekseerBodyJsonObject body;

        public float scale = 1.0f;

        [UniGLTF.JsonSchema(Required = true, MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<EffekseerImageJsonObject> images;

        [UniGLTF.JsonSchema(Required = true, MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<EffekseerModelJsonObject> models;
    }
}
