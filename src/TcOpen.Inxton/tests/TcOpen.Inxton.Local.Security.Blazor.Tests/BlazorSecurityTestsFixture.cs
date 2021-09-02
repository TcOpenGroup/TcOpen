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
        private InMemoryRepository<RoleModel> _inMemoryRepoRole;
        public BlazorSecurityTestsFixture()
        {
            _inMemoryRepoUser = new InMemoryRepository<UserData>();
            _inMemoryRepoRole = new InMemoryRepository<RoleModel>();
            Repository = new RepositoryService(_inMemoryRepoUser, _inMemoryRepoRole);
            UserStore = new UserStore(Repository);
            RoleStore = new RoleStore(Repository);
            SeedData = new Seed();
            Repository.UserRepository.Create(SeedData.DefaultUser.Id, new UserData(SeedData.DefaultUser));
            Repository.UserRepository.Create(SeedData.RemoveUser.Id, new UserData(SeedData.RemoveUser));
            Repository.UserRepository.Create(SeedData.UpdateUser.Id, new UserData(SeedData.UpdateUser));
           
            Repository.RoleRepository.Create("75f2267f-eabd-4e9b-bdfa-28cf0cb42854", SeedData.AdminRole);
            Repository.RoleRepository.Create("3fc7c8af-7ca7-46c4-b897-e11df6b6432f", SeedData.DefaultRole);

        }




        public UserStore UserStore { get; set; }
        public RoleStore RoleStore { get; set; }
      
        public Seed SeedData { get; set; }
        public IRepositoryService Repository { get; set; }

        public void Dispose()
        {
            _inMemoryRepoUser = new InMemoryRepository<UserData>();
            _inMemoryRepoRole = new InMemoryRepository<RoleModel>();
            Repository = new RepositoryService(_inMemoryRepoUser, _inMemoryRepoRole);
        }
    }
}
