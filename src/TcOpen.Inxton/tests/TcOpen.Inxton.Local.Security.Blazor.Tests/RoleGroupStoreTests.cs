using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TcOpen.Inxton.Security;
using Xunit;

namespace TcOpen.Inxton.Local.Security.Blazor.Tests
{
    public class RoleGroupStoreTests : IClassFixture<BlazorSecurityTestsFixture>
    {
        private readonly BlazorSecurityTestsFixture _fixture;

        public RoleGroupStoreTests(BlazorSecurityTestsFixture fixture)
        {
            this._fixture = fixture;
        }

        //AddRolesToGroup
        [Fact]
        public void AddRolesToGroup_Success()
        {
            //Arrange
            var group = "UpdateGroup";
            var expectedRoles = new string[] { "NEWROLE1", "NEWROLE2" };
            //Act
            var result = _fixture.Repository.RoleGroupManager.AddRolesToGroup(group, expectedRoles);
            //Assert
            Assert.True(result.Succeeded);
            var roles = _fixture.Repository.RoleGroupManager.GetRolesFromGroup(group);
            Assert.Contains(expectedRoles[0], roles);
            Assert.Contains(expectedRoles[1], roles);
        }

        [Fact]
        public void AddRolesToGroup_NoExist_Failed()
        {
            //Arrange
            var group = "NoExistGroup";
            var expectedRoles = new string[] { "NEWROLE1", "NEWROLE2" };
            //Act
            var result = _fixture.Repository.RoleGroupManager.AddRolesToGroup(group, expectedRoles);
            //Assert
            Assert.False(result.Succeeded);
            var roles = _fixture.Repository.RoleGroupManager.GetRolesFromGroup(group);
            Assert.Null(roles);
        }

