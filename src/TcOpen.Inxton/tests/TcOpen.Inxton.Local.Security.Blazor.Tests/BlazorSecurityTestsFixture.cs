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
        private BlazorRoleGroupManager _inMemoryRepoRole;
        public BlazorSecurityTestsFixture()
        {
            _inMemoryRepoUser = new InMemoryRepository<UserData>();
            //var groupRepo = new MongoDbRepository<GroupData>(new MongoDbRepositorySettings<GroupData>("mongodb://localhost:27017", "HammerBlazor", "Groups"));
            //_inMemoryRepoRole = new BlazorRoleGroupManager(groupRepo);
            Repository = new RepositoryService(_inMemoryRepoUser, _inMemoryRepoRole);
            UserStore = new UserStore(Repository);
            SeedData = new Seed();
            Repository.UserRepository.Create(SeedData.DefaultUser.Id, new UserData(SeedData.DefaultUser));
            Repository.UserRepository.Create(SeedData.RemoveUser.Id, new UserData(SeedData.RemoveUser));
            Repository.UserRepository.Create(SeedData.UpdateUser.Id, new UserData(SeedData.UpdateUser));

            _inMemoryRepoRole.CreateRole(new Inxton.Security.Role("Admin", "AdminGroup"));
            _inMemoryRepoRole.CreateRole(new Inxton.Security.Role("Default", "AdminGroup"));
         
           

        }




        public UserStore UserStore { get; set; }
      
        public Seed SeedData { get; set; }
        public IRepositoryService Repository { get; set; }

        public void Dispose()
        {
            _inMemoryRepoUser = new InMemoryRepository<UserData>();
            //_inMemoryRepoRole = new BlazorRoleGroupManager();
            Repository = new RepositoryService(_inMemoryRepoUser, _inMemoryRepoRole);
        }
    }
}
