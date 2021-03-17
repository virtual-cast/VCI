using System;
using UnityEngine;
using UniGLTF;

namespace VCI
{
    [Serializable]
    [UniGLTF.JsonSchema(Title = "vci.rect_transform")]
    public class glTF_VCAST_vci_RectTransform
    {
        [UniGLTF.JsonSchema(MinItems = 2, MaxItems = 2)]
        public float[] anchorMin;

        [UniGLTF.JsonSchema(MinItems = 2, MaxItems = 2)]
        public float[] anchorMax;

        [UniGLTF.JsonSchema(MinItems = 2, MaxItems = 2)]
        public float[] anchoredPosition;

        [UniGLTF.JsonSchema(MinItems = 2, MaxItems = 2)]
        public float[] sizeDelta;

        [UniGLTF.JsonSchema(MinItems = 2, MaxItems = 2)]
        public float[] pivot;

        public static glTF_VCAST_vci_RectTransform CreateFromRectTransform(RectTransform rt)
        {
            return new glTF_VCAST_vci_RectTransform
            {
                anchorMin = rt.anchorMin.ToArray(),
                anchorMax = rt.anchorMax.ToArray(),
                anchoredPosition = rt.anchoredPosition.ToArray(),
                sizeDelta = rt.sizeDelta.ToArray(),
                pivot = rt.pivot.ToArray()
            };
        }
    }
}