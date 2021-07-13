using System;
using System.Security.Principal;

namespace TcOpen.Inxton.Local.Security
{
    public partial class AppIdentity : IIdentity
    {
        public AppIdentity(string name, string email, string[] roles, bool canUserChangePassword, string level)
        {
            Name = name;
            Email = email;
            Roles = roles;
            Level = level;           
            CanUserChangePassword = canUserChangePassword;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string[] Roles { get; private set; }

        public string Level { get; private set; }  

        public bool CanUserChangePassword { get; private set;}

#region IIdentity Members
        public string AuthenticationType { get { return "Custom authentication"; } }

        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }
#endregion
    }
}
