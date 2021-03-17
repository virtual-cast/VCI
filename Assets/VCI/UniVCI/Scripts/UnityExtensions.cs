using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VCI
{
    public static class UnityExtensions
    {
        public static float[] ToArray(this Vector2 v)
        {
            return new float[] { v.x, v.y };
        }
    }
}

