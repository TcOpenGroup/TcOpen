using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Local.Security.Blazor.Users;

namespace TcOpen.Inxton.Local.Security.Blazor.Services
{
    public class BlazorSecurityOptions
    {
        public IRepository<UserData> UserRepo { get; set; }
        public IRepository<RoleModel> RoleRepo { get; set; }
  
    }
}
