using System;

namespace VCI
{
    /// <summary>
    /// VCI の仕様のバージョン.
    /// 定義として存在させているが、現状は UniVCI の Major.Minor バージョンと同一.
    /// </summary>
    public sealed class VCISpecVersion
    {
        public const int Major = 0;
        public const int Minor = VCIVersion.MINOR;

        public static string Version => $"{Major}.{Minor}";
    }
}