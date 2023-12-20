namespace VCI
{
    public sealed class NotSupportedVciVersionException : VciException
    {
        public NotSupportedVciVersionException(string message) : base(message)
        {
        }
    }
}
