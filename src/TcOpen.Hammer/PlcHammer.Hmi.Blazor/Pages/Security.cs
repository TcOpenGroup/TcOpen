using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TcOpen.Inxton.Data.MongoDb;
using TcOpen.Inxton.Local.Security;
using TcOpen.Inxton.Local.Security.Blazor.Users;

namespace PlcHammer.Hmi.Blazor.Pages
{
    public partial class Security
    {
       
        public async Task AddToRole(string roleName, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName.ToUpper());


            if(user != null)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var normalizer =new UpperInvariantLookupNormalizer();
                    var identityRole = new IdentityRole(roleName);
                    identityRole.NormalizedName = normalizer.NormalizeName(roleName);
                 

                    await _roleManager.CreateAsync(identityRole);
                }


                await _userManager.AddToRoleAsync(user, roleName);
                //await _userManager.AddToRolesAsync(user, roleName);
                //await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, roleName));
            }
        }
    }
}
