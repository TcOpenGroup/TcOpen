using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TcOpen.Inxton.Abstractions.Data;
using TcOpen.Inxton.Abstractions.Security;
using TcOpen.Inxton.Security.Wpf.Internal;

namespace TcOpen.Inxton.Security.Wpf
{
    public class BaseUserViewModel : BindableBase
    {
        private static IRepository<UserData>          _userRepositary;
        private static ObservableCollection<UserData> _allUsers;

        public IRepository<UserData> UserRepositary    { get => _userRepositary; set => _userRepositary = value; }
        public ObservableCollection<UserData> AllUsers { get => _allUsers; private set => _allUsers = value; }
        public List<Role> AllRoles                             { get; private set; }
        public List<string> AllGroups                          { get; private set; }
        public static event EventHandler NewUserAdded;
        
        public BaseUserViewModel()
        {
            if (Application.Current.MainWindow == null) return;
            if (_userRepositary == null)
                UserRepositary  = SecurityManager.Manager.Service.UserRepository;
            AllRoles            = SecurityManager.Manager.AvailableRoles.ToList();
            AllGroups           = SecurityManager.Manager.AvailableGroups().ToList();
            AllUsers            = new ObservableCollection<UserData>(UserRepositary.GetRecords());
        }

        protected void UsersChanged()
        {
            AllUsers.Clear();
            UserRepositary.GetRecords().ToList().ForEach(AllUsers.Add);
            NewUserAdded(this, EventArgs.Empty);
        }
    }
}
