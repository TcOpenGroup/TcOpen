using System;

namespace TcOpen.Inxton.Security
{
    /// <summary>
    /// Provides abstraction for authentication service in a vortex application.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Attempts to authenticate the user.
        /// </summary>
        /// <param name="username">User name</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        IUser AuthenticateUser(string username, string password);

        /// <summary>
        /// De-authenticates current user.
        /// </summary>
        void DeAuthenticateCurrentUser(); 
        
        /// <summary>
        /// Calculates hash of the password
        /// </summary>
        /// <param name="clearTextPassword">Plain test password</param>
        /// <param name="salt">Salt</param>
        /// <returns></returns>
        string CalculateHash(string clearTextPassword, string salt);

        /// <summary>
        /// Changes the password of given user.
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">Old password</param>
        /// <param name="newPassword1">New password</param>
        /// <param name="newPassword2">New password</param>
        void ChangePassword(string userName, string password, string newPassword1, string newPassword2);
        
        /// <summary>
        /// Occurs when the uses is successfully authenticated.
        /// </summary>
        event OnUserAuthentication OnUserAuthenticateSuccess;

        /// <summary>
        /// Occurs when user authentication fails. 
        /// </summary>
        event OnUserAuthentication OnUserAuthenticateFailed;

        /// <summary>
        /// Occurs when current user is to be de-authenticated.
        /// </summary>
        event OnUserAuthentication OnDeAuthenticating;

        /// <summary>
        /// Occurs when current user is de-authenticated.
        /// </summary>
        event OnUserAuthentication OnDeAuthenticated;

        /// <summary>
        /// Delegate is used to prevent/allow automatic user logout. 
        /// </summary>
        OnTimedLogoutRequestDelegate OnTimedLogoutRequest { get; set; }

        /// <summary>
        /// Get or set external authentication device handling.
        /// </summary>
        IExternalAuthorization ExternalAuthorization { get; set; }

        bool HasAuthorization(string roles, Action notAuthorizedAction = null);
    }
}
