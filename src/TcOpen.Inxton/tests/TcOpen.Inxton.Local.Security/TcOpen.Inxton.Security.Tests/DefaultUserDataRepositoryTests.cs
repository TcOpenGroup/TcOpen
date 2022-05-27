using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Reflection;
using TcOpen.Inxton.Security;
using TcOpen.Inxton.Local.Security;

namespace TcOpen.Inxton.Security.Tests
{
    [TestFixture()]
    public class DefaultUserDataRepositoryTests
    {
        string userFolder;

        [OneTimeSetUp]
        public void Initialize()
        {
            userFolder = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName, "users");
            if(System.IO.Directory.Exists(userFolder)) System.IO.Directory.Delete(userFolder, true);
            _userData = new DefaultUserDataRepository<UserData>(userFolder);
        }

        DefaultUserDataRepository<UserData> _userData;

        [Test()]
        public void DefaultUserDataRepositoryTest()
        {
            Assert.IsNotNull(_userData);
        }

        [Test()]
        public void CreateTest()
        {
            //Arrange
            var expected = new UserData("Jano", "JanoveHeslo", new string[] { "Johny" });

            //Act
            _userData.Create(expected.Username, expected);

            //Assert
            Assert.AreEqual(1, _userData.Queryable.Where(p => p.Username == expected.Username).Count());
        }

        [Test()]
        public void ReadTest()
        {
            //Arrange
            var expected = new UserData("Fero", "FeroveHeslo", new string[] { "Frenky" });
            _userData.Create(expected.Username, expected);
            
            //Act
            var actual = _userData.Read(expected.Username);

            //Assert         
            Assert.AreEqual(expected.Roles[0], actual.Roles[0]);
        }

        [Test()]
        public void UpdateTest()
        {
            //Arrange
            var expected = new UserData("Juro", "JuroveHeslo", new string[] { "Giorgio" });            
            _userData.Create(expected.Username, expected);
            expected.Roles.Add("new role");
            //Act
            _userData.Update(expected._EntityId, expected);

            //Assert         
            Assert.AreEqual(expected.Roles[1], _userData.Queryable.Where(p => p._EntityId == expected._EntityId).FirstOrDefault().Roles[1]);
        }

        [Test()]
        public void DeleteTest()
        {
            //Arrange
            var expected = new UserData("Jaro", "JuroveHeslo", new string[] { "Giorgio" });
            _userData.Create(expected.Username, expected); 
            
            //Act
            _userData.Delete(expected._EntityId);

            //Assert         
            Assert.IsFalse(_userData.Queryable.Where(p => p._EntityId == expected._EntityId).Any(p => p.Username == expected.Username));
        }
    }
}