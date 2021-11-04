using System;
using TMPro;
using UniGLTF;
using VRMShaders;

namespace VCI
{
    [Serializable, JsonSchema(Title = "vci.text")]
    public class TextJsonObject
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

        public bool enableWordWrapping;

        public int overflowMode;

        public bool enableKerning;

        public bool extraPadding;

        [UniGLTF.JsonSchema(MinItems = 4, MaxItems = 4)]
        public float[] margin;
    }
}