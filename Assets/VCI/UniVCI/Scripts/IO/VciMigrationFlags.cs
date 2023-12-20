using System.Text.RegularExpressions;

namespace VCI
{
    public sealed class VciMigrationFlags
    {
        /// <summary>
        /// システムの VCI バージョン
        /// </summary>
        public static int RuntimeVciMajorVersion => VCIVersion.MAJOR;

        public static int RuntimeVciMinorVersion => VCIVersion.MINOR;

        /// <summary>
        /// VCI ファイルの出力 VCI バージョン
        /// </summary>
        public int FileVciMajorVersion { get; } = VCIVersion.MAJOR;

        public int FileVciMinorVersion { get; } = VCIVersion.MINOR;

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

        /// <summary>
        /// UniVCI v0.36 未満のバージョンでは AttractableDistance は未定義のため、デフォルト値としたい。
        /// </summary>
        public bool IsItemAttractableDistanceUndefined { get; }

        /// <summary>
        /// UniVCI v0.37 未満のバージョンでは Key は未定義のため、 NodeIndex の値を Key としたい。
        /// </summary>
        public bool IsSubItemKeyUndefined { get; }

        public VciMigrationFlags(string exporterVciVersion)
        {
            if (string.IsNullOrEmpty(exporterVciVersion)) return;

            (FileVciMajorVersion, FileVciMinorVersion) = GetVersionValue(exporterVciVersion);

            IsItemAttractableFlagUndefined = FileVciMajorVersion == 0 && FileVciMinorVersion < 30;
            IsPbrBaseColorSrgb = FileVciMajorVersion == 0 && FileVciMinorVersion <= 27;
            IsAudioClipAttachPointUndefined = FileVciMajorVersion == 0 && FileVciMinorVersion < 32;
            IsItemAttractableDistanceUndefined = FileVciMajorVersion == 0 && FileVciMinorVersion < 36;
            IsSubItemKeyUndefined = FileVciMajorVersion == 0 && FileVciMinorVersion < 37;
        }

        private static (int major, int minor) GetVersionValue(string exportedVciVersion)
        {
            if (string.IsNullOrEmpty(exportedVciVersion))
            {
                throw new VciMigrationException("exportedVciVersion is empty.");
            }

            var r = new Regex(
                @"UniVCI-(?<major>[0-9]+)\.(?<minor>[0-9]+)",
                RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var match = r.Match(exportedVciVersion);
            var major = int.Parse(match.Groups["major"].Value);
            var minor = int.Parse(match.Groups["minor"].Value);
            return (major, minor);
        }
    }
}
