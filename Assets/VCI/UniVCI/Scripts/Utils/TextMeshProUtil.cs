using System;
using TMPro;

namespace VCI
{
    public static class TextMeshProUtil
    {
        public static void FitFontSize(TextMeshPro textMeshPro, float minFontSize, float maxFontSize)
        {
            if (textMeshPro == null || string.IsNullOrEmpty(textMeshPro.text)) { return; }

            var targetWidth = textMeshPro.rectTransform.rect.width - textMeshPro.margin.x - textMeshPro.margin.z;
            var targetHeight = textMeshPro.rectTransform.rect.height - textMeshPro.margin.y - textMeshPro.margin.w;

            textMeshPro.fontSize = 1;
            var textWidth = textMeshPro.preferredWidth;
            var textHeight = textMeshPro.preferredHeight;

            // marginが正の場合はpreferredWidth/Heightに加算されているが、負の場合は加算されていない
            textWidth -= textMeshPro.margin.x > 0 ? textMeshPro.margin.x : 0;
            textWidth -= textMeshPro.margin.z > 0 ? textMeshPro.margin.z : 0;
            textHeight -= textMeshPro.margin.y > 0 ? textMeshPro.margin.y : 0;
            textHeight -= textMeshPro.margin.w > 0 ? textMeshPro.margin.w : 0;

            // wrapping有効時と無効時で計算方法を同じにする（AutoSizeの挙動に準拠）
            // 折り返しが発生していたらそれ以上フォントサイズを大きくしない
            var ratio = Math.Min(targetWidth / textWidth, targetHeight / textHeight);
            textMeshPro.fontSize = Math.Min(maxFontSize, Math.Max(minFontSize, textMeshPro.fontSize * ratio));
        }
    }
}
