using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Local.Security.Blazor.Users;

namespace TcOpen.Inxton.Local.Security.Blazor.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRepository<UserData>  _userRepository;
        private readonly IRepository<BlazorRole> _roleRepository;
        private bool _disposed;
        public UnitOfWork(IRepository<UserData> userRepository, 
            IRepository<BlazorRole> roleRepository)
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

        public IRepository<BlazorRole> RoleRepository
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
