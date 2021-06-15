using NUnit.Framework;
using System.Linq;
using TcOpen.Inxton.Abstractions.Security;
using TcOpen.Inxton.Security;

namespace Vortex.SecurityTests
{
    [TestFixture]
    public class AuthorizationCheckerTests
    {

        [OneTimeSetUp]
        public void SetUp()
        {
            TcOpen.Inxton.Security.SecurityManager.CreateDefault();
            NUnit.Framework.Internal.TestExecutionContext.CurrentContext.CurrentPrincipal = SecurityManager.Manager.Principal;

            var authService = TcOpen.Inxton.Security.SecurityManager.Manager.Service as AuthenticationService;

            var records = authService.UserRepository.GetRecords("*").Select(p => p._EntityId);

            foreach (var item in records.ToList())
            {
                authService.UserRepository.Delete(item);
            }

            var userName = "Admin";
            var password = "AdminPassword";
            var roles = new string[] { "Administrator" };
            authService.UserRepository.Create(userName, new UserData(userName, password, roles.ToList())
);

            userName = "Operator";
            password = "OperatorPassword";
            roles = new string[] { "Operator" };

            authService.UserRepository.Create(userName, new UserData(userName, password, roles.ToList())
);
        }

        [Test]
        public void authentication_checker_passed()
        {
            SecurityProvider.Get.AuthenticationService.AuthenticateUser("Admin", "AdminPassword");
            Assert.AreEqual(true, AuthorizationChecker.HasAuthorization("Administrator"));
        }

        [Test]
        public void authentication_checker_failed()
        {
            SecurityProvider.Get.AuthenticationService.AuthenticateUser("Operator", "OperatorPassword");
            Assert.AreEqual(false, AuthorizationChecker.HasAuthorization("Administrator"));
        }

        [Test]
        public void authentication_checker_non_authenticated()
        {
            SecurityProvider.Get.AuthenticationService.AuthenticateUser("Admin", "AdminPassword");
            SecurityProvider.Get.AuthenticationService.DeAuthenticateCurrentUser();
            Assert.AreEqual(false, AuthorizationChecker.HasAuthorization("Administrator"));
        }

        [Test]
        public void authentication_checker_re_authenticate_success()
        {            
            SecurityProvider.Get.AuthenticationService.DeAuthenticateCurrentUser();
            Assert.AreEqual(true, AuthorizationChecker.HasAuthorization("Administrator", 
                () => SecurityProvider.Get.AuthenticationService.AuthenticateUser("Admin", "AdminPassword")));
        }

        [Test]
        public void authentication_checker_re_authenticate_failed()
        {
            SecurityProvider.Get.AuthenticationService.DeAuthenticateCurrentUser();
            Assert.AreEqual(false, AuthorizationChecker.HasAuthorization("Administrator",
                () => SecurityProvider.Get.AuthenticationService.AuthenticateUser("Admin", "AdminPassword1")));
        }
    }
}
