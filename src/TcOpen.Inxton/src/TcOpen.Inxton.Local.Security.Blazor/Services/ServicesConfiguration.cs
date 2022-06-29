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
                identity.Password.RequiredLength = 0;
                identity.Password.RequiredUniqueChars = 0;
                
                
            }
            )
            .AddCustomStores()
            .AddDefaultTokenProviders();

            services.AddScoped<IRepositoryService, RepositoryService>(provider => new RepositoryService(userRepo, roleRepo));
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();

            var allUsers = userRepo.GetRecords("*", Convert.ToInt32(userRepo.Count + 1), 0).ToList();
            if (!allUsers.Any())
            {
                CreateDefaultUser(userRepo, roleRepo);
            }

        }

        private static void CreateDefaultUser(IRepository<UserData> userRepo, IRepository<RoleModel> roleRepo)
        {
            //create Administrator role
            var identityRole = new IdentityRole("Administrator");
            identityRole.NormalizedName = new UpperInvariantLookupNormalizer().NormalizeName("Administrator");
            var roleEntity = new RoleModel
            {
                Name = identityRole.Name,
                NormalizedName = identityRole.NormalizedName,
                ConcurrencyStamp = identityRole.ConcurrencyStamp,
                _EntityId = identityRole.Id
            };
            roleRepo.Create(identityRole.Id, roleEntity);

            //create default admin user
            var user = new User("admin", null, null, false, null);
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "admin");
            user.Roles = new List<string>
            {
                roleEntity._EntityId
            }.ToArray();

            var userEntity = new UserData(user);
            userRepo.Create(user.NormalizedUserName, userEntity);
           
        }
    }
}
