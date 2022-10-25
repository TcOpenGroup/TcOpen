using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Permissions;
using TcOpen.Inxton.Input;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    public class AllGroupsGroupManagementViewModel
    {
        public ObservableCollection<GroupData> AllGroupsFiltered { get; set; }
        public ObservableCollection<String> AllRolesFiltered { get; set; }

        private GroupData _selectedGroup;
        void UpateCommands()
        {
            AvailableToCurrentRoleCommand.RaiseCanExecuteChanged();
            CurrentToAvailableRoleCommand.RaiseCanExecuteChanged();
            DeleteGroupCommand.RaiseCanExecuteChanged();
            CreateNewGroupCommand.RaiseCanExecuteChanged();
            AddRoleCommand.RaiseCanExecuteChanged();
            RemoveRoleCommand.RaiseCanExecuteChanged();

        }
        public GroupData SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                //SetProperty(ref _selectedGroup, value);
                FilterRoles();
                UpateCommands();
            }
        }

        public string NewGroupName{ get; set; }

        private string _allGroupsFilterQuery;
        public string AllGroupsFilterQuery
        {
            get => _allGroupsFilterQuery;
            set
            {
                _allGroupsFilterQuery = value;
                FilterGroupList();
            }
        }

        public RelayCommand AvailableToCurrentRoleCommand { get; private set; }
        public RelayCommand CurrentToAvailableRoleCommand { get; private set; }
        public RelayCommand DeleteGroupCommand { get; private set; }
        public RelayCommand CreateNewGroupCommand { get; private set; }
        public RelayCommand AddRoleCommand { get; private set; }
        public RelayCommand RemoveRoleCommand { get; private set; }


        public AllGroupsGroupManagementViewModel()
        {
            AllGroupsFiltered = new ObservableCollection<GroupData>();
            AllRolesFiltered = new ObservableCollection<string>();

            AvailableToCurrentRoleCommand = new RelayCommand(role => AddRole(role as String), p => true);
            CurrentToAvailableRoleCommand = new RelayCommand(role => RemoveRole(role as String), p => true);
            DeleteGroupCommand = new RelayCommand(pswd => DeleteGroup(), p => SelectedGroup != null);
            CreateNewGroupCommand = new RelayCommand(pswd => CerateNewGroup(), p => true);
            AddRoleCommand = new RelayCommand(roles => AddRole(roles), p => SelectedGroup != null);
            RemoveRoleCommand = new RelayCommand(roles => RemoveRole(roles), p => SelectedGroup != null);
            Populate();
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

        private void DeleteGroup()
        {
            if (_messageBoxService.ShowMessage(Properties.strings.AreYouSure, Properties.strings.Delete, MessageType.YesNo))
            {
                SecurityManager.RoleGroupManager.DeleteGroup(this.SelectedGroup.Name);
                TcoAppDomain.Current.Logger.Information($"Group '{this.SelectedGroup.Name}' deleted. {{@sender}}", new { UserName = this.SelectedGroup.Name });
                //GroupsChanged();
                Populate();
            }
        }

        private void CerateNewGroup()
        {
            if (NewGroupName != null && NewGroupName != "")
            {
                SecurityManager.RoleGroupManager.CreateGroup(NewGroupName);
                TcoAppDomain.Current.Logger.Information($"Group '{NewGroupName}' created. {{@sender}}", new { NewGroupName = NewGroupName });
                Populate();
            }
        }

        private void AddRole(string v)
        {
            if (!SelectedGroup.Roles.Contains(v))
            {
                SecurityManager.RoleGroupManager.AddRoleToGroup(SelectedGroup.Name, v);
                SelectedGroup.Roles.Add(v);
                FilterRoles();
            }
            else
            {
                SecurityManager.RoleGroupManager.RemoveRolesFromGroup(SelectedGroup.Name, new string[] { v });
                SelectedGroup.Roles.Remove(v);
                FilterRoles();
            }
        }

        private void RemoveRole(string v)
        {
            SecurityManager.RoleGroupManager.RemoveRolesFromGroup(SelectedGroup.Name, new string[] { v });
            SelectedGroup.Roles.Remove(v);
            FilterRoles();
        }

        public void Populate()
        {
            AllGroupsFiltered.Clear();
            SecurityManager.RoleGroupManager.GetAllGroup().ForEach(AllGroupsFiltered.Add);
        }

        private readonly IMessageBoxService _messageBoxService = new WPFMessageBoxService();
        #region List filtering

        private void FilterGroupList()
        {
            AllGroupsFiltered.Clear();
            if (AllGroupsFilterQuery == null)
            {
                Populate();
            }
            else
            {
                SecurityManager.RoleGroupManager.GetAllGroup().Where(u => GroupFilter(u))
                    .ToList()
                    .ForEach(AllGroupsFiltered.Add);
            }
        }

        private bool GroupFilter(GroupData u) => u.Name.ToLower().Contains(AllGroupsFilterQuery.ToLower());

        private void FilterRoles()
        {
            if (SelectedGroup != null)
            {
                AllRolesFiltered.Clear();
                SecurityManager.Manager.AvailableRoles.ToList()
                    .Select(x => x.Name)
                    .Except(SelectedGroup.Roles)
                    .ToList()
                    .ForEach(AllRolesFiltered.Add);
            }
        }

        #endregion List filtering
    }
}