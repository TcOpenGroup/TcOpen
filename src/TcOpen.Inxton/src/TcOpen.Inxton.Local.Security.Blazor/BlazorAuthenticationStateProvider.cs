using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TcOpen.Inxton.Security;
using System.Collections.Generic;
using TcOpen.Inxton.Data;
using System.Linq;
using TcOpen.Inxton.Local.Security.Properties;
using System.Text;
using System.Security.Cryptography;

namespace TcOpen.Inxton.Local.Security.Blazor
{
    /// <summary>
    /// Authentication state provider.
    /// </summary>
    public class BlazorAuthenticationStateProvider : AuthenticationStateProvider, IAuthenticationService
    {
        private IRepository<UserData> UserRepository;
        private BlazorRoleGroupManager roleGroupManager;

        public BlazorAuthenticationStateProvider(IRepository<UserData> userRepo, BlazorRoleGroupManager roleGroupManager)
        {
            this.UserRepository = userRepo;
            this.roleGroupManager = roleGroupManager;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (UserAccessor.Instance.Identity != null && UserAccessor.Instance.Identity.Name != string.Empty)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, UserAccessor.Instance.Identity.Name));

                foreach (var role in UserAccessor.Instance.Identity.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var identity = new ClaimsIdentity(claims, "Auth");

                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
            }
            else
            {
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }
        }

        public event OnUserAuthentication OnUserAuthenticateSuccess;
        public event OnUserAuthentication OnUserAuthenticateFailed;
        public event OnUserAuthentication OnDeAuthenticating;
        public event OnUserAuthentication OnDeAuthenticated;

        public OnTimedLogoutRequestDelegate OnTimedLogoutRequest { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IExternalAuthorization ExternalAuthorization { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        private String CalculateRoleHash(IEnumerable<string> roles, string username)
        {
            return CalculateHash(String.Join(",", roles.OrderByDescending(x => x).ToList()), username);
        }

        private void CreateDefaultUser()
        {
            var user = new User("admin", null, new string[] { "AdminGroup" }, false, null);
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = CalculateHash("admin", "admin");

            var userEntity = new UserData(user);
            UserRepository.Create(user.NormalizedUserName, userEntity);
        }

        private readonly System.Timers.Timer deauthenticateTimer = new System.Timers.Timer();
        private void SetUserTimedOutDeAuthentication(TimeSpan deauthRequestTime)
        {
            deauthenticateTimer.AutoReset = true;
            deauthenticateTimer.Enabled = false;
            deauthenticateTimer.Stop();
            if (deauthRequestTime.TotalSeconds > 0)
            {
                deauthenticateTimer.Interval = deauthRequestTime.TotalMilliseconds;
                deauthenticateTimer.Elapsed += ATimer_Elapsed;
                deauthenticateTimer.Enabled = true;
            }
        }

        private void ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (OnTimedLogoutRequest == null)
            {
                this.DeAuthenticateCurrentUser();
                deauthenticateTimer.Stop();
                return;
            }

            if (OnTimedLogoutRequest != null && OnTimedLogoutRequest())
            {
                this.DeAuthenticateCurrentUser();
                deauthenticateTimer.Stop();
            }
        }

        public IUser AuthenticateUser(string username, string password)
        {
            UserData userData = _users.FirstOrDefault(u => u.Username.Equals(username)
                 && u.HashedPassword.Equals(CalculateHash(password, u.Username))
                 && true);
            //
            if (userData == null)
            {
                OnUserAuthenticateFailed?.Invoke(username);
                this.DeAuthenticateCurrentUser();
                TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"User '{username}' has failed to authenticate with a password.{{payload}}", new { });
                throw new UnauthorizedAccessException("AccessDeniedCredentials");
            }

            VerifyRolesHash(userData);

            return AuthenticateUser(userData);
        }

        private User AuthenticateUser(UserData userData)
        {
            var user = new User(userData.Username, userData.Email, userData.Roles.ToArray(), userData.CanUserChangePassword, userData.Level);

            AppIdentity.AppPrincipal customPrincipal = Thread.CurrentPrincipal as AppIdentity.AppPrincipal;
            if (customPrincipal == null)
                throw new ArgumentException("CustomPrincipalError");

            //Authenticate the user
            string[] roles = new string[] { };
            if (user.Roles.Length > 0 && user.Roles[0] != null) //TODO
            {
                roles = roleGroupManager.GetRolesFromGroup(user.Roles[0]).ToArray();
            }

            customPrincipal.Identity = new AppIdentity(user.UserName, user.Email, roles, user.CanUserChangePassword, user.Level);
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
                throw new UnauthorizedAccessException("AccessDeniedPermissions");
            }
        }

        public User AuthenticateUser(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                OnUserAuthenticateFailed?.Invoke("empty token");
                this.DeAuthenticateCurrentUser();
                TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"User has failed to authenticate with a token (empty token).{{payload}}", new { });
                throw new UnauthorizedAccessException("AccessDeniedEmptyToken");
            }

            var userData = _users.FirstOrDefault(u => u.AuthenticationToken != null && u.AuthenticationToken.Equals(CalculateHash(token, string.Empty)));
            if (userData == null)
            {
                OnUserAuthenticateFailed?.Invoke("unknown token");
                this.DeAuthenticateCurrentUser();
                TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"User has failed to authenticate with a token (non-existing token).{{payload}}", new { });
                throw new UnauthorizedAccessException("AccessDeniedInvalidToken");
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
                //UserAccessor.Instance.Identity = null;
                UserAccessor.Instance.Identity = new AppIdentity.AnonymousIdentity();
                //customPrincipal.Identity = new AppIdentity.AnonymousIdentity();
                OnDeAuthenticated?.Invoke(userName);
                TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"User '{userName}' has de-authenticated.{{payload}}", new { UserName = userName });
            }
        }

        public string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }

        public void ChangePassword(string userName, string password, string newPassword1, string newPassword2)
        {
            if (newPassword1 != newPassword2)
                throw new PasswordsDoNotMatchException("PasswordMismatch");

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

        public bool HasAuthorization(string roles, Action notAuthorizedAction = null)
        {
            return AuthorizationChecker.HasAuthorization(roles, notAuthorizedAction);
        }
    }
}
