using System;

namespace VCI
{
    public abstract class VciException : Exception
    {
        protected VciException()
        {
        }

        public VciException(string message) : base(message)
        {
        }
    }
}
