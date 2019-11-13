using System;
using UnityEngine;
using VCIGLTF;
using VCIJSON;

namespace VCI
{
    [Serializable]
    [JsonSchema(Title = "vci.rect_transform")]
    public class glTF_VCAST_vci_RectTransform : JsonSerializableBase
    {
        [JsonSchema(MinItems = 2, MaxItems = 2)]
        public float[] anchorMin;

        [JsonSchema(MinItems = 2, MaxItems = 2)]
        public float[] anchorMax;

        [JsonSchema(MinItems = 2, MaxItems = 2)]
        public float[] anchoredPosition;

        [JsonSchema(MinItems = 2, MaxItems = 2)]
        public float[] sizeDelta;

        [JsonSchema(MinItems = 2, MaxItems = 2)]
        public float[] pivot;

        protected override void SerializeMembers(GLTFJsonFormatter f)
        {
            f.KeyValue(() => anchorMin);
            f.KeyValue(() => anchorMax);
            f.KeyValue(() => anchoredPosition);
            f.KeyValue(() => sizeDelta);
            f.KeyValue(() => pivot);
        }

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