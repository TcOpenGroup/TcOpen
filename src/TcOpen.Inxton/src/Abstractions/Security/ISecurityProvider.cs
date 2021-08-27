using System;

namespace TcOpen.Inxton.Security
{
    public interface ISecurityProvider
    {
        /// <summary>
        /// Gets the authentication service for this application.
        /// </summary>
        IAuthenticationService AuthenticationService { get; }
    }
}
