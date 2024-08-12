using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;

namespace TcoData
{
    public class BrowsableObjectCreationException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="BrowsableObjectCreationException" /> class.</summary>
        public BrowsableObjectCreationException() { }

        /// <summary>Initializes a new instance of the <see cref="BrowsableObjectCreationException" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error. </param>
        public BrowsableObjectCreationException(string message)
            : base(message) { }

        /// <summary>Initializes a new instance of the <see cref="BrowsableObjectCreationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified. </param>
        public BrowsableObjectCreationException(string message, Exception innerException)
            : base(message, innerException) { }

        /// <summary>Initializes a new instance of the <see cref="BrowsableObjectCreationException" /> class with serialized data.</summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />. </exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">The class name is <see langword="null" /> or <see cref="System.Exception.HResult" /> is zero (0). </exception>
        [SecuritySafeCritical]
        protected BrowsableObjectCreationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
