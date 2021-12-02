using System;
using UnityEngine;

namespace VCI
{
    // UPMに登録する正式なパッケージ名（not 表示名）
    // TODO: companyName, packageNamespaceをValidateする
    [Serializable]
    internal sealed class OfficialPackageName
    {
        [SerializeField]
        private string _domainNameExtension;
        [SerializeField]
        private string _companyName;
        [SerializeField]
        private string _packageNamespace;

        public OfficialPackageName(string domainNameExtension, string companyName, string packageNamespace)
        {
            _domainNameExtension = domainNameExtension;
            _companyName = companyName;
            _packageNamespace = packageNamespace;
        }

        public string GetValue()
        {
            return $"{_domainNameExtension}.{_companyName}.{_packageNamespace}";
        }
    }
}