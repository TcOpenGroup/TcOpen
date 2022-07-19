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
            BlazorRoleManager roleManager,
            IRepository<GroupData> groupRepo,
            BlazorGroupManager groupManager)
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

            if (!groupManager.GetAllGroup().Select(x => x.Name).Contains("AdminGroup"))
            {
                groupManager.Create("AdminGroup");
                groupManager.AddRole("AdminGroup", "Administrator");
            }

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
            services.AddScoped<BlazorGroupManager>(p => groupManager);
            services.AddScoped<IRepositoryService, RepositoryService>(provider => new RepositoryService(userRepo,roleManager, groupManager));
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
        }
    }
}
