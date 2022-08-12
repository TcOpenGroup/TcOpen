using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Security;
using System.Linq;
using System.Threading;
using TcOpen.Inxton.Local.Security.Properties;
using System.Text;
using System.Security.Cryptography;
using TcOpen.Inxton.Local.Security;

namespace TcOpen.Inxton.Local.Security.Blazor
{
    public class BlazorRoleGroupManager : IAuthenticationService
    {
        private IRepository<GroupData> groupRepo;
        private IRepository<UserData> UserRepository;

        public List<Role> inAppRoleCollection { get; set; } = new List<Role>();

        public BlazorRoleGroupManager(IRepository<GroupData> groupRepo, IRepository<UserData> userRepo)
        {
            this.groupRepo = groupRepo;
            this.UserRepository = userRepo;
        }

        public IdentityResult CreateRole(Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            this.inAppRoleCollection.Add(role);
            return IdentityResult.Success;
        }

        public IdentityResult CreateGroup(string name)
        {
            if (name == null || name == "")
                throw new ArgumentNullException(nameof(name));

            try
            {
                var data = new GroupData(name);
                data._Created = DateTime.Now;
                groupRepo.Create(name, data);
            }
            catch (DuplicateIdException)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Group with name {name} already exists." });
            }

            TcoAppDomain.Current.Logger.Information($"New Group '{name}' created. {{@sender}}", new { Name = name });

            return IdentityResult.Success;
        }

        public IdentityResult DeleteGroup(string name)
        {
            if (name == null || name == "")
                throw new ArgumentNullException(nameof(name));

            groupRepo.Delete(name);

            TcoAppDomain.Current.Logger.Information($"Group '{name}' deleted. {{@sender}}", new { Name = name });

            return IdentityResult.Success;
        }

        public IdentityResult AddRoleToGroup(string group, string role)
        {
            if (group == null || group == "")
                throw new ArgumentNullException(nameof(group));
            if (role == null || role == "")
                throw new ArgumentNullException(nameof(role));

            try
            {
                GroupData data = null;
                data = groupRepo.Read(group);
                if (data != null)
                {
                    data.Roles.Add(role);
                    data._Modified = DateTime.Now;
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." });
                }

                groupRepo.Update(group, data);
            }
            catch (UnableToLocateRecordId)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." });
            }

            return IdentityResult.Success;
        }

        public IdentityResult AddRolesToGroup(string group, IEnumerable<string> roles)
        {
            if (group == null || group == "")
                throw new ArgumentNullException(nameof(group));
            if (roles == null)
                throw new ArgumentNullException(nameof(roles));

            try
            {
                GroupData data = null;
                data = groupRepo.Read(group);
                if (data != null)
                {
                    foreach (var role in roles)
                    {
                        data.Roles.Add(role);
                    }
                    data._Modified = DateTime.Now;
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." });
                }

                groupRepo.Update(group, data);
            }
            catch (UnableToLocateRecordId)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." });
            }

            TcoAppDomain.Current.Logger.Information($"Group '{group}' assign '{String.Join(",", roles)}'. {{@sender}}", new { Name = group, Roles = String.Join(",", roles) });

            return IdentityResult.Success;
        }

        public IdentityResult RemoveRolesFromGroup(string group, IEnumerable<string> roles)
        {
            if (group == null || group == "")
                throw new ArgumentNullException(nameof(group));
            if (roles == null)
                throw new ArgumentNullException(nameof(roles));

            try
            {
                GroupData data = null;
                data = groupRepo.Read(group);
                if (data != null)
                {
                    foreach (var role in roles)
                    {
                        data.Roles.Remove(role);
                    }
                    data._Modified = DateTime.Now;
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." });
                }

                TcoAppDomain.Current.Logger.Information($"Group '{group}' remove '{String.Join(",", roles)}'. {{@sender}}", new { Name = group, Roles = String.Join(",", roles) });

                groupRepo.Update(group, data);
            }
            catch (UnableToLocateRecordId)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." });
            }

            return IdentityResult.Success;
        }

        public List<string> GetRolesFromGroup(string group)
        {
            if (group == null || group == "")
                return null;

            GroupData data = null;

            try
            {
                if (!groupRepo.Exists(group))
                {
                    return null;
                }
                data = groupRepo.Read(group);
            }
            catch (UnableToLocateRecordId)
            {
                return null;
            }

            return new List<string>(data.Roles);
        }

        public string GetRolesFromGroupString(string group)
        {
            if (group == null || group == "")
                return null;

            GroupData data = null;

            try
            {
                if (!groupRepo.Exists(group))
                {
                    return null;
                }
                data = groupRepo.Read(group);
            }
            catch (UnableToLocateRecordId)
            {
                return null;
            }

            return String.Join(",", data.Roles);
        }

        public List<GroupData> GetAllGroup()
        {
            List<GroupData> data = null;
            data = groupRepo.GetRecords().ToList();
            return data;
        }


        //-------------------IMPLEMENTING INTERFACE---------------

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
            var newUser = new UserData(
                   "default",
                   string.Empty,
                   string.Empty,
                   new string[] { "Administrator" }, "Administrator", string.Empty);
            newUser.CanUserChangePassword = true;
            UserRepository.Create(newUser.Username, newUser);
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
                customPrincipal.Identity = new AppIdentity.AnonymousIdentity();
                UserAccessor.Instance.Identity = customPrincipal.Identity;
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