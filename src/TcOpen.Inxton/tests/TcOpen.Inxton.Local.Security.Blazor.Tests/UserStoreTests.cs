using System;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;
using System.Collections.Generic;

namespace TcOpen.Inxton.Local.Security.Blazor.Tests
{
    public class UserStoreTests: IClassFixture<BlazorSecurityTestsFixture>
    {

        private readonly BlazorSecurityTestsFixture _fixture;

        public UserStoreTests(BlazorSecurityTestsFixture fixture)
        {
            this._fixture = fixture;

        }
        [Fact]
        public async Task CreateNewUser_Successfull()
        {
            //Arrange
            //Act
            var result = await _fixture.UserStore.CreateAsync(_fixture.SeedData.AdminUser);
            //Assert
            var user = _fixture.Repository.UserRepository.Read("ADMIN");
            Assert.NotNull(user);
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task CreateExistingUser_Failed()
        {
            //Arrange
            //Act
            var result =  await _fixture.UserStore.CreateAsync(_fixture.SeedData.DefaultUser);
            //Assert
            Assert.False(result.Succeeded);
        }

        [Fact]
        public async Task FindUserById_Successfull()
        {
            //Arrange
            //Act
            var user = await _fixture.UserStore.FindByIdAsync(_fixture.SeedData.DefaultUser.Id);
            //Assert
            Assert.NotNull(user);
        }

        [Fact]
        public async Task FindUserById_Failed()
        {
            //Arrange
            //Act
            var user = await _fixture.UserStore.FindByIdAsync("UNKNOWNID");
            //Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task FindUserByNormalizedUserName_Successfull()
        {
            //Arrange
            //Act
            var user = await _fixture.UserStore.FindByNameAsync(_fixture.SeedData.DefaultUser.NormalizedUserName);
            //Assert
            Assert.NotNull(user);
        }

        [Fact]
        public async Task FindUserByNormalizedUserName_Failed()
        {
            //Arrange
            //Act
            var user = await _fixture.UserStore.FindByIdAsync("UNKNOWNUSERNAME");
            //Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task UpdateExistingUserEmail_Successfull()
        {
            //Arrange
            var newEmail = "newmail@newmail.com";
            var customUser = new User
            {
                UserName = "update",
                NormalizedUserName = "UPDATE",
                Email = newEmail,
                NormalizedEmail = "UPDATE@UPDATE.COM",
                Roles = new List<string> { "admin", "service" }.ToArray(),
                Id = "UPDATE",
                CanUserChangePassword = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = "password"
            };
            //Act
            var result = await _fixture.UserStore.UpdateAsync(customUser);
            var user = _fixture.Repository.UserRepository.Read(customUser.Id);
            //Assert
            Assert.True(result.Succeeded);
            Assert.Equal(newEmail, user.Email);
        }

        [Fact]
        public async Task UpdateUser_Failed()
        {
            //Arrange
            var customUser = new User
            {
                UserName = "default_nonexist",
                NormalizedUserName = "DEFAULT_NONEXIST",
                Email = "email",
                NormalizedEmail = "UPDATE@UPDATE.COM",
                Roles = new List<string> { "admin", "service" }.ToArray(),
                Id = "DEFAULT_NONEXIST",
                CanUserChangePassword = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = "password"
            };
           
            //Act
            var result = await _fixture.UserStore.UpdateAsync(customUser);
            //Assert
            Assert.False(result.Succeeded);
        }

        [Fact]
        public async Task DeleteUser_Successfull()
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
        public async Task DeleteNonExistingUser_SuccessDoNotThrow()
        {
            //Arrange
            UserData user = null;
            var customUser = _fixture.SeedData.RemoveUser;
            customUser.UserName = "default_nonexist";
            customUser.NormalizedUserName = "DEFAULT_NONEXIST";
            customUser.Id = "DEFAULT_NONEXIST";
            customUser.Email = "update@email.com";
            //Act
            var result = await _fixture.UserStore.DeleteAsync(customUser);
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
        public async Task GetUserId_Success()
        {
            //Arrange
           var expectedId = _fixture.SeedData.DefaultUser.Id;
            //Act
            var id = await _fixture.UserStore.GetUserIdAsync(_fixture.SeedData.DefaultUser);
            //Assert
            
            Assert.NotNull(id);
            Assert.Equal(expectedId, id);

        }

        [Fact]
        public async Task GetUserName_Success()
        {
            //Arrange
            var expectedUsername = _fixture.SeedData.DefaultUser.UserName;
            //Act
            var username = await _fixture.UserStore.GetUserNameAsync(_fixture.SeedData.DefaultUser);
            //Assert

            Assert.NotNull(username);
            Assert.Equal(expectedUsername, username);

        }

        [Fact]
        public async Task GetNormalizedUserName_Success()
        {
            //Arrange
            var expectedUsername = _fixture.SeedData.DefaultUser.NormalizedUserName;
            //Act
            var username = await _fixture.UserStore.GetNormalizedUserNameAsync(_fixture.SeedData.DefaultUser);
            //Assert

            Assert.NotNull(username);
            Assert.Equal(expectedUsername, username);

        }

        [Fact]
        public async Task IsDefaultInRole_Success()
        {
            //Arrange
            var normalizedRoleName = "DEFAULT";
            //Act
            var result = await _fixture.UserStore.IsInRoleAsync(_fixture.SeedData.DefaultUser, normalizedRoleName);
            //Assert
            Assert.True(result);

        }

        [Fact]
        public async Task GetUsersInDefaultRole_Success()
        {
            //Arrange
            var usersCount = 1;
            var normalizedRoleName = "DEFAULT";
            //Act
            var result = await _fixture.UserStore.GetUsersInRoleAsync(normalizedRoleName);
            //Assert
            Assert.NotNull(result);
            Assert.Contains(_fixture.SeedData.DefaultUser.UserName, result.Select(x => x.UserName));

        }


    }
}
