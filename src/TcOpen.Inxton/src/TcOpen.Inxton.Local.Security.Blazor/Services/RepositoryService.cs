using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Local.Security.Blazor.Users;

namespace TcOpen.Inxton.Local.Security.Blazor.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly IRepository<UserData>  _userRepository;
        private readonly BlazorRoleManager _roleInAppRepository; 
        private readonly BlazorGroupManager _groupManager;
        private bool _disposed;
        public RepositoryService(IRepository<UserData> userRepository,BlazorRoleManager roleInAppRepository, BlazorGroupManager groupManager)
        {
            _userRepository = userRepository;
            _roleInAppRepository = roleInAppRepository;
            _groupManager = groupManager;
        }
        public IRepository<UserData> UserRepository
        {
            get
            {
                return _userRepository;
            }
        }

        public BlazorRoleManager RoleInAppRepository
        {
            get
            {
                return _roleInAppRepository;
            }
        }

        public BlazorGroupManager GroupManager
        {
            get
            {
                return _groupManager;
            }
        }

        public void Dispose()
        {
            _disposed = true;
        }
    }
}
