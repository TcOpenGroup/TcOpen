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

namespace TcOpen.Inxton.Local.Security.Blazor.Services
{
    public static class ServicesConfiguration
    {
        public static void AddVortexBlazorSecurity(this IServiceCollection services, 
            IRepository<UserData> userRepo, 
            IRepository<RoleModel> roleRepo)
        {


            services.AddIdentity<User, IdentityRole>(identity =>
            {
                identity.Password.RequireDigit = false;
                identity.Password.RequireLowercase = false;
                identity.Password.RequireNonAlphanumeric = false;
                identity.Password.RequireUppercase = false;
                identity.Password.RequiredLength = 1;
                identity.Password.RequiredUniqueChars = 0;
            }
            )
            .AddCustomStores()
            .AddDefaultTokenProviders();

            services.AddScoped<IRepositoryService, RepositoryService>(provider => new RepositoryService(userRepo, roleRepo));
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();

           

        }
    }
}
