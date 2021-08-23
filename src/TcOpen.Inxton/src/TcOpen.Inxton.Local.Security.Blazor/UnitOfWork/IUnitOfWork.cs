using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Local.Security.Blazor.Users;

namespace TcOpen.Inxton.Local.Security.Blazor.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<UserData> UserRepository { get; }
        IRepository<BlazorRole> RoleRepository { get; }

        void Commit();
    }
}
