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
        private BlazorRoleGroupManager _roleGroupManager { get; set; }
        [Inject]
        private BlazorAlertManager _alertManager { get; set; }

        private IList<RoleData> AvailableRoles { get; set; }
        private IList<RoleData> AssignedRoles { get; set; }

        public GroupData SelectedGroupN { get; set; }
        public string newGroupName { get; set; }

        public void AssignRoles()
        {
            _roleGroupManager.AddRolesToGroup(SelectedGroupN.Name, AvailableRoles.Where(x => x.IsSelected == true).Select(x => x.Role.Name));
            GroupClicked(SelectedGroupN);
        }

        public void ReturnRoles()
        {
            _roleGroupManager.RemoveRolesFromGroup(SelectedGroupN.Name, AssignedRoles.Where(x => x.IsSelected == true).Select(x => x.Role.Name));
            GroupClicked(SelectedGroupN);
        }

        public void GroupClicked(GroupData group)
        {
            SelectedGroupN = group;
            AssignedRoles = _roleGroupManager.GetRolesFromGroup(group.Name).Select(x => new RoleData(_roleGroupManager.inAppRoleCollection.Where(x1 => x1.Name == x).FirstOrDefault())).ToList();
            AvailableRoles = _roleGroupManager.inAppRoleCollection.Where(x => !AssignedRoles.Select(x => x.Role.Name).Contains(x.Name)).Select(x => new RoleData(x)).ToList();
            StateHasChanged();
        }

        public void CloseGroupDetail()
        {
            SelectedGroupN = null;
        }

        public void CreateGroup()
        {
            var result = _roleGroupManager.CreateGroup(newGroupName);
            if (result.Succeeded)
            {
                _alertManager.addAlert("success", "Group succesfully created!");
            }
            else
            {
                _alertManager.addAlert("warning", "Group was not created!");
            }
            StateHasChanged();
        }

        public void DeleteGroup(GroupData group)
        {
            SelectedGroupN = null;
            var result = _roleGroupManager.DeleteGroup(group.Name);
            if (result.Succeeded)
            {
                _alertManager.addAlert("success", "Group succesfully deleted!");
            }
            else
            {
                _alertManager.addAlert("warning", "Group was not deleted!");
            }
            StateHasChanged();
        }
    }
}