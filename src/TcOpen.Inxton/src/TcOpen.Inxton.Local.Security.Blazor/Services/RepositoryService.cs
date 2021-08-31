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
        private readonly IRepository<RoleModel> _roleRepository;
        private bool _disposed;
        public RepositoryService(IRepository<UserData> userRepository, 
            IRepository<RoleModel> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public IRepository<UserData> UserRepository
        {
            get
            {
                return _userRepository;
            }
        }

        public IRepository<RoleModel> RoleRepository
        {
            get
            {
                return _roleRepository;
            }
        }

        public void Dispose()
        {
            _disposed = true;
        }
    }
}
