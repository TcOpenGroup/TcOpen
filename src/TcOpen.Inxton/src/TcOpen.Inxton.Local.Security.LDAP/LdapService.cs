using Novell.Directory.Ldap;
using System;
using System.Linq;
using System.Threading;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security.LDAP
{
    /// <summary>
    /// Service for Security manager to authenicate via LDAP.
    /// This service is intendted to be used when your whole user management is delegated to another service 
    /// like Active Directory, Azure Activy Directory or other LDAP based system.    /// 
    /// Usage:
    /// <code>
    ///  SecurityManager.Create(new LdapService(new LdapConfig(
    ///         host: "yourLDAPdomain.com",
    ///         port: 636,
    ///         useSsl: true,
    ///         searchBase: "OU=AADDC Users,DC=testldap,DC=com"
    ///         )));
    /// </code>
    /// See also <seealso cref="LDAP.LdapConfig"/> for more info about config.
    /// </summary>
    public class LdapService : IAuthenticationService
    {
        public LdapConfig LdapConfig { get; }

        public OnTimedLogoutRequestDelegate OnTimedLogoutRequest { get; set; }
        public IExternalAuthorization ExternalAuthorization { get; set; }

        public event OnUserAuthentication OnUserAuthenticateSuccess;
        public event OnUserAuthentication OnUserAuthenticateFailed;
        public event OnUserAuthentication OnDeAuthenticating;
        public event OnUserAuthentication OnDeAuthenticated;

        /// <summary>
        /// This function is used if it's not possible to create a user from LDAP response.
        /// When this function is invoked, username and ldapconnection are passed through to create a new user.
        /// Use the search method of ldapconnection to query for user data.
        /// </summary>
        public Func<string, ILdapConnection, IUser> CreateUserOnBound;

        public LdapService(LdapConfig LdapConfig)
        {
            this.LdapConfig = LdapConfig;
        }

        public IUser AuthenticateUser(string username, string password)
        {
            using (var ldapConnection = new LdapConnection() { SecureSocketLayer = LdapConfig.UseSsl })
            {
                ConnectAndBind(ldapConnection, username, password);
                if (ldapConnection.Connected && ldapConnection.Bound)
                {
                    //over here is user authorized. All I have to do is to create an IUser from the DB
                    var user = TryToCreateActiveDirectoryUser(username, ldapConnection) ?? CreateUserOnBound(username, ldapConnection);
                    ldapConnection.Disconnect();
                    SetPrincipal(user);
                    OnUserAuthenticateSuccess(username);
                    return user;
                }
                else
                {
                    ldapConnection.Disconnect();
                    OnUserAuthenticateFailed(username);
                    throw new UnauthorizedAccessException("Cannot bind, verify LDAP credentials");
                }
            }
        }
        private void ConnectAndBind(ILdapConnection ldapConnection, string username, string password)
        {
            try
            {
                ldapConnection.Connect(LdapConfig.Host, LdapConfig.Port);
                ldapConnection.Bind(username, password);
            }
            catch (LdapException e)
            {
                OnUserAuthenticateFailed(username);
                throw new UnauthorizedAccessException("Cannot connect or bind, verify LDAP credentials,host,port.", e);
            }
        }
        private IUser TryToCreateActiveDirectoryUser(string username, LdapConnection ldapConnection)
        {
            try
            {
                var userEntry = SearchForUserEntry(username, ldapConnection);
                return CreateUserFromEntity(userEntry);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private LdapEntry SearchForUserEntry(string username, ILdapConnection ldapConnection)
        {
            var filter = $"{FILTER_ATTRIBUTE_EMAIL}={username}";
            var filterAttrs = new string[] { FILTER_ATTRIBUTE_USERNAME, EMAIL_ATTRIBUTE, NAME_ATTRIBUTE, GIVEN_NAME_ATTRIBUTE, LAST_NAME_ATTRIBUTE, MEMBER_OF_ATTRIBUTE };
            var entities = ldapConnection.Search(LdapConfig.SearchBase, LdapConnection.SCOPE_SUB, $"({filter})", filterAttrs, false);
            entities.HasMore();// Documentation claims LdapConnection.Search is synchronous, but it isn't.  https://github.com/dsbenghe/Novell.Directory.Ldap.NETStandard/issues/55
            if (entities.Count != 1)
            {
                var errMsg = $"LDAP entities.Count == {entities.Count}. <> 1 !!! Filter: \n {filter} \n {string.Join(" ", filterAttrs)}";
                throw new Exception(errMsg);
            }
            return entities.Next();
        }

        private IUser CreateUserFromEntity(LdapEntry userEntry)
        {
            var memberOf = userEntry.getAttribute(MEMBER_OF_ATTRIBUTE)?.StringValueArray.SelectMany(x => x.Split(';').Select(y => y.Split(',').First().Split('=').Last())).ToArray();
            var username = userEntry.getAttribute(FILTER_ATTRIBUTE_USERNAME)?.StringValue;
            var email = userEntry.getAttribute(EMAIL_ATTRIBUTE)?.StringValue;
            return new LdapUser { UserName = username, Roles = memberOf };
        }
        private void SetPrincipal(IUser ldapUser)
        {
            AppIdentity.AppPrincipal customPrincipal = (Thread.CurrentPrincipal as AppIdentity.AppPrincipal) ?? throw new ArgumentException();
            //Authenticate the user
            customPrincipal.Identity = new AppIdentity(ldapUser.UserName, ldapUser.Email, ldapUser.Roles, ldapUser.CanUserChangePassword, ldapUser.Level);
            UserAccessor.Instance.Identity = customPrincipal.Identity;
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
            }
        }
        public string CalculateHash(string clearTextPassword, string salt)
            => throw new NotImplementedException();


        public void ChangePassword(string userName, string password, string newPassword1, string newPassword2)
            => throw new NotImplementedException();

        public bool HasAuthorization(string roles, Action notAuthorizedAction = null)
        {
            return AuthorizationChecker.HasAuthorization(roles, notAuthorizedAction);
        }

        const string FILTER_ATTRIBUTE_USERNAME = "sAMAccountName";
        const string FILTER_ATTRIBUTE_EMAIL = "userPrincipalName";
        const string EMAIL_ATTRIBUTE = "userPrincipalName";
        const string NAME_ATTRIBUTE = "name";
        const string GIVEN_NAME_ATTRIBUTE = "givenName";
        const string LAST_NAME_ATTRIBUTE = "sn";
        const string MEMBER_OF_ATTRIBUTE = "memberOf";
    }


}
