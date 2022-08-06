using System;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace TcOpen.Inxton.Local.Security.Blazor.Tests
{
    public class UserStoreTests : IClassFixture<BlazorSecurityTestsFixture>
    {
        private readonly BlazorSecurityTestsFixture _fixture;

        public UserStoreTests(BlazorSecurityTestsFixture fixture)
        {
            this._fixture = fixture;
        }

        //CreateAsync
        [Fact]
        public async Task CreateAsync_Success()
        {
            //Arrange
            //Act
            var result = await _fixture.UserStore.CreateAsync(_fixture.SeedData.CreateUser);
            //Assert
            var user = _fixture.Repository.UserRepository.Read("CREATE");
            Assert.NotNull(user);
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task CreateAsync_Exist_Failed()
        {
            //Arrange
            //Act
            var result = await _fixture.UserStore.CreateAsync(_fixture.SeedData.ExistUser);
            //Assert
            Assert.False(result.Succeeded);
        }

        [Fact]
        public async Task CreateAsync_Null_Failed()
        {
            //Arrange
            IdentityResult result = null;
            //Act
            try
            {
                result = await _fixture.UserStore.CreateAsync(null);
            }
            catch (ArgumentNullException)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //DeleteAsync
        [Fact]
        public async Task DeleteAsync_Success()
        {
            //Arrange
            UserData user = null;
            //Act
            var result = await _fixture.UserStore.DeleteAsync(_fixture.SeedData.RemoveUser);
            //Assert
            try
            {
                user = _fixture.Repository.UserRepository.Read(_fixture.SeedData.RemoveUser.Id);
            }
            catch (UnableToLocateRecordId)
            {
                Assert.Null(user);
            }
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task DeleteAsync_NoExist_Failed()
        {
            //Arrange
            UserData user = null;
            //Act
            var result = await _fixture.UserStore.DeleteAsync(_fixture.SeedData.NoExistUser);
            //Assert
            try
            {
                user = _fixture.Repository.UserRepository.Read(_fixture.SeedData.RemoveUser.Id);
            }
            catch (UnableToLocateRecordId)
            {
                Assert.Null(user);
            }
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task DeleteAsync_Null_Failed()
        {
            //Arrange
            IdentityResult result = null;
            //Act
            try
            {
                result = await _fixture.UserStore.DeleteAsync(null);
            }
            catch (ArgumentNullException)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //FindByIdAsync
        [Fact]
        public async Task FindByIdAsync_Success()
        {
            //Arrange
            //Act
            var user = await _fixture.UserStore.FindByIdAsync(_fixture.SeedData.ExistUser.Id);
            //Assert
            Assert.NotNull(user);
        }

        [Fact]
        public async Task FindByIdAsync_NoExist_Failed()
        {
            //Arrange
            //Act
            var user = await _fixture.UserStore.FindByIdAsync(_fixture.SeedData.NoExistUser.Id);
            //Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task FindByIdAsync_Null_Failed()
        {
            //Arrange
            User result = null;
            //Act
            try
            {
                result = await _fixture.UserStore.FindByIdAsync(null);
            }
            catch (ArgumentNullException)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task FindByIdAsync_Empty_Failed()
        {
            //Arrange
            User result = null;
            //Act
            try
            {
                result = await _fixture.UserStore.FindByIdAsync("");
            }
            catch (ArgumentNullException)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //FindByNameAsync
        [Fact]
        public async Task FindByNameAsync_Success()
        {
            //Arrange
            //Act
            var user = await _fixture.UserStore.FindByNameAsync(_fixture.SeedData.ExistUser.NormalizedUserName);
            //Assert
            Assert.NotNull(user);
        }

        [Fact]
        public async Task FindByNameAsync_NoExist_Failed()
        {
            //Arrange
            //Act
            var user = await _fixture.UserStore.FindByNameAsync(_fixture.SeedData.NoExistUser.NormalizedUserName);
            //Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task FindByNameAsync_Null_Failed()
        {
            //Arrange
            User result = null;
            //Act
            try
            {
                result = await _fixture.UserStore.FindByNameAsync(null);
            }
            catch (ArgumentNullException)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task FindByNameAsync_Empty_Failed()
        {
            //Arrange
            User result = null;
            //Act
            try
            {
                result = await _fixture.UserStore.FindByNameAsync("");
            }
            catch (ArgumentNullException)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //GetNormalizedUserNameAsync
        [Fact]
        public async Task GetNormalizedUserNameAsync_Success()
        {
            //Arrange
            var expectedUsername = _fixture.SeedData.ExistUser.NormalizedUserName;
            //Act
            var username = await _fixture.UserStore.GetNormalizedUserNameAsync(_fixture.SeedData.ExistUser);
            //Assert
            Assert.NotNull(username);
            Assert.Equal(expectedUsername, username);
        }

        [Fact]
        public async Task GetNormalizedUserNameAsync_Null_Failed()
        {
            //Arrange
            String result = null;
            //Act
            try
            {
                result = await _fixture.UserStore.GetNormalizedUserNameAsync(null);
            }
            catch (ArgumentNullException)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //GetPasswordHashAsync
        [Fact]
        public async Task GetPasswordHashAsync_Success()
        {
            //Arrange
            var expectedPassword = _fixture.SeedData.ExistUser.PasswordHash;
            //Act
            var password = await _fixture.UserStore.GetPasswordHashAsync(_fixture.SeedData.ExistUser);
            //Assert
            Assert.NotNull(password);
            Assert.Equal(expectedPassword, password);
        }

        [Fact]
        public async Task GetPasswordHashAsync_Null_Failed()
        {
            //Arrange
            String result = null;
            //Act
            try
            {
                result = await _fixture.UserStore.GetPasswordHashAsync(null);
            }
            catch (ArgumentNullException)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //GetRolesAsync
        [Fact]
        public async Task GetRolesAsync_Success()
        {
            //Arrange
            var expectedRoles = new List<String> { "Administrator" };
            //Act
            var roles = await _fixture.UserStore.GetRolesAsync(_fixture.SeedData.AdminUser);
            //Assert
            Assert.NotNull(roles);
            Assert.Equal(expectedRoles, roles);
        }

        [Fact]
        public async Task GetRolesAsync_NullRoles_Success()
        {
            //Arrange
            //Act
            var roles = await _fixture.UserStore.GetRolesAsync(_fixture.SeedData.ExistUser);
            //Assert
            Assert.Equal(new List<String>(), roles);
        }

        [Fact]
        public async Task GetRolesAsync_Null_Failed()
        {
            //Arrange
            IList<String> result = null;
            //Act
            try
            {
                result = await _fixture.UserStore.GetRolesAsync(null);
            }
            catch (ArgumentNullException)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //GetSecurityStampAsync
        [Fact]
        public async Task GetSecurityStampAsync_Success()
        {
            //Arrange
            var expectedSecurityStamp = _fixture.SeedData.ExistUser.SecurityStamp;
            //Act
            var securityStamp = await _fixture.UserStore.GetSecurityStampAsync(_fixture.SeedData.ExistUser);
            //Assert
            Assert.NotNull(securityStamp);
            Assert.Equal(expectedSecurityStamp, securityStamp);
        }

        [Fact]
        public async Task GetSecurityStampAsync_Null_Failed()
        {
            //Arrange
            string result = null;
            //Act
            try
            {
                result = await _fixture.UserStore.GetSecurityStampAsync(null);
            }
            catch (ArgumentNullException)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //GetUserIdAsync
        [Fact]
        public async Task GetUserIdAsync_Success()
        {
            //Arrange
            var expectedId = _fixture.SeedData.ExistUser.Id;
            //Act
            var id = await _fixture.UserStore.GetUserIdAsync(_fixture.SeedData.ExistUser);
            //Assert
            Assert.NotNull(id);
            Assert.Equal(expectedId, id);
        }

        [Fact]
        public async Task GetUserIdAsync_Null_Failed()
        {
            //Arrange
            string result = null;
            //Act
            try
            {
                result = await _fixture.UserStore.GetUserIdAsync(null);
            }
            catch (ArgumentNullException)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //GetUserNameAsync
        [Fact]
        public async Task GetUserNameAsync_Success()
        {
            //Arrange
            var expectedUsername = _fixture.SeedData.ExistUser.UserName;
            //Act
            var username = await _fixture.UserStore.GetUserNameAsync(_fixture.SeedData.ExistUser);
            //Assert
            Assert.NotNull(username);
            Assert.Equal(expectedUsername, username);
        }

        [Fact]
        public async Task GetUserNameAsync_Null_Failed()
        {
            //Arrange
            string result = null;
            //Act
            try
            {
                result = await _fixture.UserStore.GetUserNameAsync(null);
            }
            catch (ArgumentNullException)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //GetUsersInRoleAsync
        [Fact]
        public async Task GetUsersInRoleAsync_Success()
        {
            //Arrange
            var usersCount = 2;
            var normalizedRole = "ADMINISTRATOR";
            //Act
            var users = await _fixture.UserStore.GetUsersInRoleAsync(normalizedRole);
            //Assert
            Assert.NotNull(users);
            Assert.True(users.Count == usersCount);
            Assert.Contains(_fixture.SeedData.DefaultUser.UserName, users.Select(x => x.UserName));
        }

        [Fact]
        public async Task GetUsersInRoleAsync_NoExist_Failed()
        {
            //Arrange
            IList<User> users = null;
            //Act
            try
            {
                users = await _fixture.UserStore.GetUsersInRoleAsync("NOEXISTROLE");
            }
            catch (Exception)
            {
                Assert.Null(users);
            }
            //Assert
            Assert.Null(users);
        }

        [Fact]
        public async Task GetUsersInRoleAsync_Empty_Failed()
        {
            //Arrange
            IList<User> result = null;
            //Act
            try
            {
                result = await _fixture.UserStore.GetUsersInRoleAsync("");
            }
            catch (Exception)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUsersInRoleAsync_Null_Failed()
        {
            //Arrange
            IList<User> result = null;
            //Act
            try
            {
                result = await _fixture.UserStore.GetUsersInRoleAsync(null);
            }
            catch (Exception)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //HasPasswordAsync
        [Fact]
        public async Task HasPasswordAsync_Success()
        {
            //Arrange
            //Act
            var password = await _fixture.UserStore.HasPasswordAsync(_fixture.SeedData.ExistUser);
            //Assert
            Assert.True(password);
        }

        [Fact]
        public async Task HasPasswordAsync_Null_Failed()
        {
            //Arrange
            bool result = false;
            //Act
            try
            {
                result = await _fixture.UserStore.HasPasswordAsync(null);
            }
            catch (ArgumentNullException)
            {
                Assert.False(result);
            }
            //Assert
            Assert.False(result);
        }

        //IsInRoleAsync
        [Fact]
        public async Task IsInRoleAsync_Success()
        {
            //Arrange
            var normalizedRoleName = "DEFAULT";
            //Act
            var result = await _fixture.UserStore.IsInRoleAsync(_fixture.SeedData.DefaultUser, normalizedRoleName);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsInRoleAsync_NoExist_Failed()
        {
            //Arrange
            bool result = false;
            //Act
            try
            {
                result = await _fixture.UserStore.IsInRoleAsync(_fixture.SeedData.NoExistUser, "NOEXISTROLE");
            }
            catch (Exception)
            {
                Assert.False(result);
            }
            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task IsInRoleAsync_NullRoles_Success()
        {
            //Arrange
            bool result = false;
            //Act
            try
            {
                result = await _fixture.UserStore.IsInRoleAsync(_fixture.SeedData.ExistUser, "NOEXISTROLE");
            }
            catch (Exception)
            {
                Assert.False(result);
            }
            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task IsInRoleAsync_Empty_Failed()
        {
            //Arrange
            bool result = false;
            //Act
            try
            {
                result = await _fixture.UserStore.IsInRoleAsync(_fixture.SeedData.DefaultUser, "");
            }
            catch (ArgumentNullException)
            {
                Assert.False(result);
            }
            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task IsInRoleAsync_Null_Failed()
        {
            //Arrange
            bool result = false;
            //Act
            try
            {
                result = await _fixture.UserStore.IsInRoleAsync(_fixture.SeedData.DefaultUser, null);
            }
            catch (ArgumentNullException)
            {
                Assert.False(result);
            }
            //Assert
            Assert.False(result);
        }

        //SetNormalizedUserNameAsync
        [Fact]
        public async Task SetNormalizedUserNameAsync_Success()
        {
            //Arrange
            var normalizedName = "NEWNORMALIZEDNAME";
            //Act
            await _fixture.UserStore.SetNormalizedUserNameAsync(_fixture.SeedData.UpdateUser, normalizedName);
            //Assert
            Assert.Equal(normalizedName, _fixture.SeedData.UpdateUser.NormalizedUserName);
        }

        [Fact]
        public async Task SetNormalizedUserNameAsync_NoExist_Failed()
        {
            //Arrange
            //Act
            try
            {
                await _fixture.UserStore.SetNormalizedUserNameAsync(_fixture.SeedData.NoExistUser, "normalizedName");
            }
            catch (UnableToLocateRecordId)
            {
                Assert.True(true);
            }
            //Assert
        }

        [Fact]
        public async Task SetNormalizedUserNameAsync_Empty_Failed()
        {
            //Arrange
            //Act
            try
            {
                await _fixture.UserStore.SetNormalizedUserNameAsync(_fixture.SeedData.DefaultUser, "");
            }
            catch (ArgumentNullException)
            {
                Assert.True(true);
            }
            //Assert
        }

        [Fact]
        public async Task ISetNormalizedUserNameAsync_Null_Failed()
        {
            //Act
            try
            {
                await _fixture.UserStore.SetNormalizedUserNameAsync(_fixture.SeedData.DefaultUser, null);
            }
            catch (ArgumentNullException)
            {
                Assert.True(true);
            }
            //Assert
        }

        //SetPasswordHashAsync
        [Fact]
        public async Task SetPasswordHashAsync_Success()
        {
            //Arrange
            var password = "NEWPASSWORD";
            //Act
            await _fixture.UserStore.SetPasswordHashAsync(_fixture.SeedData.UpdateUser, password);
            //Assert
            Assert.Equal(password, _fixture.SeedData.UpdateUser.PasswordHash);
        }

        [Fact]
        public async Task SetPasswordHashAsync_NoExist_Failed()
        {
            //Arrange
            //Act
            try
            {
                await _fixture.UserStore.SetPasswordHashAsync(_fixture.SeedData.NoExistUser, "password");
            }
            catch (UnableToLocateRecordId)
            {
                Assert.True(true);
            }
            //Assert
        }

        [Fact]
        public async Task SetPasswordHashAsync_Empty_Failed()
        {
            //Arrange
            //Act
            try
            {
                await _fixture.UserStore.SetPasswordHashAsync(_fixture.SeedData.DefaultUser, "");
            }
            catch (ArgumentNullException)
            {
                Assert.True(true);
            }
            //Assert
        }

        [Fact]
        public async Task SetPasswordHashAsync_Null_Failed()
        {
            //Arrange
            //Act
            try
            {
                await _fixture.UserStore.SetPasswordHashAsync(_fixture.SeedData.DefaultUser, null);
            }
            catch (ArgumentNullException)
            {
                Assert.True(true);
            }
            //Assert
        }

        //SetSecurityStampAsync
        [Fact]
        public async Task SetSecurityStampAsync_Success()
        {
            //Arrange
            var stamp = Guid.NewGuid().ToString();
            //Act
            await _fixture.UserStore.SetSecurityStampAsync(_fixture.SeedData.UpdateUser, stamp);
            //Assert
            Assert.Equal(stamp, _fixture.SeedData.UpdateUser.SecurityStamp);
        }

        [Fact]
        public async Task SetSecurityStampAsync_Failed()
        {
            //Arrange
            //Act
            try
            {
                await _fixture.UserStore.SetSecurityStampAsync(_fixture.SeedData.NoExistUser, "securityStamp");
            }
            catch (UnableToLocateRecordId)
            {
                Assert.True(true);
            }
            //Assert
        }

        [Fact]
        public async Task SetSecurityStampAsync_Empty_Failed()
        {
            //Arrange
            //Act
            try
            {
                await _fixture.UserStore.SetSecurityStampAsync(_fixture.SeedData.DefaultUser, "");
            }
            catch (ArgumentNullException)
            {
                Assert.True(true);
            }
            //Assert
        }

        [Fact]
        public async Task SetSecurityStampAsync_Null_Failed()
        {
            //Arrange
            //Act
            try
            {
                await _fixture.UserStore.SetSecurityStampAsync(_fixture.SeedData.DefaultUser, null);
            }
            catch (ArgumentNullException)
            {
                Assert.True(true);
            }
            //Assert
        }

        //SetUserNameAsync
        [Fact]
        public async Task SetUserNameAsync_Success()
        {
            //Arrange
            var userName = "NEWUSERNAME";
            //Act
            await _fixture.UserStore.SetUserNameAsync(_fixture.SeedData.UpdateUser, userName);
            //Assert
            Assert.Equal(userName, _fixture.SeedData.UpdateUser.UserName);
        }

        [Fact]
        public async Task SetUserNameAsync_Failed()
        {
            //Arrange
            //Act
            try
            {
                await _fixture.UserStore.SetUserNameAsync(_fixture.SeedData.NoExistUser, "userName");
            }
            catch (UnableToLocateRecordId)
            {
                Assert.True(true);
            }
            //Assert
        }

        [Fact]
        public async Task SetUserNameAsync_Empty_Failed()
        {
            //Arrange
            //Act
            try
            {
                await _fixture.UserStore.SetUserNameAsync(_fixture.SeedData.DefaultUser, "");
            }
            catch (ArgumentNullException)
            {
                Assert.True(true);
            }
            //Assert
        }

        [Fact]
        public async Task SetUserNameAsync_Null_Failed()
        {
            //Arrange
            //Act
            try
            {
                await _fixture.UserStore.SetUserNameAsync(_fixture.SeedData.DefaultUser, null);
            }
            catch (ArgumentNullException)
            {
                Assert.True(true);
            }
            //Assert
        }

        //UpdateAsync
        [Fact]
        public async Task UpdateAsync_Success()
        {
            //Arrange
            var userData = _fixture.Repository.UserRepository.Read(_fixture.SeedData.UpdateUser.NormalizedUserName);
            var user = new User(userData);
            user.UserName = "newUpdate";
            user.Email = "newupdate@newupdate.com";
            user.PasswordHash = "password";
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.CanUserChangePassword = true;
            user.Roles = new string[] { "NEWROLE1", "NEWROLE2" };
            //Act
            var result = await _fixture.UserStore.UpdateAsync(user);
            //Assert
            Assert.True(result.Succeeded);
            var updatedUser = _fixture.Repository.UserRepository.Read(_fixture.SeedData.UpdateUser.NormalizedUserName);
            Assert.Equal(user.UserName, updatedUser.Username);
            Assert.Equal(user.Email, updatedUser.Email);
            Assert.Equal(user.PasswordHash, updatedUser.HashedPassword);
            Assert.Equal(user.SecurityStamp, updatedUser.SecurityStamp);
            Assert.Equal(user.CanUserChangePassword, updatedUser.CanUserChangePassword);
            Assert.Equal(user.Roles, updatedUser.Roles);
        }

        [Fact]
        public async Task UpdateAsync_Failed()
        {
            //Arrange
            //Act
            var result = await _fixture.UserStore.UpdateAsync(_fixture.SeedData.NoExistUser);
            //Assert
            Assert.False(result.Succeeded);
        }
    }
}
