﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using TcOpen.Inxton.Abstractions.Security;
using TcOpen.Inxton.Security.Wpf.Dialog;
using TcOpen.Inxton.Security.Wpf.Internal;

namespace TcOpen.Inxton.Security.Wpf
{
    public class AllUsersViewModel : BaseUserViewModel
    {

        public ObservableCollection<UserData> AllUsersFiltered { get; set; }
        public ObservableCollection<String> AllRolesFiltered { get; set; }

        private UserData _selectedUser;
        public UserData SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                SetProperty(ref _selectedUser, value);
                FilterRoles();
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

        public RelayCommand AvailableToCurrentRoleCommand { get; private set; }
        public RelayCommand CurrentToAvailbleRoleCommand { get; private set; }
        public RelayCommand AddMultipleRolesCommand { get; private set; }
        public RelayCommand StarEditUserCommand { get; private set; }
        public RelayCommand DeleteUserCommand { get; private set; }
        public RelayCommand AddRoleCommand { get; private set; }
        public RelayCommand RemoveRoleCommand { get; private set; }
        public RelayCommand RequestTokenChangeCommand     { get; private set; }  


        public AllUsersViewModel() : base()
        {
            AllUsersFiltered = new ObservableCollection<UserData>();
            AllRolesFiltered = new ObservableCollection<string>();

            AvailableToCurrentRoleCommand = new RelayCommand(role  => AddRole(role as String)    , p => true);
            CurrentToAvailbleRoleCommand  = new RelayCommand(role  => RemoveRole(role as String) , p => true);
            AddMultipleRolesCommand       = new RelayCommand(group => AddGroup(group as string)  , x => SelectedUser != null);
            StarEditUserCommand           = new RelayCommand(pswd  => UpdateUser(pswd as Pwds)   , p => true);
            DeleteUserCommand             = new RelayCommand(pswd  => DeleteUser()               , p => SelectedUser != null);
            AddRoleCommand                = new RelayCommand(roles => AddRole(roles)             , p => SelectedUser != null);
            RemoveRoleCommand             = new RelayCommand(roles => RemoveRole(roles)          , p => SelectedUser != null);           
            Populate();
            BaseUserViewModel.NewUserAdded += Refresh;
        }

        

        private void AddRole(object roles) => (roles as ObservableCollection<object>)
            .OfType<String>()
            .ToList()
            .ForEach(AddRole);

        private void RemoveRole(object roles) => (roles as ObservableCollection<object>)
            .OfType<String>()
            .ToList()
            .ForEach(RemoveRole);

        private void Refresh(object sender, EventArgs e) => Populate();

        private void DeleteUser()
        {
            if (_messageBoxService.ShowMessage(Properties.strings.AreYouSure,Properties.strings.Delete, MessageType.YesNo))
                ActionRunner.Runner.Execute(() =>
                {
                    UserRepositary.Delete(this.SelectedUser.Username);
                    UsersChanged();
                    Populate();
                });
        }

        private void UpdateUser(Pwds pwds) => ActionRunner.Runner.Execute(() =>
        {            
            if(!(String.IsNullOrEmpty(pwds.Pb1.Password) && String.IsNullOrEmpty(pwds.Pb2.Password)))
                SelectedUser.SetPlainTextPassoword(pwds.Pb1.Password);
            UserRepositary.Update(this.SelectedUser.Username, SelectedUser);
            pwds.Pb1.Clear();
            pwds.Pb2.Clear();
            UsersChanged();
            Populate();
        });


        private void AddRole(string v)
        {
            if (!SelectedUser.Roles.Contains(v))
            {
                SelectedUser.Roles.Add(v);
                FilterRoles();
            }
            else
            {
                SelectedUser.Roles.Remove(v);
               FilterRoles();
            }
        }

        private void RemoveRole(string v)
        {
            SelectedUser.Roles.Remove(v);
            FilterRoles();
        }


        private void AddGroup(string group) =>
            AllRoles.Where(role => role.DefaultGroup == group)
                .Select(   role => role.Name)
                .ToList()
                .ForEach(  role => AddRole(role));

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

        private void FilterRoles()
        {
            if (SelectedUser != null)
            {
                AllRolesFiltered.Clear();
                AllRoles
                    .Select(x => x.Name)
                    .Except(SelectedUser.Roles)
                    .ToList()
                    .ForEach(AllRolesFiltered.Add);
            }
        }

        #endregion List filtering
    }
}