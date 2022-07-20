using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
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
        private BlazorGroupManager _groupManager { get; set; }

        private User SelectedUser { get; set; }
        private RegisterUserModel _model { get; set; }
        public bool IsUserUpdated { get; set; }

        public void RowClicked(User user)
        {
            SelectedUser = user;
            IsUserUpdated = false;
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
                IsUserUpdated = true;
            }
        }

        protected override void OnInitialized()
        {
            IsUserUpdated = false;
            _model = new RegisterUserModel();
        }
    }
}
