using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Local.Security.Blazor.Areas.Identity.Pages;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security.Blazor
{
    public partial class UserManagementView
    {

        private class RoleData
        {
            public RoleData(Role role) 
            {
                Role = role;
            }
            public Role Role { get; set; }
            public bool IsSelected { get; set; }
         
        }

        [Inject]
        private UserManager<User> _userManager { get; set; }
        [Inject]
        private SignInManager<User> _signInManager { get; set; }
        [Inject]
        private BlazorRoleManager _roleManager { get; set; }

        private User SelectedUser { get; set; }
        private IQueryable<User> Users { get; set; }
        private IList<RoleData> AvailableRoles { get; set; }
        private IList<RoleData> AssignedRoles { get; set; }
        public bool IsUserUpdated { get; set; }

        public void RoleAdded()
        {
            AvailableRoles = GetAvailableRoles();
            StateHasChanged();
        }

        public async Task AssignRoles()
        {
            await _userManager.AddToRolesAsync(SelectedUser, AvailableRoles.Where(x => x.IsSelected == true).Select(x => x.Role.Name));
            await RowClicked(SelectedUser);
        }

        public async Task ReturnRoles()
        {
            await _userManager.RemoveFromRolesAsync(SelectedUser, AssignedRoles.Where(x => x.IsSelected == true).Select(x => x.Role.Name));
            await RowClicked(SelectedUser);
        }

        public string SelectedGroup { get; set; }
        public async Task RowClicked(User user)
        {
            SelectedUser = user;
            var userAssignedRoles = await _userManager.GetRolesAsync(user);
            AssignedRoles = _roleManager.InAppRoleCollection.Where(p => userAssignedRoles.Any(p2 => p2 == p.Name)).Select(x=> new RoleData(x)).ToList(); 
            AvailableRoles = GetAvailableRoles();
            IsUserUpdated = false;
            StateHasChanged();
        }

        public void CloseUserDetail()
        {
            SelectedUser = null;
        }

        private IList<RoleData> GetAvailableRoles() =>
            _roleManager.InAppRoleCollection
                .Where(x => !AssignedRoles.Select(x => x.Role.Name).Contains(x.Name))
                .Select(x => new RoleData(x))
                .ToList();

        

        public async Task DeleteUser(User user)
        {
            await _userManager.DeleteAsync(user);
            SelectedUser = null;
        }

        public async Task OnAddGroupClicked(string selectedGroup)
        {
            var filtered = AvailableRoles.Where(x => x.Role.DefaultGroup == selectedGroup);
            foreach (var item in AvailableRoles)
            {
                if(filtered.Contains(item))
                    item.IsSelected = true;
            }
            await AssignRoles();
            
        }

        public async Task OnRemoveGroupClicked(string selectedGroup)
        {
            var filtered = AssignedRoles.Where(x => x.Role.DefaultGroup == selectedGroup);
            foreach (var item in AssignedRoles)
            {
                if (filtered.Contains(item))
                    item.IsSelected = true;
            }
            await ReturnRoles();

        }


        public async Task UpdateUser(User user)
        {
            await _userManager.UpdateAsync(user);
            IsUserUpdated = true;
        }

    }
}
