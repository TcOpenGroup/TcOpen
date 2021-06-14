using TcOpen.Inxton.Abstractions.Security;

namespace TcOpen.Inxton.Security
{
    public class User : IUser
    {
        public User(string username, string email, string[] roles, bool canUserChangePassword, string level)
        {
            Username = username;
            Email = email;
            Roles = roles;
            Level = level;
            CanUserChangePassword = canUserChangePassword;
        }
        public string Username
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
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
