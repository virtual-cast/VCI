using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VCI
{
    [CreateAssetMenu]
    public class VCIConfig: ScriptableObject
    {
        private static VCIConfig _config;

        [SerializeField]
        bool _openDirectoryAfterExport;

        [SerializeField]
        int _primaryLanguageIndex;

        [SerializeField]
        VCITermData[] _terms;

        private static VCIConfig Config
        {
            get
            {
                if (_config == null) _config = Resources.Load<VCIConfig>("VCIConfig");
                return _config;
            }
        }

        public static bool IsOpenDirectoryAfterExport => Config._openDirectoryAfterExport;

        public static string GetText(string key)
        {
            var list = new List<VCITermData> {Config._terms[Config._primaryLanguageIndex]};
            list.AddRange(Config._terms.Where((o, i) => i != Config._primaryLanguageIndex));

            foreach (var res in list)
            {
                var text = res.GetText(key);
                if (!string.IsNullOrEmpty(text)) return text;
            }
            return "";
        }
    }
}