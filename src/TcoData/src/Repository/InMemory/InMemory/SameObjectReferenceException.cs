using System;
using System.Runtime.Serialization;

namespace TcOpen.Inxton.Data.InMemory
{
    public class SameObjectReferenceException : Exception
    {
        public SameObjectReferenceException() { }

        public SameObjectReferenceException(string message)
            : base(message) { }

        public SameObjectReferenceException(string message, Exception innerException)
            : base(message, innerException) { }

        protected SameObjectReferenceException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
