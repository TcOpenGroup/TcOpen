using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Local.Security.Blazor.Areas.Identity.Pages;

namespace TcOpen.Inxton.Local.Security.Blazor
{
    public partial class UserManagementView
    {
        [Inject]
        private UserManager<User> _userManager { get; set; }
        [Inject]
        private  SignInManager<User> _signInManager { get; set; }
        [Inject]
        private  RoleManager<IdentityRole> _roleManager { get; set; }
        [Inject]
        private RevalidatingIdentityAuthenticationStateProvider<User> _authenticationStateProvider { get; set; }


    }
}
