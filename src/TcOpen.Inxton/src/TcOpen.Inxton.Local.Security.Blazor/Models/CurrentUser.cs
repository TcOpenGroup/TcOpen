using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Local.Security.Blazor.Users
{
    public class CurrentUser
    {
        public string Username { get; set; }
        public bool CanUserChangePassword { get; set; }
        //public ObjectId _id { get; set; }
    }
}

