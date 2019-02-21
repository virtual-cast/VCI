#pragma warning disable
using System;

namespace VCI
{
    public class VCISpecVersion
    {
        public const int Major = 0;

        //public const int Minor = 10;

        // “––ÊUniVCI‚Ìƒo[ƒWƒ‡ƒ“‚Æ“¯‚¶
        public const int Minor = VCIVersion.MINOR;

        public static string Version
        {
            get
            {
                return String.Format("{0}.{1}", Major, Minor);
            }
        }
    }
}
