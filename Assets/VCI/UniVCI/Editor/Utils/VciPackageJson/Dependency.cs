using System;
using UnityEngine;

namespace VCI
{
    // 依存パッケージ
    [Serializable]
    internal sealed class Dependency
    {
        [SerializeField]
        private OfficialPackageName _officialPackageName;

        [SerializeField]
        private PackageVersion _packageVersion;

        public OfficialPackageName OfficialPackageName => _officialPackageName;
        public PackageVersion PackageVersion => _packageVersion;

        public Dependency(OfficialPackageName officialPackageName, PackageVersion packageVersion)
        {
            _officialPackageName = officialPackageName;
            _packageVersion = packageVersion;
        }
    }
}