using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Security;
using Xunit;

namespace TcOpen.Inxton.Local.Security.Blazor.Tests
{
    public class RoleStoreTests : IClassFixture<BlazorSecurityTestsFixture>
    {
        private readonly BlazorSecurityTestsFixture _fixture;

        public RoleStoreTests(BlazorSecurityTestsFixture fixture)
        {
            this._fixture = fixture;

        }

        [Fact]
        public void CreateNewRole_Successfull()
        {
            //Arrange
            var newRole = new Role("TestRole", "TestGroup");
            //Act
            _fixture.Repository.RoleGroupManager.CreateRole(newRole);
            var createdRole = _fixture.Repository.RoleGroupManager.inAppRoleCollection.Find(x=>x.Name  == newRole.Name);
            //Assert
            Assert.NotNull(createdRole);
        }

        [Fact]
        public void GetGroupString_Successfull()
        {
            //Arrange
            var group = "AdminGroup";
            var expected = "Administrator";
            //Act
            var groupString = _fixture.Repository.RoleGroupManager.GetRolesFromGroupString(group);
            //Assert
            Assert.Equal(expected, groupString);
        }
    }
}
