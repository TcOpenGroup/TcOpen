using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using TcOpen.Inxton.Input;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    public class AddNewUserGroupManagementViewModel : BaseUserViewModel
    {
        public UserData NewUser { get; set; }

        public RelayCommand StartCreateNewCommand { get; private set; }

        public AddNewUserGroupManagementViewModel()
            : base()
        {
            StartCreateNewCommand = new RelayCommand(
                pswd => CreateNew((Pwds)pswd),
                x => IsCreationAllowed(x)
            );

            NewUser = new UserData() { Email = string.Empty };

            NewUser.PropertyChanged += NewUser_PropertyChanged;
        }

        private void NewUser_PropertyChanged(
            object sender,
            System.ComponentModel.PropertyChangedEventArgs e
        )
        {
            StartCreateNewCommand.RaiseCanExecuteChanged();
        }

        public List<string> Groups
        {
            get
            {
                List<string> groups = new List<string>();
                SecurityManager.RoleGroupManager.GetAllGroup().ForEach(a => groups.Add(a.Name));
                return groups;
            }
        }
        public string SelectedGroup { get; set; }

        private bool IsCreationAllowed(object x) => !string.IsNullOrEmpty(NewUser?.Username);

        private void CreateNew(Pwds pwds)
        {
            try
            {
                if (pwds.Pb1.Password != pwds.Pb2.Password)
                    throw new Exception("Passwords do not match");
                if (!emailMatch.IsMatch(NewUser.Email) && !String.IsNullOrEmpty(NewUser.Email))
                    throw new Exception("Email does not have required format");

                NewUser.SetPlainTextPassword(pwds.Pb1.Password);
                NewUser._Created = DateTime.Now;
                NewUser._Modified = DateTime.Now;
                if (string.IsNullOrEmpty(SelectedGroup))
                    throw new Exception("Assign group first");
                NewUser.Roles = new ObservableCollection<string>(new string[] { SelectedGroup });
                NewUser.Level = SelectedGroup;
                UserRepository.Create(NewUser.Username, NewUser);

                TcoAppDomain.Current.Logger.Information(
                    $"New user '{NewUser.Username}' created. {{@sender}}",
                    new { UserName = NewUser.Username, Roles = NewUser.Roles }
                );

                UsersChanged();
                ClearFields(pwds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error creating new user: '{ex.Message}'",
                    "Failed to create user",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        private void ClearFields(Pwds p)
        {
            NewUser.Username = string.Empty;
            NewUser.Email = string.Empty;
            NewUser.CanUserChangePassword = false;
            NewUser.Roles.Clear();
            p.Pb1.Password = string.Empty;
            p.Pb2.Password = string.Empty;
        }

        private void HashPassword(UserData user, string password)
        {
            user.HashedPassword =
                TcOpen.Inxton.Local.Security.SecurityManager.Manager.Service.CalculateHash(
                    password,
                    $"{user.Username}"
                );
        }

        private readonly Regex emailMatch = new Regex(@".+@.+\..+");
    }
}
