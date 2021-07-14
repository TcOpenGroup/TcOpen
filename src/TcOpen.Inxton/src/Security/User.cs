using Microsoft.AspNetCore.Identity;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security
{
    public class User : IdentityUser<string>, IUser
    {
        public User(string username, string email, string[] roles, bool canUserChangePassword, string level)
        {
            var normalizer = new UpperInvariantLookupNormalizer();
            UserName = username;
            NormalizedUserName = normalizer.NormalizeName(UserName);            
            Email = email;
            NormalizedEmail = normalizer.NormalizeEmail(email);       
            Roles = roles;
            Level = level;
            CanUserChangePassword = canUserChangePassword;            
        }
        
        public string Level { get; set; }

        public string[] Roles
        {
            get;
            set;
        }

        public bool CanUserChangePassword { get; set; }
    }
}
