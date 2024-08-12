using System;
using NUnit.Framework;
using TcOpen.Inxton.Local.Security;
using TcOpen.Inxton.Local.Security.LDAP;

namespace TcOpen.Inxton.Security.LDAP.Tests
{
    [TestFixture]
    public class LdapAuthServiceTests
    {
        IAuthenticationService authService;

        [OneTimeSetUp]
        public void SetUp()
        {
            SecurityManager.Create(
                new LdapService(
                    new LdapConfig("ldap.forumsys.com", 389, false, "dc=example,dc=com")
                )
            );
            NUnit.Framework.Internal.TestExecutionContext.CurrentContext.CurrentPrincipal =
                SecurityManager.Manager.Principal;

            if (authService == null)
            {
                authService = SecurityProvider.Get.AuthenticationService as LdapService;
                authService.OnUserAuthenticateFailed += AuthService_OnUserAuthenticateFailed;
                authService.OnUserAuthenticateSuccess += AuthService_OnUserAuthenticateSuccess;
                authService.OnDeAuthenticated += AuthService_OnUserDeAuthenticated;
                try
                {
                    var actual = authService.AuthenticateUser("", "");
                }
                catch (Exception)
                {
                    // Swallow;
                }
            }
        }

        private string AuthService_OnUserDeAuthenticatedMessage;

        private void AuthService_OnUserDeAuthenticated(string username)
        {
            AuthService_OnUserDeAuthenticatedMessage = $"Deauth {username}";
        }

        private string AuthService_OnUserAuthenticateSuccessMessage;

        private void AuthService_OnUserAuthenticateSuccess(string username)
        {
            AuthService_OnUserAuthenticateSuccessMessage = $"Success {username}";
        }

        private string AuthService_OnUserAuthenticateFailedMessage;

        private void AuthService_OnUserAuthenticateFailed(string username)
        {
            AuthService_OnUserAuthenticateFailedMessage = $"Failed {username}";
        }

        [Test()]
        public void AuthenticationServiceTest()
        {
            //-- Assert
            Assert.IsNotNull(authService);
        }

        [Test()]
        public void AuthenticateUserSuccessTest()
        {
            //-- Arrange
            var userName = "uid=tesla,dc=example,dc=com";
            var password = "password";
            (authService as LdapService).CreateUserOnBound = (username, connection) =>
                new LdapUser { UserName = username };

            //-- Act
            var actual = authService.AuthenticateUser(userName, password);

            //-- Assert
            Assert.AreEqual(userName, actual.UserName);
            Assert.AreEqual($"Success {userName}", AuthService_OnUserAuthenticateSuccessMessage);
        }

        [Test()]
        public void AuthenticateUserFailedDuePasswordTest()
        {
            //-- Arrange
            var userName = "uid=tesla,dc=example,dc=com";
            var password = "passwordToAuthFailedDuePassword";
            var roles = new string[] { "Tester" };

            //-- Assert
            Assert.Throws(
                typeof(System.UnauthorizedAccessException),
                () => authService.AuthenticateUser(userName, "wrongPassword")
            );
            Assert.AreEqual($"Failed {userName}", AuthService_OnUserAuthenticateFailedMessage);
        }

        [Test()]
        public void DeAuthenticateCurrentUserTest()
        {
            //-- Arrange
            //-- Arrange
            var userName = "uid=tesla,dc=example,dc=com";
            var password = "password";
            (authService as LdapService).CreateUserOnBound = (username, connection) =>
                new LdapUser { UserName = username };
            var actual = authService.AuthenticateUser(userName, password);

            //-- Act
            authService.DeAuthenticateCurrentUser();

            //-- Assert
            Assert.AreEqual($"Deauth {userName}", AuthService_OnUserDeAuthenticatedMessage);
        }
    }
}
