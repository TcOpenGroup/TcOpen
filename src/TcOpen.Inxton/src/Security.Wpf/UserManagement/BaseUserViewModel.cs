﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Security;
using Vortex.Presentation;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    public class BaseUserViewModel : BindableBase
    {
        private static IRepository<UserData>          _userRepositary;
        private static ObservableCollection<UserData> _allUsers;

        public IRepository<UserData> UserRepository     { get => _userRepositary; set => _userRepositary = value; }
        public ObservableCollection<UserData> AllUsers  { get => _allUsers;private set => _allUsers = value; }
        public List<Role> AllRoles                      { get; private set; } = new List<Role>();
        public List<string> AllGroups                          { get; private set; }
        public static event EventHandler OnNewUserAdded;
        
        public BaseUserViewModel()
        {
            if (Application.Current.MainWindow == null) return;
            if (_userRepositary == null)
                UserRepository  = SecurityManager.Manager.UserRepository;
            AllRoles            = SecurityManager.Manager.AvailableRoles.ToList();
            AllGroups           = SecurityManager.Manager.AvailableGroups().ToList();
            AllUsers            = new ObservableCollection<UserData>(UserRepository.Queryable.Where(p=> true));
        }





        protected void UsersChanged()
        {
            AllUsers.Clear();
            UserRepository.Queryable.Where(p => true).ToList().ForEach(AllUsers.Add);
            OnNewUserAdded?.Invoke(this, EventArgs.Empty);
        }
    }
}
