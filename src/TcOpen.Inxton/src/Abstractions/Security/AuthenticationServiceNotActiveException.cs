using System;
using System.Runtime.Serialization;

namespace TcOpen.Inxton.Security
{
    /// <summary>
    /// Authentication service is not available exception.
    /// </summary>
    public class AuthenticationServiceNotActiveException : System.Security.SecurityException
    {
        /// <summary>Initializes a new instance of the <see cref="AuthenticationServiceNotActiveException"></see> class with serialized data.</summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="info">info</paramref> is null.</exception>
        protected AuthenticationServiceNotActiveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        /// <summary>Initializes a new instance of the <see cref="AuthenticationServiceNotActiveException"></see> class with a specified error message.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public AuthenticationServiceNotActiveException(
            string message = "Authentication service is not active. You must assign appropriate authentication service.") : base(message)
        {

        }

        /// <summary>Initializes a new instance of the <see cref="AuthenticationServiceNotActiveException"></see> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception. If the inner parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
        public AuthenticationServiceNotActiveException(string message, Exception inner) : base(message, inner)
        {

        }

        /// <summary>Initializes a new instance of the <see cref="AuthenticationServiceNotActiveException"></see> class with a specified error message and the permission type that caused the exception to be thrown.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="type">The type of the permission that caused the exception to be thrown.</param>
        public AuthenticationServiceNotActiveException(string message, Type type) : base(message, type)
        {

        }

        /// <summary>Initializes a new instance of the <see cref="AuthenticationServiceNotActiveException"></see> class with a specified error message, the permission type that caused the exception to be thrown, and the permission state.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="type">The type of the permission that caused the exception to be thrown.</param>
        /// <param name="state">The state of the permission that caused the exception to be thrown.</param>
        public AuthenticationServiceNotActiveException(string message, Type type, string state) : base(message, type, state)
        {

        }
    }
}
