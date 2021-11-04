using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VCI
{
    public static class HumanBodyBonesUtil
    {
        private static readonly HumanBodyBones[] Values = Enum.GetValues(typeof(HumanBodyBones)) as HumanBodyBones[];
        private static readonly string[] Strings = Values.Select(x => x.ToString()).ToArray();

        private static readonly Dictionary<string, HumanBodyBones> DeserializeMapping = Values
            .ToDictionary(x => x.ToString(), x => x);

        public static HumanBodyBones[] GetValues()
        {
            return Values;
        }

        public static string[] GetStrings()
        {
            return Strings;
        }

        public static bool TryDeserializing(string src, out HumanBodyBones val)
        {
            if (DeserializeMapping.TryGetValue(src, out val))
            {
                return true;
            }
            else
            {
                val = HumanBodyBones.LastBone;
                return false;
            }
        }
    }
}