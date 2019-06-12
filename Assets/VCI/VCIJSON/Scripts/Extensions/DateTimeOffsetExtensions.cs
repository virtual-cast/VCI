using System;

namespace VCIJSON
{
    public static class DateTimeOffsetExtensions
    {
        public const long TicksPerSecond = 10000000;
        public static readonly DateTimeOffset EpochTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
        [Obsolete("Use EpochTime")] public static readonly DateTimeOffset EpocTime = EpochTime;
#if !NET_4_6 && !NET_STANDARD_2_0
        public static long ToUnixTimeSeconds(this DateTimeOffset now)
        {
            if (now < EpochTime)
            {
                throw new ArgumentOutOfRangeException();
            }
            return (now - EpochTime).Ticks / TicksPerSecond;
        }
#endif
    }
}