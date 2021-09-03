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
            AdminUser = new User
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                Roles = new List<string>().ToArray(),
                Id = "ADMIN",
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
                Roles = new List<string> { "3fc7c8af-7ca7-46c4-b897-e11df6b6432f" }.ToArray(),
                Id = "DEFAULT",
                CanUserChangePassword = true,
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
                Id = "REMOVE",
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
                Id = "UPDATE",
                CanUserChangePassword = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = "password"

            };

            AdminRole = new RoleModel 
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            DefaultRole = new RoleModel
            {
                Name = "Default",
                NormalizedName = "DEFAULT"
            };
        }
        public User AdminUser { get; set; }
        public User DefaultUser { get; set; }
        public User RemoveUser { get; set; }
        public User UpdateUser { get; set; }
        public RoleModel AdminRole { get; set; }
        public RoleModel DefaultRole { get; set; }
    }
}
