using System;
using TMPro;
using VCIGLTF;
using VCIJSON;

namespace VCI
{
    [Serializable, JsonSchema(Title = "vci.text")]
    public class glTF_VCAST_vci_Text : JsonSerializableBase
    {
        public string fontName;

        public string text;

        public bool richText;

        public float fontSize;

        public bool autoSize;

        public int fontStyle;

        [JsonSchema(MinItems = 4, MaxItems = 4)]
        public float[] color;

        public bool enableVertexGradient;

        [JsonSchema(MinItems = 4, MaxItems = 4)]
        public float[] topLeftColor;

        [JsonSchema(MinItems = 4, MaxItems = 4)]
        public float[] topRightColor;

        [JsonSchema(MinItems = 4, MaxItems = 4)]
        public float[] bottomLeftColor;

        [JsonSchema(MinItems = 4, MaxItems = 4)]
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

        [JsonSchema(MinItems = 4, MaxItems = 4)]
        public float[] margin;

        protected override void SerializeMembers(GLTFJsonFormatter f)
        {
            f.KeyValue(() => text);
        }

        public static glTF_VCAST_vci_Text Create(TextMeshPro tmp)
        {
            return new glTF_VCAST_vci_Text
            {
                fontName = tmp.font.faceInfo.familyName,
                text = tmp.text,
                richText =  tmp.richText,
                autoSize =  tmp.autoSizeTextContainer,
                fontSize = tmp.fontSize,
                fontStyle = (int)tmp.fontStyle,
                color = tmp.color.ToArray(),
                enableVertexGradient = tmp.enableVertexGradient,
                topLeftColor = tmp.colorGradient.topLeft.ToArray(),
                topRightColor = tmp.colorGradient.topRight.ToArray(),
                bottomLeftColor = tmp.colorGradient.bottomLeft.ToArray(),
                bottomRightColor = tmp.colorGradient.bottomRight.ToArray(),
                characterSpacing = tmp.characterSpacing,
                wordSpacing = tmp.wordSpacing,
                lineSpacing = tmp.lineSpacing,
                paragraphSpacing = tmp.paragraphSpacing,
                alignment = (int)tmp.alignment,
                enableWordWrapping = tmp.enableWordWrapping,
                overflowMode = (int)tmp.overflowMode,
                enableKerning = tmp.enableKerning,
                extraPadding = tmp.extraPadding,
                margin = tmp.margin.ToArray()
            };
        }
    }
}