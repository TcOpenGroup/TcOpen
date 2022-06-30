using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Local.Security.Blazor.Areas.Identity.Pages;
using TcOpen.Inxton.Local.Security.Blazor.Extension;
using TcOpen.Inxton.Local.Security.Blazor.Stores;
using TcOpen.Inxton.Local.Security.Blazor.Users;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security.Blazor.Services
{
    public static class ServicesConfiguration
    {

        public static void AddVortexBlazorSecurity(this IServiceCollection services, 
            IRepository<UserData> userRepo,
            BlazorRoleManager roleManager)
        {

            services.AddIdentity<User, Role>(identity =>
            {
                identity.Password.RequireDigit = false;
                identity.Password.RequireLowercase = false;
                identity.Password.RequireNonAlphanumeric = false;
                identity.Password.RequireUppercase = false;
                identity.Password.RequiredLength = 0;
                identity.Password.RequiredUniqueChars = 0;
                
                
            }
            )
            .AddCustomStores()
            .AddDefaultTokenProviders();


            roleManager.CreateRole(new Role("Administrator", "AdminGroup"));
            var allUsers = userRepo.GetRecords("*", Convert.ToInt32(userRepo.Count + 1), 0).ToList();
            if (!allUsers.Any())
            {
                
               
                string[] roles = { "Administrator" };
                //create default admin user
                var user = new User("admin", null, roles, false, null);
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "admin");
                user.Roles = new List<string>
                {
                    "Administrator"
                }.ToArray();

                var userEntity = new UserData(user);
                userRepo.Create(user.NormalizedUserName, userEntity);
            }
             services.AddScoped<BlazorRoleManager>(p=>roleManager);
             services.AddScoped<IRepositoryService, RepositoryService>(provider => new RepositoryService(userRepo,roleManager));
             services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();

        }

        
    }
}
