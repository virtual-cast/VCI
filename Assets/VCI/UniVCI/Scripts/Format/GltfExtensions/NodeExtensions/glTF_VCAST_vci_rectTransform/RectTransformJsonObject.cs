using System;
using UnityEngine;
using UniGLTF;

namespace VCI
{
    [Serializable]
    [UniGLTF.JsonSchema(Title = "vci.rect_transform")]
    public class RectTransformJsonObject
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
    }
}