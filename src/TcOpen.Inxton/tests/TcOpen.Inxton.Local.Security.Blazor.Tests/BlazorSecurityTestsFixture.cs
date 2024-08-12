using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Data.InMemory;
using TcOpen.Inxton.Local.Security.Blazor.Services;
using TcOpen.Inxton.Local.Security.Blazor.Stores;
using TcOpen.Inxton.Local.Security.Blazor.Users;

namespace TcOpen.Inxton.Local.Security.Blazor.Tests
{
    public class BlazorSecurityTestsFixture : IDisposable
    {
        private InMemoryRepository<UserData> _inMemoryRepoUser;
        private InMemoryRepository<GroupData> _inMemoryRepoGroup;
        private RoleGroupManager _roleGroupManager;

        public BlazorSecurityTestsFixture()
        {
            _inMemoryRepoUser = new InMemoryRepository<UserData>();
            _inMemoryRepoGroup = new InMemoryRepository<GroupData>();
            _roleGroupManager = new RoleGroupManager(_inMemoryRepoGroup);
            Repository = new RepositoryService(_inMemoryRepoUser, _roleGroupManager);
            UserStore = new UserStore(Repository);
            SeedData = new Seed();

            Repository.UserRepository.Create(
                SeedData.ExistUser.UserName,
                new UserData(SeedData.ExistUser)
            );
            Repository.UserRepository.Create(
                SeedData.RemoveUser.UserName,
                new UserData(SeedData.RemoveUser)
            );
            Repository.UserRepository.Create(
                SeedData.UpdateUser.UserName,
                new UserData(SeedData.UpdateUser)
            );
            Repository.UserRepository.Create(
                SeedData.AdminUser.UserName,
                new UserData(SeedData.AdminUser)
            );
            Repository.UserRepository.Create(
                SeedData.DefaultUser.UserName,
                new UserData(SeedData.DefaultUser)
            );

            _roleGroupManager.CreateRole(new Inxton.Security.Role("RemoveRole"));
            _roleGroupManager.CreateRole(new Inxton.Security.Role("UpdateRole"));
            _roleGroupManager.CreateRole(new Inxton.Security.Role("Administrator"));
            _roleGroupManager.CreateRole(new Inxton.Security.Role("Default"));

            _roleGroupManager.CreateGroup("RemoveGroup");
            _roleGroupManager.CreateGroup("RemoveRolesGroup");
            _roleGroupManager.CreateGroup("UpdateGroup");
            _roleGroupManager.CreateGroup("DefaultGroup");

            _roleGroupManager.AddRolesToGroup(
                "DefaultGroup",
                new string[] { "Administrator", "Default" }
            );
            _roleGroupManager.AddRolesToGroup(
                "RemoveRolesGroup",
                new string[] { "Administrator", "Default" }
            );
        }

        public UserStore UserStore { get; set; }
        public Seed SeedData { get; set; }

        public IRepositoryService Repository { get; set; }

        public void Dispose()
        {
            _inMemoryRepoUser = new InMemoryRepository<UserData>();
            Repository = new RepositoryService(_inMemoryRepoUser, _roleGroupManager);
        }
    }
}
