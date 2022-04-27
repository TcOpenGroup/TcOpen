﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Security;
using TcOpen.Inxton.Local.Security.Properties;

namespace TcOpen.Inxton.Local.Security
{
    public partial class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService(IRepository<UserData> repository)
        {
            this.UserRepository = repository;
        }

        private List<UserData> _users
        {
            get
            {
                var allUsers = GetAllUsers();
                if (!allUsers.Any())
                {
                    CreateDefaultUser();
                    return GetAllUsers();
                }
                else return allUsers;

            }
        }

        private List<UserData> GetAllUsers() => UserRepository.GetRecords("*", Convert.ToInt32(UserRepository.Count + 1), 0).ToList();

        public OnTimedLogoutRequestDelegate OnTimedLogoutRequest { get; set; }

        public event OnUserAuthentication OnDeAuthenticating;
        public event OnUserAuthentication OnUserAuthenticateSuccess;
        public event OnUserAuthentication OnUserAuthenticateFailed;
        public event OnUserAuthentication OnDeAuthenticated;
        private IExternalAuthorization externalAuthorization;
        public IExternalAuthorization ExternalAuthorization
        {
            get { return this.externalAuthorization; }
            set
            {
                externalAuthorization = value;
                externalAuthorization.AuthorizationRequest += ExternalAuthorization_AuthorizationRequest;
                externalAuthorization.AuthorizationTokenChange += ExternalAuthorization_AuthorizationTokenChange;
            }
        }

        private void ExternalAuthorization_AuthorizationTokenChange(string token)
        {
            ChangeToken(SecurityManager.Manager.Principal.Identity.Name, token);
        }

        public void ChangeToken(string userName, string token)
        {

            if (_users.Exists(p => !string.IsNullOrEmpty(p.AuthenticationToken)
                                   && p.AuthenticationToken == this.CalculateHash(token, string.Empty)
                                   && p.Username != userName))
            {
                throw new ExistingTokenException(strings.TokenAssigned);
            }


            var authenticated = _users.FirstOrDefault(p => p.Username == userName);

            if (authenticated != null)
            {
                var user = this.UserRepository.Read(userName);
                user.AuthenticationToken = this.CalculateHash(token, string.Empty);
                this.UserRepository.Update(userName, user);
            }
        }

        private User ExternalAuthorization_AuthorizationRequest(string token)
        {
            return this.AuthenticateUser(token);
        }

        public IRepository<UserData> UserRepository { get; private set; }


        public IUser AuthenticateUser(string username, string clearTextPassword)
        {
            UserData userData = _users.FirstOrDefault(u => u.Username.Equals(username)
                 && u.HashedPassword.Equals(CalculateHash(clearTextPassword, u.Username))
                 && true);
            //
            if (userData == null)
            {
                OnUserAuthenticateFailed?.Invoke(username);
                this.DeAuthenticateCurrentUser();
                TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"User '{username}' has failed to authenticate with a password.{{payload}}", new { });
                throw new UnauthorizedAccessException(strings.AccessDeniedCredentials);
            }

            VerifyRolesHash(userData);

