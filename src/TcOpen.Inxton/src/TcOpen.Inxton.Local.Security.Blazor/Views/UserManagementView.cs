using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using TcOpen.Inxton.Local.Security.Blazor.Users;
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
        private BlazorRoleGroupManager _roleGroupManager { get; set; }
        [Inject]
        private BlazorAlertManager _alertManager { get; set; }

        private User SelectedUser { get; set; }
        private RegisterUserModel _model { get; set; }

        public void RowClicked(User user)
        {
            SelectedUser = user;
            //_model = new RegisterUserModel();

            _model.Username = user.UserName;
            _model.Password = "password";
            _model.ConfirmPassword = "password";
            _model.CanUserChangePassword = user.CanUserChangePassword;
            _model.Email = user.Email;
            _model.Group = user.Roles[0];

            StateHasChanged();
        }

        public void CloseUserDetail()
        {
            SelectedUser = null;
        }

        public async Task DeleteUser(User user)
        {
            await _userManager.DeleteAsync(user);
            SelectedUser = null;
            _alertManager.addAlert("success", "User succesfully deleted!");
            TcoAppDomain.Current.Logger.Information($"User '{user.UserName}' deleted. {{@sender}}", new { UserName = user.UserName });
        }

        private async void OnValidUpdate()
        {
            if (_model.Password != "password")
            {
                SelectedUser.PasswordHash = _userManager.PasswordHasher.HashPassword(SelectedUser, _model.Password);
            }
            SelectedUser.UserName = _model.Username;
            SelectedUser.CanUserChangePassword = _model.CanUserChangePassword;
            SelectedUser.Email = _model.Email;
            SelectedUser.Roles = new string[1] { _model.Group };
            var result = await _userManager.UpdateAsync(SelectedUser);
            if (result.Succeeded)
            {
                _alertManager.addAlert("success", "User succesfully updated!");
                TcoAppDomain.Current.Logger.Information($"User '{SelectedUser.UserName}' updated. {{@sender}}", new { UserName = SelectedUser.UserName, Group = SelectedUser.Roles });
            }
            else
            {
                _alertManager.addAlert("warning", "User was not updated!");
            }
        }

        protected override void OnInitialized()
        {
            _model = new RegisterUserModel();
        }
    }
}
