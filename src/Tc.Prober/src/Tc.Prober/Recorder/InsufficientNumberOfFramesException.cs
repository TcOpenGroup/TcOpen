namespace Tc.Prober.Recorder
{
    using System;
    using System.Runtime.Serialization;

    public class InsufficientNumberOfFramesException : Exception
    {
        public InsufficientNumberOfFramesException()
        {
        }

        public InsufficientNumberOfFramesException(string message) : base(message)
        {
        }

        public InsufficientNumberOfFramesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InsufficientNumberOfFramesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
