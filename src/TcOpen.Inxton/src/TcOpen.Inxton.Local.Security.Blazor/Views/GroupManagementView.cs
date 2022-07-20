using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security.Blazor
{
    public partial class GroupManagementView
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
        private BlazorRoleManager _roleManager { get; set; }
        [Inject]
        private BlazorGroupManager _groupManager { get; set; }

        private IList<RoleData> AvailableRoles { get; set; }
        private IList<RoleData> AssignedRoles { get; set; }

        public GroupData SelectedGroupN { get; set; }
        public string newGroupName { get; set; }

        public async Task AssignRoles()
        {
            await _groupManager.AddRolesAsync(SelectedGroupN.Name, AvailableRoles.Where(x => x.IsSelected == true).Select(x => x.Role.Name));
            GroupClicked(SelectedGroupN);
        }

        public async Task ReturnRoles()
        {
            await _groupManager.RemoveRolesAsync(SelectedGroupN.Name, AssignedRoles.Where(x => x.IsSelected == true).Select(x => x.Role.Name));
            GroupClicked(SelectedGroupN);
        }

        public void GroupClicked(GroupData group)
        {
            SelectedGroupN = group;
            AssignedRoles = _groupManager.GetRoles(group.Name).Select(x => new RoleData(_roleManager.InAppRoleCollection.Where(x1 => x1.Name == x).FirstOrDefault())).ToList();
            AvailableRoles = _roleManager.InAppRoleCollection.Where(x => !AssignedRoles.Select(x => x.Role.Name).Contains(x.Name)).Select(x => new RoleData(x)).ToList();
            StateHasChanged();
        }

        public void CloseGroupDetail()
        {
            SelectedGroupN = null;
        }

        public async Task CreateGroup()
        {
            await _groupManager.CreateAsync(newGroupName);
            StateHasChanged();
        }

        public async Task DeleteGroup(GroupData group)
        {
            await _groupManager.DeleteAsync(group.Name);
            StateHasChanged();
        }
    }
}