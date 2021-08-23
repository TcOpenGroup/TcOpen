using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TcOpen.Inxton.Data.MongoDb;
using TcOpen.Inxton.Local.Security;

namespace PlcHammer.Hmi.Blazor.Pages
{
    public partial class Security
    {
        //MongoDbRepository<UserData> MongoRepo { get; set; } = new MongoDbRepository<UserData>(new MongoDbRepositorySettings<UserData>("mongodb://localhost:27017", "Hammer", "Users"));
        public async Task AddToRole(string roleName, string userName)
        {
            
            //var user = MongoRepo.GetRecords(userName).First();
            //var mongoUser = new User(user);
            
            ////var u = await _userManager.FindByNameAsync(userName);
            //await _userManager.UpdateSecurityStampAsync(mongoUser);

            //var role = new MongoRole(roleName);

            

            

            //var RoleExists = await _roleManager.RoleExistsAsync(roleName);
            //if (!RoleExists)
            //    await _roleManager.CreateAsync(role);

            //var roleId = await _roleManager.GetRoleIdAsync(role);

            //user.Roles.Add(roleId.ToString());
            
            //mongoUser.Roles.Append(roleId.ToString());

            //// if (u == null) return NotFound();

            //await _userManager.AddToRoleAsync(mongoUser, roleName);
            //await _userManager.AddClaimAsync(mongoUser, new Claim(ClaimTypes.Role, roleName));

            //MongoRepo.Update(userName, user);


        }

        public string RoleUser { get; set; }
        public async Task GetRoles(string userName)
        {
            //var user = MongoRepo.GetRecords(userName).First();
            //var mongoUser = new User(user);

            ////var u = await _userManager.FindByNameAsync(userName);
            //await _userManager.UpdateSecurityStampAsync(mongoUser);

            //var roles = await _userManager.GetRolesAsync(mongoUser);
            ////_roleManager.

            //RoleUser = roles.First();

        }
    }
}