        [Fact]
        public void AddRolesToGroup_Empty_Failed()
        {
            //Arrange
            IdentityResult result = null;
            //Act
            try
            {
                result = _fixture.Repository.RoleGroupManager.AddRolesToGroup(
                    "",
                    new string[] { "" }
                );
            }
            catch (Exception)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void AddRolesToGroup_Null_Failed()
        {
            //Arrange
            IdentityResult result = null;
            //Act
            try
            {
                result = _fixture.Repository.RoleGroupManager.AddRolesToGroup(null, null);
            }
            catch (Exception)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //AddRoleToGroup
        [Fact]
        public void AddRoleToGroup_Success()
        {
            //Arrange
            var group = "UpdateGroup";
            var role = "NEWROLE3";
            //Act
            var result = _fixture.Repository.RoleGroupManager.AddRoleToGroup(group, role);
            //Assert
            Assert.True(result.Succeeded);
            var roles = _fixture.Repository.RoleGroupManager.GetRolesFromGroup(group);
            Assert.Contains(role, roles);
        }

        [Fact]
        public void AddRoleToGroup_Empty_Failed()
        {
            //Arrange
            IdentityResult result = null;
            //Act
            try
            {
                result = _fixture.Repository.RoleGroupManager.AddRoleToGroup("", "");
            }
            catch (Exception)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void AddRoleToGroup_Null_Failed()
        {
            //Arrange
            IdentityResult result = null;
            //Act
            try
            {
                result = _fixture.Repository.RoleGroupManager.AddRoleToGroup(null, null);
            }
            catch (Exception)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //CreateGroup
        [Fact]
        public void CreateGroup_Success()
        {
            //Arrange
            var group = "CreateGroup";
            //Act
            var result = _fixture.Repository.RoleGroupManager.CreateGroup(group);
            //Assert
            Assert.True(result.Succeeded);
            var groups = _fixture.Repository.RoleGroupManager.GetAllGroup();
            bool found = false;
            foreach (GroupData data in groups)
            {
                if (data.Name == group)
                    found = true;
            }
            Assert.True(found);
        }

        [Fact]
        public void CreateGroup_Empty_Failed()
        {
            //Arrange
            IdentityResult result = null;
            //Act
            try
            {
                result = _fixture.Repository.RoleGroupManager.CreateGroup("");
            }
            catch (Exception)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void CreateGroup_Null_Failed()
        {
            //Arrange
            IdentityResult result = null;
            //Act
            try
            {
                result = _fixture.Repository.RoleGroupManager.CreateGroup(null);
            }
            catch (Exception)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //CreateRole
        [Fact]
        public void CreateRole_Success()
        {
            //Arrange
            var expectedRole = new Role("CreateRole");
            //Act
            var result = _fixture.Repository.RoleGroupManager.CreateRole(expectedRole);
            //Assert
            Assert.True(result.Succeeded);
            var role = _fixture.Repository.RoleGroupManager.inAppRoleCollection.Find(x =>
                x.Name == expectedRole.Name
            );
            Assert.Equal(expectedRole, role);
        }

        [Fact]
        public void CreateRole_Null_Failed()
        {
            //Arrange
            IdentityResult result = null;
            //Act
            try
            {
                result = _fixture.Repository.RoleGroupManager.CreateRole(null);
            }
            catch (Exception)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //DeleteGroup
        [Fact]
        public void DeleteGroup_Success()
        {
            //Arrange
            var group = "RemoveGroup";
            //Act
            var result = _fixture.Repository.RoleGroupManager.DeleteGroup(group);
            //Assert
            Assert.True(result.Succeeded);
            var groups = _fixture.Repository.RoleGroupManager.GetAllGroup();
            bool found = false;
            foreach (GroupData data in groups)
            {
                if (data.Name == group)
                    found = true;
            }
            Assert.False(found);
        }

        [Fact]
        public void DeleteGroup_Null_Failed()
        {
            //Arrange
            IdentityResult result = null;
            //Act
            try
            {
                result = _fixture.Repository.RoleGroupManager.DeleteGroup(null);
            }
            catch (Exception)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        //GetAllGroup
        [Fact]
        public void GetAllGroup_Success()
        {
            //Arrange
            //Act
            var groups = _fixture.Repository.RoleGroupManager.GetAllGroup();
            //Assert
            Assert.NotNull(groups);
        }

        //GetRolesFromGroup
        [Fact]
        public void GetRolesFromGroup_Success()
        {
            //Arrange
            var group = "DefaultGroup";
            var expectedRoles = new string[] { "Administrator", "Default" };
            //Act
            var roles = _fixture.Repository.RoleGroupManager.GetRolesFromGroup(group);
            //Assert
            Assert.Equal(expectedRoles, roles);
        }

        [Fact]
        public void GetRolesFromGroup_NoExist_Failed()
        {
            //Arrange
            //Act
            List<string> result = _fixture.Repository.RoleGroupManager.GetRolesFromGroup(
                "NoExistGroup"
            );
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetRolesFromGroup_Empty_Failed()
        {
            //Arrange
            //Act
            List<string> result = _fixture.Repository.RoleGroupManager.GetRolesFromGroup("");
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetRolesFromGroup_Null_Failed()
        {
            //Arrange
            //Act
            List<string> result = _fixture.Repository.RoleGroupManager.GetRolesFromGroup(null);
            //Assert
            Assert.Null(result);
        }

        //GetRolesFromGroupString
        [Fact]
        public void GetRolesFromGroupString_Success()
        {
            //Arrange
            var group = "DefaultGroup";
            var expectedRoles = "Administrator,Default";
            //Act
            var roles = _fixture.Repository.RoleGroupManager.GetRolesFromGroupString(group);
            //Assert
            Assert.Equal(expectedRoles, roles);
        }

        [Fact]
        public void GetRolesFromGroupString_NoExist_Failed()
        {
            //Arrange
            //Act
            string result = _fixture.Repository.RoleGroupManager.GetRolesFromGroupString(
                "NoExistGroup"
            );
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetRolesFromGroupString_Empty_Failed()
        {
            //Arrange
            //Act
            string result = _fixture.Repository.RoleGroupManager.GetRolesFromGroupString("");
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetRolesFromGroupString_Null_Failed()
        {
            //Arrange
            //Act
            string result = _fixture.Repository.RoleGroupManager.GetRolesFromGroupString(null);
            //Assert
            Assert.Null(result);
        }

        //RemoveRolesFromGroup
        [Fact]
        public void RemoveRolesFromGroup_Success()
        {
            //Arrange
            var group = "RemoveRolesGroup";
            var expectedRoles = new string[] { "Administrator" };
            //Act
            var result = _fixture.Repository.RoleGroupManager.RemoveRolesFromGroup(
                group,
                expectedRoles
            );
            //Assert
            Assert.True(result.Succeeded);
            var roles = _fixture.Repository.RoleGroupManager.GetRolesFromGroup(group);
            Assert.Contains("Default", roles);
        }

        [Fact]
        public void RemoveRolesFromGroup_NoExist_Failed()
        {
            //Arrange
            //Act
            IdentityResult result = _fixture.Repository.RoleGroupManager.RemoveRolesFromGroup(
                "NoExistGroup",
                new string[] { "NoExistRole" }
            );
            //Assert
            Assert.False(result.Succeeded);
        }

        [Fact]
        public void RemoveRolesFromGroup_Empty_Failed()
        {
            //Arrange
            IdentityResult result = null;
            //Act
            try
            {
                result = _fixture.Repository.RoleGroupManager.RemoveRolesFromGroup(
                    "",
                    new string[] { "" }
                );
            }
            catch (Exception)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void RemoveRolesFromGroup_Null_Failed()
        {
            //Arrange
            IdentityResult result = null;
            //Act
            try
            {
                result = _fixture.Repository.RoleGroupManager.RemoveRolesFromGroup(null, null);
            }
            catch (Exception)
            {
                Assert.Null(result);
            }
            //Assert
            Assert.Null(result);
        }
    }
}
