using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using TcOpen.Inxton.Abstractions.Security;
using TcOpen.Inxton.Security.Wpf.Internal;
using TcOpen.Inxton.Security.Wpf.UserManagement;

namespace TcOpen.Inxton.Security.Wpf
{
    public class AddNewUserViewModel : BaseUserViewModel
    {

        public UserData NewUser                      { get; set; }
        public ObservableCollection<String> AllRolesFiltered { get; set; }
 

        public RelayCommand StartCreateNewCommand   { get; private set; }
        public RelayCommand AddRoleCommand          { get; private set; }
        public RelayCommand RemoveRoleCommand       { get; private set; }
        public RelayCommand AddRolesCommand { get; private set; }
        public RelayCommand RemoveRolesCommand { get; private set; }
        public RelayCommand AddMultipleRolesCommand { get; private set; }

        public AddNewUserViewModel() : base()
        {
            StartCreateNewCommand    = new RelayCommand(pswd  => CreateNew((Pwds)pswd)            , x => IsCreationAllowed(x));
            AddRoleCommand           = new RelayCommand(role  => AddRole(role as String)          , x => IsCreationAllowed(x));
            RemoveRoleCommand        = new RelayCommand(role  => RemoveRole(role as String)       , x => IsCreationAllowed(x));
            AddRolesCommand          = new RelayCommand(role  => AddRoles(role)                   , x => IsCreationAllowed(x));
            RemoveRolesCommand       = new RelayCommand(role  => RemoveRoles(role)                , x => IsCreationAllowed(x));
            AddMultipleRolesCommand  = new RelayCommand(group => AddMultipleRoles(group as string), x => IsCreationAllowed(x));

            NewUser                 = new UserData() { Email = string.Empty };
            AllRolesFiltered        = new ObservableCollection<String>(AllRoles.Select(x => x.Name).ToList());            
        }

        private bool IsCreationAllowed(object x) => !String.IsNullOrEmpty(NewUser.Username);

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
            ActionRunner.Runner.Execute(() =>
            {
                if (pwds.Pb1.Password != pwds.Pb2.Password)
                    throw new Exception("Passwords do not match");
                if (!emailMatch.IsMatch(NewUser.Email) && !String.IsNullOrEmpty(NewUser.Email))
                    throw new Exception("Email does not match format");

                NewUser.SetPlainTextPassoword(pwds.Pb1.Password);
                UserRepositary.Create(NewUser.Username, NewUser);
                UsersChanged();
            });
            ClearFields(pwds);
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
            user.HashedPassword = TcOpen.Inxton.Security.SecurityManager.Manager.Service.CalculateHash(password, $"{user.Username}");
        }

        private readonly Regex emailMatch = new Regex(@".+@.+\..+");

    }
}
