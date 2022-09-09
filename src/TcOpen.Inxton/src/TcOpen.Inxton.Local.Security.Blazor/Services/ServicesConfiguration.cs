using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;
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
            RoleGroupManager roleGroupManager)
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

            BlazorAuthenticationStateProvider blazorAuthenticationStateProvider = new BlazorAuthenticationStateProvider(userRepo, roleGroupManager);

            SecurityManager.Create(blazorAuthenticationStateProvider);

            services.AddScoped<RoleGroupManager>(p => roleGroupManager);
            services.AddScoped<BlazorAlertManager>();
            services.AddScoped<IRepositoryService, RepositoryService>(provider => new RepositoryService(userRepo, roleGroupManager));
            //services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
            services.AddScoped<AuthenticationStateProvider, BlazorAuthenticationStateProvider>(p => blazorAuthenticationStateProvider);
        }
    }
}
