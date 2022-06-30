using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        //[Fact]
        //public async Task CreateNewRole_Successfull()
        //{
        //    //Arrange
        //    var newRole = new IdentityRole("custom");
        //    newRole.NormalizedName = "CUSTOM";
        //    newRole.Id = Guid.NewGuid().ToString();
        //    //Act
        //    var result = await _fixture.RoleStore.CreateAsync(newRole);
        //    //Assert
        //    var role = _fixture.Repository.RoleRepository.Read(newRole.Id);
        //    Assert.NotNull(role);
        //    Assert.True(result.Succeeded);
        //}


        //[Fact]
        //public async Task FindRoleById_Successfull()
        //{
        //    //Arrange
        //    //Act
        //    var role = await _fixture.RoleStore.FindByIdAsync(_fixture.SeedData.DefaultRole._EntityId);
        //    //Assert
        //    Assert.NotNull(role);

        //}
        //[Fact]
        //public async Task FindRoleByName_Successfull()
        //{
        //    //Arrange
        //    //Act
        //    var role = await _fixture.RoleStore.FindByNameAsync(_fixture.SeedData.DefaultRole.NormalizedName);
        //    //Assert
        //    Assert.NotNull(role);

        //}
    }
}
