using System;
using TMPro;
using UniGLTF;
using VRMShaders;

namespace VCI
{
    [Serializable, JsonSchema(Title = "vci.text")]
    public sealed class TextJsonObject
    {
        public string fontName;

        public string text;

        public bool richText;

        public float fontSize;

        public bool autoSize;

        public int fontStyle;

        [UniGLTF.JsonSchema(MinItems = 4, MaxItems = 4)]
        public float[] color;

        public bool enableVertexGradient;

        [UniGLTF.JsonSchema(MinItems = 4, MaxItems = 4)]
        public float[] topLeftColor;

        [UniGLTF.JsonSchema(MinItems = 4, MaxItems = 4)]
        public float[] topRightColor;

        [UniGLTF.JsonSchema(MinItems = 4, MaxItems = 4)]
        public float[] bottomLeftColor;

        [UniGLTF.JsonSchema(MinItems = 4, MaxItems = 4)]
        public float[] bottomRightColor;

        public float characterSpacing;

        public float wordSpacing;

        public float lineSpacing;

        public float paragraphSpacing;

        public int alignment;

        /// <summary>Obsolete。シリアライズしない。</summary>
        [JsonSchema(SerializationConditions = new [] {"JsonSchemaUtil.False"})]
        public bool enableWordWrapping;

        public int textWrappingMode;

        public int overflowMode;

        /// <summary>Obsolete。シリアライズしない。</summary>
        [JsonSchema(SerializationConditions = new [] {"JsonSchemaUtil.False"})]
        public bool enableKerning;

        public uint[] fontFeatures;

        public bool extraPadding;

        [UniGLTF.JsonSchema(MinItems = 4, MaxItems = 4)]
        public float[] margin;
    }
}