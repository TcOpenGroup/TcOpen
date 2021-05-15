using System;
using System.Runtime.Serialization;

namespace TcoCore.Threading
{
    public class MissingUiDispactcherException : Exception
    {
        public MissingUiDispactcherException()
        {
            
        }

        public MissingUiDispactcherException(string message) : base(message)
        {
        }

        public MissingUiDispactcherException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingUiDispactcherException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
