using System;
using System.Globalization;

namespace VCI
{
    public sealed class VciMigrationFlags
    {
        /// <summary>
        /// システムの VCI バージョン
        /// </summary>
        public int RuntimeVciVersionNumber { get; } = GetVersionNumber(VCIVersion.VCI_VERSION);

        /// <summary>
        /// VCI ファイルの出力 VCI バージョン
        /// </summary>
        public int ExportedVciVersionNumber { get; } = GetVersionNumber(VCIVersion.VCI_VERSION);

        /// <summary>
        /// UniVCI v0.30 未満のバージョンでは Attractable フラグは未定義のため true と見做したい。
        /// </summary>
        public bool IsItemAttractableFlagUndefined { get; }

        /// <summary>
        /// UniVCI v0.27 以下のバージョンでは baseColor が sRGB で記録されているため linear に変換したい。
        /// </summary>
        public bool IsPbrBaseColorSrgb { get; }

        /// <summary>
        /// UniVCI v0.32 未満のバージョンでは AudioClip の配置場所を記録する拡張は存在しないため root に Attach したい。
        /// </summary>
        public bool IsAudioClipAttachPointUndefined { get; }

        public VciMigrationFlags(glTF_VCAST_vci_meta meta)
        {
            if (meta == null) return;

            ExportedVciVersionNumber = GetVersionNumber(meta.exporterVCIVersion);

            IsItemAttractableFlagUndefined = ExportedVciVersionNumber < 30;
            IsPbrBaseColorSrgb = ExportedVciVersionNumber <= 27;
            IsAudioClipAttachPointUndefined = ExportedVciVersionNumber < 32;
        }

        private static int GetVersionNumber(string exportedVciVersion)
        {
            return (int) Math.Round(float.Parse(GetVersionValue(exportedVciVersion), CultureInfo.InvariantCulture) * 100);
        }

        private static string GetVersionValue(string exportedVciVersion)
        {
            if (string.IsNullOrEmpty(exportedVciVersion))
            {
                throw new Exception("exportedVciVersion is empty.");
            }

            System.Text.RegularExpressions.Regex r =
                new System.Text.RegularExpressions.Regex(
                    @"UniVCI-(?<version>[+-]?[0-9]+[.]?[0-9]([eE][+-])?[0-9])",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase
                    | System.Text.RegularExpressions.RegexOptions.Singleline);
            var match = r.Match(exportedVciVersion);
            var version = match.Groups["version"].Value;
            return version;
        }

    }
}