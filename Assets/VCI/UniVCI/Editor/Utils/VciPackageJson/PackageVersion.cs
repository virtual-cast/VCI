using System;
using UnityEngine;

namespace VCI
{
    // パッケージのバージョン
    // * セマンティックバージョニングに従う
    [Serializable]
    internal sealed class PackageVersion
    {
        [SerializeField]
        private int _major;
        [SerializeField]
        private int _minor;
        [SerializeField]
        private int _patch;

        public PackageVersion(int major, int minor, int patch)
        {
            _major = major;
            _minor = minor;
            _patch = patch;
        }

        public string GetValue()
        {
            return $"{_major}.{_minor}.{_patch}";
        }
    }
}