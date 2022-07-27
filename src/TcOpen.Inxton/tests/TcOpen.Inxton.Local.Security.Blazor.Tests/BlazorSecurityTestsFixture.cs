using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private BlazorRoleGroupManager _roleGroupManager;
        public BlazorSecurityTestsFixture()
        {
            _inMemoryRepoUser = new InMemoryRepository<UserData>();
            _inMemoryRepoGroup = new InMemoryRepository<GroupData>();
            _roleGroupManager = new BlazorRoleGroupManager(_inMemoryRepoGroup);
            Repository = new RepositoryService(_inMemoryRepoUser, _roleGroupManager);
            UserStore = new UserStore(Repository);
            SeedData = new Seed();

            Repository.UserRepository.Create(SeedData.ExistUser.Id, new UserData(SeedData.ExistUser));
            Repository.UserRepository.Create(SeedData.RemoveUser.Id, new UserData(SeedData.RemoveUser));
            Repository.UserRepository.Create(SeedData.UpdateUser.Id, new UserData(SeedData.UpdateUser));
            Repository.UserRepository.Create(SeedData.AdminUser.Id, new UserData(SeedData.AdminUser));
            Repository.UserRepository.Create(SeedData.DefaultUser.Id, new UserData(SeedData.DefaultUser));

            _roleGroupManager.CreateRole(new Inxton.Security.Role("RemoveRole"));
            _roleGroupManager.CreateRole(new Inxton.Security.Role("UpdateRole"));
            _roleGroupManager.CreateRole(new Inxton.Security.Role("Administrator"));
            _roleGroupManager.CreateRole(new Inxton.Security.Role("Default"));

            _roleGroupManager.CreateGroup("RemoveGroup");
            _roleGroupManager.CreateGroup("RemoveRolesGroup");
            _roleGroupManager.CreateGroup("UpdateGroup");
            _roleGroupManager.CreateGroup("AdminGroup");
            _roleGroupManager.CreateGroup("DefaultGroup");

            _roleGroupManager.AddRoleToGroup("AdminGroup", "Administrator");
            _roleGroupManager.AddRolesToGroup("DefaultGroup", new string[] { "Administrator", "Default" });
            _roleGroupManager.AddRolesToGroup("RemoveRolesGroup", new string[] { "Administrator", "Default" });
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
