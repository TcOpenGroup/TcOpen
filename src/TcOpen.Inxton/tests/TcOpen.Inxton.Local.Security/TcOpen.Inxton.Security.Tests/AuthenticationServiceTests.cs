using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using TcOpen.Inxton.Local.Security;
using TcOpen.Inxton.Security;
using static TcOpen.Inxton.Local.Security.AppIdentity;

namespace TcOpen.Inxton.Security.Tests
{
    [TestFixture()]
    public class AuthenticationServiceTests
    {
        string outputDir = Path.Combine(
            new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName,
            "SecurityManager"
        );

        AuthenticationService authService;

        [OneTimeSetUp]
        public void SetUp()
        {
            TcOpen.Inxton.Local.Security.SecurityManager.CreateDefault();
            NUnit.Framework.Internal.TestExecutionContext.CurrentContext.CurrentPrincipal =
                SecurityManager.Manager.Principal;

            if (authService == null)
            {
                authService = SecurityProvider.Get.AuthenticationService as AuthenticationService;
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
                var users = authService
                    .UserRepository.Queryable.Where(p => true)
                    .Select(p => p.Username)
                    .ToList();

                foreach (var item in users)
                {
                    authService.UserRepository.Delete(item);
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
            var userName = "TestUserToAuthSuccess";
            var password = "passwordToAuthSuccess";
            var roles = new string[] { "Tester" };

            authService.UserRepository.Create(
                userName,
                new UserData(userName, password, roles.ToList())
            );

            //-- Act
            var actual = authService.AuthenticateUser(userName, password);

            //-- Assert
            Assert.AreEqual(userName, actual.UserName);
            Assert.AreEqual(1, roles.Length);
            Assert.AreEqual(roles[0], actual.Roles[0]);
            Assert.AreEqual($"Success {userName}", AuthService_OnUserAuthenticateSuccessMessage);
        }

        [Test()]
        public void AuthenticateUserFailedDuePasswordTest()
        {
            //-- Arrange
            var userName = "TestUserToAuthFailedDuePassword";
            var password = "passwordToAuthFailedDuePassword";
            var roles = new string[] { "Tester" };

            authService.UserRepository.Create(
                userName,
                new UserData(userName, password, roles.ToList())
            );

            //-- Assert
            Assert.Throws(
                typeof(System.UnauthorizedAccessException),
                () => authService.AuthenticateUser(userName, "wrongPassword")
            );
            Assert.AreEqual($"Failed {userName}", AuthService_OnUserAuthenticateFailedMessage);
        }

        [Test()]
        public void AuthenticateUserFailedDueUserNameTest()
        {
            //-- Arrange
            var userName = "TestUserToAuthFailedDueUserName";
            var password = "passwordToAuthFailedDueUserName";
            var roles = new string[] { "Tester" };

            authService.UserRepository.Create(
                userName,
                new UserData(userName, password, roles.ToList())
            );

            //-- Assert
            Assert.Throws(
                typeof(System.UnauthorizedAccessException),
                () => authService.AuthenticateUser("WrongUserName", password)
            );
            Assert.AreEqual($"Failed WrongUserName", AuthService_OnUserAuthenticateFailedMessage);
        }

        [Test()]
        public void AuthenticateUserFailedDueUserNameAndPasswordTest()
        {
            //-- Arrange
            var userName = "TestUserToAuthFailedDueUserAndPasswordName";
            var password = "passwordToAuthFailedDueUserAndPasswordName";
            var roles = new string[] { "Tester" };

            authService.UserRepository.Create(
                userName,
                new UserData(userName, password, roles.ToList())
            );

            //-- Assert
            Assert.Throws(
                typeof(System.UnauthorizedAccessException),
                () => authService.AuthenticateUser("WrongUserNameAndPassword", "WrongPassword")
            );
            Assert.AreEqual(
                $"Failed WrongUserNameAndPassword",
                AuthService_OnUserAuthenticateFailedMessage
            );
        }

        [Test()]
        public void AuthenticateUserFailedDueAdditionalRole()
        {
            //-- Arrange
            var userName = "AuthenticateUserFailedDueAdditionalRole";
            var password = "AuthenticateUserFailedDueAdditionalRole";
            var roles = new string[] { "AuthenticateUserFailedDueNotMatchingRolesHash" };
            var userToTest = new UserData(userName, password, roles.ToList());
            userToTest.Roles.Add("RoleThatThisUserShouldNotHave");
            authService.UserRepository.Create(userName, userToTest);

            //-- Assert
            Assert.Throws(
                typeof(System.UnauthorizedAccessException),
                () =>
                    authService.AuthenticateUser(
                        "WrongUserNameAndPassword",
                        "AuthenticateUserFailedDueNotMatchingRolesHash"
                    )
            );
            Assert.AreEqual(
                $"Failed WrongUserNameAndPassword",
                AuthService_OnUserAuthenticateFailedMessage
            );
        }

        [Test()]
        public void AuthenticateUserFailedDueNotMatchingRoleChecksum()
        {
            //-- Arrange
            var userName = "AuthenticateUserFailedDueNotMatchingRolesHash";
            var password = "AuthenticateUserFailedDueNotMatchingRolesHash";
            var roles = new string[] { "AuthenticateUserFailedDueNotMatchingRolesHash" };
            var userToTest = new UserData(userName, password, roles.ToList());
            userToTest.RoleHash =
                "QXV0aGVudGljYXRlVXNlckZhaWxlZER1ZU5vdE1hdGNoaW5nUm9sZUNoZWNrc3Vt";

            authService.UserRepository.Create(userName, userToTest);

            //-- Assert
            Assert.Throws(
                typeof(System.UnauthorizedAccessException),
                () =>
                    authService.AuthenticateUser(
                        "WrongUserNameAndPassword",
                        "AuthenticateUserFailedDueNotMatchingRolesHash"
                    )
            );
            Assert.AreEqual(
                $"Failed WrongUserNameAndPassword",
                AuthService_OnUserAuthenticateFailedMessage
            );
        }

        [Test()]
        public void RoleHashIsCalculatedProperly()
        {
            //-- Arrange
            var userName = "TestUser";
            var password = "TestUser";
            var roles = new string[] { "TestRole1", "TestRole2" };
            var userToTest = new UserData(userName, password, roles.ToList());

            Assert.AreEqual(userToTest.RoleHash, "zhsvnkB9I8CImR/UKOXEfHb0oAQ1Az2pNQegv66gXuQ=");
        }

        [Test()]
        public void RoleHashIsNotDependedOnRoleOrder()
        {
            //-- Arrange
            var userName = "TestUser";
            var password = "TestUser";
            var roles = new string[] { "TestRole2", "TestRole1" };
            var userToTest = new UserData(userName, password, roles.ToList());

            Assert.AreEqual(userToTest.RoleHash, "zhsvnkB9I8CImR/UKOXEfHb0oAQ1Az2pNQegv66gXuQ=");
        }

        [Test()]
        public void RoleHashIsCalculatedProperlyAfterAddingNewRole()
        {
            //-- Arrange
            var userName = "TestUser";
            var password = "test";
            var roles = new List<String>() { "TestRole1" };
            var userToTest = new UserData(userName, password, roles.ToList());
            userToTest.Roles.Add("TestRole2");
            userToTest.UpdateRoleHash();

            Assert.AreEqual(userToTest.RoleHash, "zhsvnkB9I8CImR/UKOXEfHb0oAQ1Az2pNQegv66gXuQ=");
        }

        [Test()]
        public void DeAuthenticateCurrentUserTest()
        {
            //-- Arrange
            var userName = "TestUserToAuthSuccess";
            var password = "passwordToAuthSuccess";
            var actual = authService.AuthenticateUser(userName, password);

            //-- Act
            authService.DeAuthenticateCurrentUser();

            //-- Assert
            Assert.AreEqual(
                $"Deauth TestUserToAuthSuccess",
                AuthService_OnUserDeAuthenticatedMessage
            );
        }

        [Test()]
        public void CalculateHashTest()
        {
            //-- Arrange
            var password = "faklshdfkahklsdhfalskdhf43r592q64ytgfsaefjd";

            //-- Act
            var actual = authService.CalculateHash(password, "UserName");

            Assert.AreEqual("v2YFX9RZrFMag03gF7g8vHnY5XLHhEySVyhkqRkWItU=", actual);
        }

        [Test()]
        public void ChangePasswordTest()
        {
            //-- Arrange
            var userName = "TestUserToPwdChange";
            var password = "passwordToChange";
            var newPassword = "newpassword";
            var roles = new string[] { "Tester" };

            authService.UserRepository.Create(
                userName,
                new UserData(userName, password, roles.ToList())
            );

            var actual = authService.AuthenticateUser(userName, password);

            Assert.AreEqual(userName, actual.UserName);
            Assert.AreEqual(1, roles.Length);
            Assert.AreEqual(roles[0], actual.Roles[0]);

            authService.DeAuthenticateCurrentUser();

            //-- Act

            authService.ChangePassword(userName, password, newPassword, newPassword);
            var changed = authService.AuthenticateUser(userName, newPassword);

            //-- Assert

            Assert.AreEqual(userName, changed.UserName);
            Assert.AreEqual(1, roles.Length);
            Assert.AreEqual(roles[0], changed.Roles[0]);
        }

        [Test()]
        public void ChangePasswordFailMissedAuthenticationTest()
        {
            //-- Arrange
            var userName = "TestUserToPwdChangeFailedAuthorisation";
            var password = "passwordToChange";
            var newPassword = "newpassword";
            var roles = new string[] { "Tester" };

            authService.UserRepository.Create(
                userName,
                new UserData(userName, password, roles.ToList())
            );

            var actual = authService.AuthenticateUser(userName, password);

            Assert.AreEqual(userName, actual.UserName);
            Assert.AreEqual(1, roles.Length);
            Assert.AreEqual(roles[0], actual.Roles[0]);

            authService.DeAuthenticateCurrentUser();

            //-- Act / Assert

            Assert.Throws(
                typeof(System.UnauthorizedAccessException),
                () => authService.ChangePassword(userName, password + "a", newPassword, newPassword)
            );
        }

        [Test()]
        public void ChangePasswordFailPasswordsDoNotMatch()
        {
            //-- Arrange
            var userName = "TestUserToPwdChangePasswordDoNotMatch";
            var password = "passwordToChange";
            var newPassword = "newpassword";
            var roles = new string[] { "Tester" };

            authService.UserRepository.Create(
                userName,
                new UserData(userName, password, roles.ToList())
            );

            var actual = authService.AuthenticateUser(userName, password);

            Assert.AreEqual(userName, actual.UserName);
            Assert.AreEqual(1, roles.Length);
            Assert.AreEqual(roles[0], actual.Roles[0]);

            authService.DeAuthenticateCurrentUser();

            //-- Act / Assert

            Assert.Throws(
                typeof(PasswordsDoNotMatchException),
                () => authService.ChangePassword(userName, password, newPassword, newPassword + "b")
            );
        }

        [Test()]
        public void AuthenticateWithToken()
        {
            //-- Arrange
            var userName = "UserTokenAuthentication";
            var password = "token";
            var roles = new string[] { "Tester" };
            var token = "usersToken";

            authService.ExternalAuthorization = new ExternalAuthenticator() { Token = token };

            authService.UserRepository.Create(
                userName,
                new UserData(userName, password, roles.ToList())
            );

            authService.AuthenticateUser(userName, password);

            authService.ExternalAuthorization.RequestTokenChange(token);

            authService.DeAuthenticateCurrentUser();

            var actual = authService.ExternalAuthorization.RequestAuthorization(token);

            Assert.AreEqual(userName, actual.UserName);
            Assert.AreEqual(1, roles.Length);
            Assert.AreEqual(roles[0], actual.Roles[0]);
        }

        [Test()]
        public void DeAuthenticateWithToken()
        {
            //-- Arrange
            var userName = "UserTokenAuthenticationDeauthWithToken";
            var password = "token";
            var roles = new string[] { "Tester" };
            var token = "usersToken-UserTokenAuthenticationDeauthWithToken";

            authService.ExternalAuthorization = new ExternalAuthenticator() { Token = token };

            authService.UserRepository.Create(
                userName,
                new UserData(userName, password, roles.ToList())
            );

            authService.AuthenticateUser(userName, password);

            authService.ExternalAuthorization.RequestTokenChange(token);

            authService.DeAuthenticateCurrentUser();

            var actual = authService.ExternalAuthorization.RequestAuthorization(token);

            Assert.AreEqual(userName, actual.UserName);
            Assert.AreEqual(1, roles.Length);
            Assert.AreEqual(roles[0], actual.Roles[0]);

            actual = authService.ExternalAuthorization.RequestAuthorization(token);

            Assert.IsNull(actual);
            Assert.AreEqual(
                typeof(AnonymousIdentity),
                TcOpen.Inxton.Local.Security.SecurityManager.Manager.Principal.Identity.GetType()
            );
        }

        [Test()]
        public void AuthenticateWithInexistingToken()
        {
            //-- Arrange
            var userName = "UserUnknownTokenAuthentication";
            var password = "token";
            var roles = new string[] { "Tester" };
            var token = "halabala";

            var externalAuthorization = new ExternalAuthenticator() { Token = token };

            authService.ExternalAuthorization = externalAuthorization;

            authService.UserRepository.Create(
                userName,
                new UserData(userName, password, roles.ToList())
            );

            authService.AuthenticateUser(userName, password);

            authService.ExternalAuthorization.RequestTokenChange(token);

            authService.DeAuthenticateCurrentUser();

            var inexistingToken = "fjalsdjl";

            externalAuthorization.Token = inexistingToken;

            authService.ExternalAuthorization.RequestAuthorization(inexistingToken);

            AppPrincipal customPrincipal = Thread.CurrentPrincipal as AppPrincipal;

            Assert.AreEqual(string.Empty, customPrincipal.Identity.Name);
            Assert.AreEqual(0, customPrincipal.Identity.Roles.Length);
            Assert.AreEqual(string.Empty, customPrincipal.Identity.Level);
        }

        [Test()]
        public void AuthenticateWithEmptyToken()
        {
            //-- Arrange
            var userName = "UserEmptyTokenAuthentication";
            var password = "token";
            var roles = new string[] { "Tester" };
            var token = string.Empty;

            var externalAuthorization = new ExternalAuthenticator() { Token = token };

            authService.ExternalAuthorization = externalAuthorization;

            authService.UserRepository.Create(
                userName,
                new UserData(userName, password, roles.ToList())
            );

            authService.AuthenticateUser(userName, password);

            authService.ExternalAuthorization.RequestTokenChange(token);

            authService.DeAuthenticateCurrentUser();

            externalAuthorization.Token = string.Empty;

            authService.ExternalAuthorization.RequestAuthorization(token);

            AppPrincipal customPrincipal = Thread.CurrentPrincipal as AppPrincipal;

            Assert.AreEqual(string.Empty, customPrincipal.Identity.Name);
            Assert.AreEqual(0, customPrincipal.Identity.Roles.Length);
            Assert.AreEqual(string.Empty, customPrincipal.Identity.Level);
        }

        [Test()]
        public void AddExistingToken()
        {
            var token = "sameToken";
            var externalAuthorization = new ExternalAuthenticator() { Token = token };

            authService.ExternalAuthorization = externalAuthorization;

            authService.UserRepository.Create(
                "ExistingToken1",
                new UserData("ExistingToken1", "halabala", new string[] { "Tester" })
            );
            authService.UserRepository.Create(
                "ExistingToken2",
                new UserData("ExistingToken2", "halabala", new string[] { "Tester" })
            );

            authService.AuthenticateUser("ExistingToken1", "halabala");

            authService.ExternalAuthorization.RequestTokenChange(token);

            authService.DeAuthenticateCurrentUser();

            authService.AuthenticateUser("ExistingToken2", "halabala");

            Assert.Throws(
                typeof(ExistingTokenException),
                () => authService.ExternalAuthorization.RequestTokenChange(token)
            );
        }

        public class ExternalAuthenticator : ExternalAuthorization
        {
            public string Token { get; set; }
        }
    }
}
