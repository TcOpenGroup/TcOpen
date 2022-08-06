using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using TcOpen.Inxton.Security;
using System.Linq;

namespace TcOpen.Inxton.Local.Security
{
    public class User : IdentityUser<string>, IUser
    {
        public User()
        {

        }
        public User(string username, string email, string[] roles, bool canUserChangePassword, string level)
        {
            var normalizer = new UpperInvariantLookupNormalizer();
            UserName = username;
            NormalizedUserName = normalizer.NormalizeName(UserName);            
            Email = email;
            NormalizedEmail = normalizer.NormalizeEmail(email);       
            Roles = roles?.ToArray();
            Level = level;
            CanUserChangePassword = canUserChangePassword;  
        }

        public User(UserData userData)
        {
            var normalizer = new UpperInvariantLookupNormalizer();
            UserName = userData.Username;
            NormalizedUserName = normalizer.NormalizeName(UserName);
            Email = userData.Email;
            NormalizedEmail = normalizer.NormalizeEmail(Email);
            Roles = userData.Roles.ToArray();
            Level = userData.Level;
            CanUserChangePassword = userData.CanUserChangePassword;
            PasswordHash = userData.HashedPassword;
            SecurityStamp = userData.SecurityStamp;
            Id = userData._EntityId;
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
