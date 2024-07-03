using System;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security
{
    internal class DummyAuthenticationService : IAuthenticationService
    {
        public IRepository<UserData> UserRepository =>
            throw new AuthenticationServiceNotActiveException();

        public OnTimedLogoutRequestDelegate OnTimedLogoutRequest
        {
            get => throw new AuthenticationServiceNotActiveException();
            set => throw new AuthenticationServiceNotActiveException();
        }
        public IExternalAuthorization ExternalAuthorization
        {
            get => throw new AuthenticationServiceNotActiveException();
            set => throw new AuthenticationServiceNotActiveException();
        }

        public event OnUserAuthentication OnUserAuthenticateSuccess;
        public event OnUserAuthentication OnUserAuthenticateFailed;
        public event OnUserAuthentication OnDeAuthenticated;
        public event OnUserAuthentication OnDeAuthenticating;

        public IUser AuthenticateUser(string username, string password)
        {
            throw new AuthenticationServiceNotActiveException();
        }

        public string CalculateHash(string clearTextPassword, string salt)
        {
            throw new AuthenticationServiceNotActiveException();
        }

        public void ChangePassword(
            string userName,
            string password,
            string newPassword1,
            string newPassword2
        )
        {
            throw new AuthenticationServiceNotActiveException();
        }

        public void DeAuthenticateCurrentUser()
        {
            throw new AuthenticationServiceNotActiveException();
        }

        public bool HasAuthorization(string roles, Action notAuthorizedAction = null)
        {
            throw new AuthenticationServiceNotActiveException();
        }
    }
}
