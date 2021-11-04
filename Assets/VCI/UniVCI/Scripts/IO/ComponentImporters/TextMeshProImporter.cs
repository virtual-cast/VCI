using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UniGLTF;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public static class TextMeshProImporter
    {
        public static async Task<List<TextMeshPro>> LoadAsync(
            VciData vciData,
            IList<Transform> unityNodes,
            IFontProvider fontProvider,
            IAwaitCaller awaitCaller)
        {
            var texts = new List<TextMeshPro>();

            // NOTE: Text 拡張と RectTransform 拡張のどちらも存在する node が VCI として正常なので、それを対象とする.
            var textExtensions = vciData.TextNodes.ToDictionary(x => x.gltfNodeIdx, x => x.extension);
            var extensionPairs = new Dictionary<int, (glTF_VCAST_vci_text, glTF_VCAST_vci_rectTransform)>();
            foreach (var (nodeIdx, rectTransformExtension) in vciData.RectTransformNodes)
            {
                if (textExtensions.ContainsKey(nodeIdx))
                {
                    extensionPairs.Add(nodeIdx, (textExtensions[nodeIdx], rectTransformExtension));
                }
            }

            foreach (var kv in extensionPairs)
            {
                var nodeIdx = kv.Key;
                var (textExtension, rectTransformExtension) = kv.Value;
                var go = unityNodes[nodeIdx].gameObject;

                var textJson = textExtension.text;
                var rectTransformJson = rectTransformExtension.rectTransform;
                if (textJson == null || rectTransformJson == null) continue;

                var rt = go.AddComponent<RectTransform>();
                // NOTE: RectTransformのAddComponentで元のtransformが置き換わるのでNodesも更新する。
                unityNodes[nodeIdx] = rt.transform;
                var tmp = go.AddComponent<TextMeshPro>();

                // Get font
                if (fontProvider != null)
                {
                    var font = fontProvider.GetDefaultFont();
                    if (font != null) tmp.font = font;
                }

                // Set TMPText
                tmp.text = textJson.text;
                tmp.richText = textJson.richText;
                tmp.fontSize = textJson.fontSize;
                tmp.autoSizeTextContainer = textJson.autoSize;
                tmp.fontStyle = (FontStyles) textJson.fontStyle;
                tmp.color = new Color(textJson.color[0], textJson.color[1], textJson.color[2], textJson.color[3]);
                tmp.enableVertexGradient = textJson.enableVertexGradient;
                tmp.colorGradient = new VertexGradient(
                    new Color(textJson.topLeftColor[0], textJson.topLeftColor[1], textJson.topLeftColor[2],
                        textJson.topLeftColor[3]),
                    new Color(textJson.topRightColor[0], textJson.topRightColor[1], textJson.topRightColor[2],
                        textJson.topRightColor[3]),
                    new Color(textJson.bottomLeftColor[0], textJson.bottomLeftColor[1], textJson.bottomLeftColor[2],
                        textJson.bottomLeftColor[3]),
                    new Color(textJson.bottomRightColor[0], textJson.bottomRightColor[1],
                        textJson.bottomRightColor[2], textJson.bottomRightColor[3])
                );
                tmp.characterSpacing = textJson.characterSpacing;
                tmp.wordSpacing = textJson.wordSpacing;
                tmp.lineSpacing = textJson.lineSpacing;
                tmp.paragraphSpacing = textJson.paragraphSpacing;
                tmp.alignment = (TextAlignmentOptions) textJson.alignment;
                tmp.enableWordWrapping = textJson.enableWordWrapping;
                // NOTE: overflowModeのインポート時にエラーになる可能性があるので無効にする。
                // tmp.overflowMode = (TextOverflowModes) vci_text.overflowMode;
                tmp.enableKerning = textJson.enableKerning;
                tmp.extraPadding = textJson.extraPadding;
                tmp.margin = new Vector4(textJson.margin[0], textJson.margin[1], textJson.margin[2],
                    textJson.margin[3]);

                // Set RectTransform
                rt.anchorMin = new Vector2(rectTransformJson.anchorMin[0], rectTransformJson.anchorMin[1]);
                rt.anchorMax = new Vector2(rectTransformJson.anchorMax[0], rectTransformJson.anchorMax[1]);
                rt.anchoredPosition = new Vector2(rectTransformJson.anchoredPosition[0], rectTransformJson.anchoredPosition[1]);
                rt.sizeDelta = new Vector2(rectTransformJson.sizeDelta[0], rectTransformJson.sizeDelta[1]);
                rt.pivot = new Vector2(rectTransformJson.pivot[0], rectTransformJson.pivot[1]);

                texts.Add(tmp);

                // NOTE: TMP の追加は非常にスパイクが大きい。(2ms)
                await awaitCaller.NextFrame();
            }

            return texts;
        }


    }
}