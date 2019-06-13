using System;

namespace VCIJSON
{
    public class MsgPackTypeException : Exception
    {
        public MsgPackTypeException(string msg) : base(msg)
        {
        }
    }
}