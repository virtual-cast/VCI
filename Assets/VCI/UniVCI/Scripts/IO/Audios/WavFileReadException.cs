using System;

namespace VCI
{
    public sealed class WavFileReadException : Exception
    {
        public WavFileReadException(string message) : base(message)
        {
        }
    }
}
