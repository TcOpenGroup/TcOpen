using System;
using System.Collections.ObjectModel;
using System.Linq;
using TcOpen.Inxton.Security;
using TcOpen.Inxton.Input;
using System.Windows;
using System.Collections.Generic;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    public class AllUsersGroupManagementViewModel : BaseUserViewModel
    {
        public ObservableCollection<UserData> AllUsersFiltered { get; set; }

        private UserData _selectedUser;
        void UpateCommands()
        {
            StarEditUserCommand.RaiseCanExecuteChanged();
            DeleteUserCommand.RaiseCanExecuteChanged();
        }
        public UserData SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                SetProperty(ref _selectedUser, value);
                UpateCommands();
            }
        }

        private string _allUsersFilterQuery;
        public string AllUsersFilterQuery
        {
            get => _allUsersFilterQuery;
            set
            {
                _allUsersFilterQuery = value;
                FilterUserList();
            }
        }
        public List<string> Groups
        {
            get {
                List<string> groups = new List<string>();
                SecurityManager.RoleGroupManager.GetAllGroup().ForEach(a => groups.Add(a.Name));
                return groups;
            }
        }

        public RelayCommand StarEditUserCommand { get; private set; }
        public RelayCommand DeleteUserCommand { get; private set; }
        public RelayCommand RequestTokenChangeCommand { get; private set; }


        public AllUsersGroupManagementViewModel() : base()
        {
            AllUsersFiltered = new ObservableCollection<UserData>();
            StarEditUserCommand = new RelayCommand(pswd => UpdateUser(pswd as Pwds), p => true);
            DeleteUserCommand = new RelayCommand(pswd => DeleteUser(), p => SelectedUser != null);
            Populate();
            BaseUserViewModel.OnNewUserAdded += Refresh;

        }

        private void Refresh(object sender, EventArgs e) => Populate();

        private void DeleteUser()
        {
            if (_messageBoxService.ShowMessage(Properties.strings.AreYouSure, Properties.strings.Delete, MessageType.YesNo))
            {
                UserRepository.Delete(this.SelectedUser.Username);
                TcoAppDomain.Current.Logger.Information($"User '{this.SelectedUser.Username}' deleted. {{@sender}}", new { UserName = this.SelectedUser.Username });
                UsersChanged();
                Populate();
            }
        }

        private void UpdateUser(Pwds pwds)
        {
            try
            {
                if (this.SelectedUser != null)
                {
                    if (!(String.IsNullOrEmpty(pwds.Pb1.Password) && String.IsNullOrEmpty(pwds.Pb2.Password)))
                        SelectedUser.SetPlainTextPassword(pwds.Pb1.Password);

                    if (pwds.Pb1.Password != pwds.Pb2.Password)
                        throw new Exception("Passwords do not match");

                    UserRepository.Update(this.SelectedUser.Username, SelectedUser);
                    TcoAppDomain.Current.Logger.Information($"User '{this.SelectedUser.Username}' updated. {{@sender}}", new { UserName = this.SelectedUser.Username, Roles = this.SelectedUser.Roles });
                    pwds.Pb1.Clear();
                    pwds.Pb2.Clear();
                    UsersChanged();
                    Populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating new user: '{ex.Message}'", "Failed to create user", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void Populate()
        {
            AllUsersFiltered.Clear();
            AllUsers.ToList().ForEach(AllUsersFiltered.Add);
        }
        private readonly IMessageBoxService _messageBoxService = new WPFMessageBoxService();
        #region List filtering

        private void FilterUserList()
        {
            AllUsersFiltered.Clear();
            if (AllUsersFilterQuery == null)
            {
                Populate();
            }
            else
            {
                AllUsers
                    .Where(u => UserFilter(u))
                    .ToList()
                    .ForEach(AllUsersFiltered.Add);
            }
        }

        private bool UserFilter(UserData u) => u.Username.ToLower().Contains(AllUsersFilterQuery.ToLower());

        #endregion List filtering
    }
}