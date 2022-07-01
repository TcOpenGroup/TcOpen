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
                Roles = new string[] { "Admin"},
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
                Roles = new string[] { "Default" },
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

           
        }
        public User AdminUser { get; set; }
        public User DefaultUser { get; set; }
        public User RemoveUser { get; set; }
        public User UpdateUser { get; set; }
       
    }
}
