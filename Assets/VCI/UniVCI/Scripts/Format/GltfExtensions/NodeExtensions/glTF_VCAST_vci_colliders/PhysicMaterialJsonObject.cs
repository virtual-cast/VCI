using System;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// PhysicMaterial info
    /// </summary>
    [Serializable]
    public sealed class PhysicMaterialJsonObject
    {
        public const string AverageCombineString = "average";
        public const string MinimumCombineString = "minimum";
        public const string MaximumCombineString = "maximum";
        public const string MultiplyCombineString = "multiply";

        public float dynamicFriction;
        public float staticFriction;
        public float bounciness;
        public string frictionCombine;
        public string bounceCombine;
    }
}
