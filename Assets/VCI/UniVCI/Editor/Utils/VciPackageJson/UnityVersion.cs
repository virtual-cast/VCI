using System;
using UnityEngine;

namespace VCI
{
    // パッケージに互換性がある最小の Unity バージョン
    [Serializable]
    internal sealed class UnityVersion
    {
        [SerializeField]
        private int _major;

        [SerializeField]
        public int _minor;

        public UnityVersion(int major, int minor)
        {
            _major = major;
            _minor = minor;
        }

        public string GetValue()
        {
            return $"{_major}.{_minor}";
        }
    }
}