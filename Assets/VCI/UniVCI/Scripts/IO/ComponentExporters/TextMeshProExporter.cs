using TMPro;
using UniGLTF;
using UnityEngine;
using ColorSpace = VRMShaders.ColorSpace;

namespace VCI
{
    public static class TextMeshProExporter
    {
        public static glTF_VCAST_vci_rectTransform ExportTextMeshProRectTransform(Transform node)
        {
            var textMeshPro = node.GetComponent<TextMeshPro>();
            var rectTransform = node.GetComponent<RectTransform>();

            if (textMeshPro == null || rectTransform == null)
            {
                return null;
            }

            return new glTF_VCAST_vci_rectTransform
            {
                rectTransform = ExportRectTransform(rectTransform)
            };
        }

        public static glTF_VCAST_vci_text ExportTextMeshProText(Transform node)
        {
            var textMeshPro = node.GetComponent<TextMeshPro>();
            var rectTransform = node.GetComponent<RectTransform>();

            if (textMeshPro == null || rectTransform == null)
            {
                return null;
            }

            return new glTF_VCAST_vci_text
            {
                text = ExportTextMeshPro(textMeshPro),
            };
        }

        private static RectTransformJsonObject ExportRectTransform(RectTransform rt)
        {
            return new RectTransformJsonObject
            {
                anchorMin = rt.anchorMin.ToArray(),
                anchorMax = rt.anchorMax.ToArray(),
                anchoredPosition = rt.anchoredPosition.ToArray(),
                sizeDelta = rt.sizeDelta.ToArray(),
                pivot = rt.pivot.ToArray()
            };
        }

        private static TextJsonObject ExportTextMeshPro(TextMeshPro tmp)
        {
            return new TextJsonObject
            {
                fontName = tmp.font.faceInfo.familyName,
                text = tmp.text,
                richText =  tmp.richText,
                autoSize =  tmp.autoSizeTextContainer,
                fontSize = tmp.fontSize,
                fontStyle = (int)tmp.fontStyle,
                color = tmp.color.ToFloat4(ColorSpace.sRGB, ColorSpace.sRGB),
                enableVertexGradient = tmp.enableVertexGradient,
                topLeftColor = tmp.colorGradient.topLeft.ToFloat4(ColorSpace.sRGB, ColorSpace.sRGB),
                topRightColor = tmp.colorGradient.topRight.ToFloat4(ColorSpace.sRGB, ColorSpace.sRGB),
                bottomLeftColor = tmp.colorGradient.bottomLeft.ToFloat4(ColorSpace.sRGB, ColorSpace.sRGB),
                bottomRightColor = tmp.colorGradient.bottomRight.ToFloat4(ColorSpace.sRGB, ColorSpace.sRGB),
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