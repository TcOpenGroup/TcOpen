using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using TcOpen.Inxton.Abstractions.Security;

namespace TcOpen.Inxton.Security.Tests
{

    [TestFixture()]
    public class SecurityManagerTests
    {
        string outputDir = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName, "SecurityManager");
        [OneTimeSetUp]
        public void SetUp()
        {
            SecurityManager.CreateDefault();
            NUnit.Framework.Internal.TestExecutionContext.CurrentContext.CurrentPrincipal = SecurityManager.Manager.Principal;

            var authService = SecurityManager.Manager.Service as AuthenticationService;

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


        [Test()]
        public void CreateTest()
        {                    
            //-- Assert
            Assert.IsNotNull(SecurityManager.Manager);
        }
#if NETFRAMEWORK
        [Test()]
        public void AuthorizeAdministratorAndAccessObject()
        {
            //-- Arrange
            TcOpen.Inxton.Security.SecurityManager.Manager.Service.AuthenticateUser("Admin", "AdminPassword");

            //-- Act
            var actual = new AdminAccessTestClass();

            //-- Assert
            Assert.IsNotNull(actual);
        }

        [Test()]
        public void AuthorizeOperatorAndDenyObject()
        {
            //-- Arrange
            TcOpen.Inxton.Security.SecurityManager.Manager.Service.AuthenticateUser("Operator", "OperatorPassword");           

            //-- Assert
            Assert.Throws(typeof(System.Security.SecurityException), () => new AdminAccessTestClass());
        }

#endif        
    }

    [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
    public class AdminAccessTestClass
    {

    }
}