using System.Collections.Generic;
using UnityEngine;

namespace VCI
{
    // ref: https://docs.unity3d.com/ja/2019.4/Manual/upm-manifestPkg.html
    // UniVCIパッケージのpackage.jsonに書き込むべき情報をまとめたクラス
    [CreateAssetMenu(fileName = "VciPackageSettings", menuName = "ScriptableObject/Create VciPackageSettings")]
    internal sealed class VciPackageSettings : ScriptableObject
    {
        // required
        [SerializeField]
        private OfficialPackageName _officialPackageName;

        // required
        [SerializeField]
        private PackageVersion _packageVersion;

        [SerializeField]
        private string _displayName;

        [SerializeField]
        private string _description;

        [SerializeField]
        private UnityVersion _unityVersion;

        [SerializeField]
        private List<string> _keywords;

        [SerializeField]
        private Author _author;

        [SerializeField]
        private List<Dependency> _dependencies;

        public OfficialPackageName OfficialPackageName => _officialPackageName;
        public PackageVersion PackageVersion => _packageVersion;
        public string DisplayName => _displayName;
        public string Description => _description;
        public string UnityVersion => _unityVersion.GetValue();
        public IReadOnlyList<string> Keywords => _keywords;
        public Author Author => _author;
        public IReadOnlyList<Dependency> Dependencies => _dependencies;
    }






}