            return AuthenticateUser(userData);
        }

        private User AuthenticateUser(UserData userData)
        {
            var user = new User(userData.Username, userData.Email, userData.Roles.ToArray(), userData.CanUserChangePassword, userData.Level);

            AppIdentity.AppPrincipal customPrincipal = Thread.CurrentPrincipal as AppIdentity.AppPrincipal;
            if (customPrincipal == null)
                throw new ArgumentException(strings.CustomPrincipalError);

            //Authenticate the user
            customPrincipal.Identity = new AppIdentity(user.UserName, user.Email, user.Roles, user.CanUserChangePassword, user.Level);
            UserAccessor.Instance.Identity = customPrincipal.Identity;
            OnUserAuthenticateSuccess?.Invoke(user.UserName);
            SetUserTimedOutDeAuthentication(userData.LogoutTime);
            TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"User '{user.UserName}' has authenticated.{{payload}}", new { UserName = user.UserName, CanChangePassword = user.CanUserChangePassword, Roles = string.Join(",", user.Roles), Id = user.Id });
            return user;
        }

        private void VerifyRolesHash(UserData userData)
        {
            bool roleHashMatches = false;
            if (userData.RoleHash != null)
            {
                roleHashMatches = userData.RoleHash.Equals(CalculateRoleHash(userData.Roles, userData.Username));
            }
            else
            {
                userData.RoleHash = CalculateRoleHash(userData.Roles, userData.Username);
                roleHashMatches = userData.RoleHash.Equals(CalculateRoleHash(userData.Roles, userData.Username));
            }



            if (!roleHashMatches)
            {
                throw new UnauthorizedAccessException(strings.AccessDeniedPermissions);
            }
        }

        public User AuthenticateUser(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                OnUserAuthenticateFailed?.Invoke("empty token");
                this.DeAuthenticateCurrentUser();
                TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"User has failed to authenticate with a token (empty token).{{payload}}", new { });
                throw new UnauthorizedAccessException(strings.AccessDeniedEmptyToken);
            }

            var userData = _users.FirstOrDefault(u => u.AuthenticationToken != null && u.AuthenticationToken.Equals(CalculateHash(token, string.Empty)));
            if (userData == null)
            {
                OnUserAuthenticateFailed?.Invoke("unknown token");
                this.DeAuthenticateCurrentUser();
                TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"User has failed to authenticate with a token (non-existing token).{{payload}}", new { });
                throw new UnauthorizedAccessException(strings.AccessDeniedInvalidToken);
            }

            VerifyRolesHash(userData);

            return AuthenticateUser(userData);
        }

        public void DeAuthenticateCurrentUser()
        {
            AppIdentity.AppPrincipal customPrincipal = Thread.CurrentPrincipal as AppIdentity.AppPrincipal;
            if (customPrincipal != null)
            {
                var userName = customPrincipal.Identity.Name;
                OnDeAuthenticating?.Invoke(userName);
                customPrincipal.Identity = new AppIdentity.AnonymousIdentity();
                UserAccessor.Instance.Identity = customPrincipal.Identity;
                OnDeAuthenticated?.Invoke(userName);
                TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"User '{userName}' has de-authenticated.{{payload}}", new { UserName = userName });
            }
        }

        public void ChangePassword(string userName, string password, string newPassword1, string newPassword2)
        {
            if (newPassword1 != newPassword2)
                throw new PasswordsDoNotMatchException(strings.PasswordMismatch);

            var authenticated = AuthenticateUser(userName, password);

            if (authenticated.UserName == userName)
            {
                var user = this.UserRepository.Read(userName);
                user.HashedPassword = this.CalculateHash(newPassword1, userName);
                this.UserRepository.Update(userName, user);
                TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"User '{authenticated.UserName}' has changed password.{{payload}}", 
                    new { UserName = authenticated.UserName, CanChangePassword = authenticated.CanUserChangePassword, Roles = string.Join(",", authenticated.Roles) });
            }
        }

        public string CalculateHash(string textToHash, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(textToHash + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }

        private String CalculateRoleHash(IEnumerable<string> roles, string username)
        {
            return CalculateHash(String.Join(",", roles.OrderByDescending(x => x).ToList()), username);
        }

        private void CreateDefaultUser()
        {
            var newUser = new UserData(
                   "default",
                   string.Empty,
                   string.Empty,
                   new string[] { "Administrator" }, "Administrator", string.Empty);
            newUser.CanUserChangePassword = true;
            UserRepository.Create(newUser.Username, newUser);
        }

        private void SetUserTimedOutDeAuthentication(TimeSpan deauthRequestTime)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Enabled = false;
            if (deauthRequestTime.TotalSeconds > 0)
            {
                aTimer.Interval = deauthRequestTime.TotalMilliseconds;
                aTimer.Elapsed += ATimer_Elapsed;
                aTimer.Enabled = true;
            }
        }

        private void ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (OnTimedLogoutRequest == null)
            {
                this.DeAuthenticateCurrentUser();
                return;
            }

            if (OnTimedLogoutRequest != null && OnTimedLogoutRequest())
            {
                this.DeAuthenticateCurrentUser();
            }
        }
    }
}
