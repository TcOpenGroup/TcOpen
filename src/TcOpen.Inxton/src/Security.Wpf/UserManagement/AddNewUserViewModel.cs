using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using TcOpen.Inxton.Security;
using TcOpen.Inxton.Input;
using System.Windows;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    public class AddNewUserViewModel : BaseUserViewModel
    {
        public UserData NewUser                      { get; set; }
        public ObservableCollection<string> AllRolesFiltered { get; set; }
        
        public RelayCommand StartCreateNewCommand   { get; private set; }
        public RelayCommand AddRoleCommand          { get; private set; }
        public RelayCommand RemoveRoleCommand       { get; private set; }
        public RelayCommand AddRolesCommand { get; private set; }
        public RelayCommand RemoveRolesCommand { get; private set; }
        public RelayCommand AddMultipleRolesCommand { get; private set; }

        public AddNewUserViewModel() : base()
        {
            StartCreateNewCommand =     new RelayCommand(pswd => CreateNew((Pwds)pswd), x => IsCreationAllowed(x));
            AddRoleCommand =            new RelayCommand(role => AddRole(role as String), x => IsCreationAllowed(x));
            RemoveRoleCommand =         new RelayCommand(role => RemoveRole(role as String), x => IsCreationAllowed(x));
            AddRolesCommand =           new RelayCommand(role => AddRoles(role), x => IsCreationAllowed(x));
            RemoveRolesCommand =        new RelayCommand(role => RemoveRoles(role), x => IsCreationAllowed(x));
            AddMultipleRolesCommand =   new RelayCommand(group => AddMultipleRoles(group as string), x => IsCreationAllowed(x));

            NewUser                 =   new UserData() { Email = string.Empty };
            AllRolesFiltered        =   new ObservableCollection<string>(AllRoles?.Select(x => x.Name)?.ToList());

            NewUser.PropertyChanged += NewUser_PropertyChanged;
            
        }

        private void NewUser_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StartCreateNewCommand.RaiseCanExecuteChanged();
            AddRoleCommand             .RaiseCanExecuteChanged();
            RemoveRoleCommand          .RaiseCanExecuteChanged();
            AddRolesCommand            .RaiseCanExecuteChanged();
            RemoveRolesCommand         .RaiseCanExecuteChanged();
            AddMultipleRolesCommand.RaiseCanExecuteChanged();
        }

        private bool IsCreationAllowed(object x) => !string.IsNullOrEmpty(NewUser?.Username);

        private void AddRoles(object roles) => (roles as ObservableCollection<object>)
            .OfType<String>()
            .Where(role => !String.IsNullOrEmpty(role))
            .ToList()
            .ForEach(AddRole);

        private void RemoveRoles(object roles) => (roles as ObservableCollection<object>)
            .OfType<String>()
            .Where(role => !String.IsNullOrEmpty(role))
            .ToList()
            .ForEach(RemoveRole);

        private void AddRole(string v)
        {
            if (!NewUser.Roles.Contains(v))
            {
                NewUser.Roles.Add(v);
                AllRolesFiltered.Remove(v);
            }
            else
            {
                NewUser.Roles.Remove(v);
                AllRolesFiltered.Add(v);
            }
        }

        private void RemoveRole(string v)
        {
            NewUser.Roles.Remove(v);
            AllRolesFiltered.Add(v);
        }

        private void AddMultipleRoles(string v) =>
            AllRoles.Where(role => role.DefaultGroup == v)
                .Select(role => role.Name)
                .ToList()
                .ForEach(role => AddRole(role));

        private void CreateNew(Pwds pwds)
        {
            try
            {
                if (pwds.Pb1.Password != pwds.Pb2.Password)
                    throw new Exception("Passwords do not match");
                if (!emailMatch.IsMatch(NewUser.Email) && !String.IsNullOrEmpty(NewUser.Email))
                    throw new Exception("Email does not have required format");

                NewUser.SetPlainTextPassword(pwds.Pb1.Password);
                UserRepository.Create(NewUser.Username, NewUser);
                NewUser._Created = DateTime.Now;
                NewUser._Modified = DateTime.Now;

                TcoAppDomain.Current.Logger.Information($"New user '{NewUser.Username}' created. {{@sender}}", new { UserName = NewUser.Username, Roles = NewUser.Roles });

                UsersChanged();
                ClearFields(pwds);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating new user: '{ex.Message}'", "Failed to create user", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void ClearFields(Pwds p)
        {
            NewUser.Username              = string.Empty;
            NewUser.Email                 = string.Empty;
            NewUser.CanUserChangePassword = false;
            NewUser.Roles.Clear();
            p.Pb1.Password                = string.Empty;
            p.Pb2.Password                = string.Empty;
            
            AllRolesFiltered.ReplaceWith(AllRoles.Select(x => x.Name).ToList());
        }

        private void HashPassword(UserData user, string password)
        {
            user.HashedPassword = TcOpen.Inxton.Local.Security.SecurityManager.Manager.Service.CalculateHash(password, $"{user.Username}");
        }

        private readonly Regex emailMatch = new Regex(@".+@.+\..+");

    }
}
