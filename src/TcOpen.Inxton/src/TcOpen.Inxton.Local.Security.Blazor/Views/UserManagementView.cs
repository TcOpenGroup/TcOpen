﻿using Microsoft.AspNetCore.Components;
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

        private class RoleData
        {
            public RoleData(string roleName)
            {
                RoleName = roleName;
            }
            public string RoleName { get; set; }
            public bool IsSelected { get; set; }
        }

        [Inject]
        private UserManager<User> _userManager { get; set; }
        [Inject]
        private SignInManager<User> _signInManager { get; set; }
        [Inject]
        private RoleManager<IdentityRole> _roleManager { get; set; }

        private User SelectedUser { get; set; }
        private IQueryable<User> Users { get; set; }
        private IList<RoleData> AvailableRoles { get; set; }
        private IList<RoleData> AssignedRoles { get; set; }

        public void RoleAdded()
        {
            AvailableRoles = GetAvailableRoles();
            StateHasChanged();
        }

        public async Task AssignRoles()
        {
            await _userManager.AddToRolesAsync(SelectedUser, AvailableRoles.Where(x => x.IsSelected == true).Select(x => x.RoleName));
            await RowClicked(SelectedUser);
        }

        public async Task ReturnRoles()
        {
            await _userManager.RemoveFromRolesAsync(SelectedUser, AssignedRoles.Where(x => x.IsSelected == true).Select(x => x.RoleName));
            await RowClicked(SelectedUser);
        }

       
        public async Task RowClicked(User user)
        {
            SelectedUser = user;
            AssignedRoles = (await _userManager.GetRolesAsync(user)).Select(x => new RoleData(x)).ToList();
            AvailableRoles = GetAvailableRoles();
            StateHasChanged();
        }

        public void CloseUserDetail()
        {
            SelectedUser = null;
        }

        private IList<RoleData> GetAvailableRoles() =>
            _roleManager.Roles
                .Where(x => !AssignedRoles.Select(x => x.RoleName).Contains(x.Name))
                .Select(x => new RoleData(x.Name))
                .ToList();

        public string RoleName { get; set; }
        public async Task CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return;
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var normalizer = new UpperInvariantLookupNormalizer();
                var identityRole = new IdentityRole(roleName);
                identityRole.NormalizedName = normalizer.NormalizeName(roleName);
                await _roleManager.CreateAsync(identityRole);
                StateHasChanged();
                if(SelectedUser != null) RoleAdded();
            }

        }

       

        public async Task DeleteUser(User user)
        {
            await _userManager.DeleteAsync(user);
            SelectedUser = null;
        }

        public async Task DeleteRole(IdentityRole role)
        {
            await _roleManager.DeleteAsync(role);
            StateHasChanged();
            if (SelectedUser != null) RoleAdded();

        }

        public async Task UpdateUser(User user)
        {
            await _userManager.UpdateAsync(user);
           
        }

    }
}
