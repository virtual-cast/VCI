using System;
using System.Runtime.Serialization;

namespace VCI
{
    [Serializable]
    public class VciValidatorException : Exception
    {
        public VciValidationErrorType ErrorType { get; }

        /// <summary>
        /// バリデーションに引っかかったオブジェクトがある場合、その参照
        /// </summary>
        public UnityEngine.Object Object { get; }

        public VciValidatorException() : base() { }

        public VciValidatorException(VciValidationErrorType errorType) : base("")
        {
            ErrorType = errorType;
        }

        public VciValidatorException(VciValidationErrorType errorType, UnityEngine.Object obj) : base("")
        {
            ErrorType = errorType;
            Object = obj;
        }

        public VciValidatorException(VciValidationErrorType errorType, string message) : base(message)
        {
            ErrorType = errorType;
        }

        public VciValidatorException(VciValidationErrorType errorType, UnityEngine.Object obj, string message) : base(message)
        {
            ErrorType = errorType;
            Object = obj;
        }

        public VciValidatorException(string message) : base(message) { }

        public VciValidatorException(string message, Exception innerException)
            : base(message, innerException) { }

        protected VciValidatorException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
