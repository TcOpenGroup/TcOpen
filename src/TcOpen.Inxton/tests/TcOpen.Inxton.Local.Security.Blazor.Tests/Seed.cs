using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Local.Security.Blazor.Users;

namespace TcOpen.Inxton.Local.Security.Blazor.Tests
{
    public class Seed
    {
        public Seed()
        {
            SeedData();
        }

        public void SeedData()
        {
            ExistUser = new User
            {
                UserName = "exist",
                NormalizedUserName = "EXIST",
                Email = "exist@exist.com",
                NormalizedEmail = "EXIST@EXIST.COM",
                Roles = new List<string>().ToArray(),
                Id = "exist",
                CanUserChangePassword = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = "password"
            };

            NoExistUser = new User
            {
                UserName = "noexist",
                NormalizedUserName = "NOEXIST",
                Email = "noexist@noexist.com",
                NormalizedEmail = "NOEXIST@NOEXIST.COM",
                Roles = new List<string>().ToArray(),
                Id = "noexist",
                CanUserChangePassword = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = "password"
            };

            CreateUser = new User
            {
                UserName = "create",
                NormalizedUserName = "CREATE",
                Email = "create@create.com",
                NormalizedEmail = "CREATE@CREATE.COM",
                Roles = new List<string>().ToArray(),
                Id = "create",
                CanUserChangePassword = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = "password"
            };

            RemoveUser = new User
            {
                UserName = "remove",
                NormalizedUserName = "REMOVE",
                Email = "remove@remove.com",
                NormalizedEmail = "REMOVE@REMOVE.COM",
                Roles = new List<string>().ToArray(),
                Id = "remove",
                CanUserChangePassword = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = "password"
            };

            UpdateUser = new User
            {
                UserName = "update",
                NormalizedUserName = "UPDATE",
                Email = "update@update.com",
                NormalizedEmail = "UPDATE@UPDATE.COM",
                Roles = new List<string>().ToArray(),
                Id = "update",
                CanUserChangePassword = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = "password"
            };

            AdminUser = new User
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                Roles = new string[] { "AdminGroup" },
                Id = "admin",
                CanUserChangePassword = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = "password"
            };

            DefaultUser = new User
            {
                UserName = "default",
                NormalizedUserName = "DEFAULT",
                Email = "default@default.com",
                NormalizedEmail = "DEFAULT@DEFAULT.COM",
                Roles = new string[] { "DefaultGroup" },
                Id = "default",
                CanUserChangePassword = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = "password"
            };
        }

        public User ExistUser { get; set; }
        public User NoExistUser { get; set; }
        public User CreateUser { get; set; }
        public User RemoveUser { get; set; }
        public User UpdateUser { get; set; }
        public User AdminUser { get; set; }
        public User DefaultUser { get; set; }
    }
}
