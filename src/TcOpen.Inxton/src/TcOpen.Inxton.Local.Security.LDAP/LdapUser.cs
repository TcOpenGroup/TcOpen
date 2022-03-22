using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security.LDAP
{
    public class LdapUser : IUser
    {
        public bool CanUserChangePassword { get; set; }
        public string Email { get; set; }
        public string Level { get; set; }
        public string[] Roles { get; set; }
        public string UserName { get; set; }
    }
}