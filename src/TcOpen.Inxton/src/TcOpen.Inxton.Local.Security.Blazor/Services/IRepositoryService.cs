using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Local.Security.Blazor.Users;

namespace TcOpen.Inxton.Local.Security.Blazor.Services
{
    public interface IRepositoryService : IDisposable
    {
        IRepository<UserData> UserRepository { get; }
        BlazorRoleGroupManager RoleGroupManager { get; }
    }
}
