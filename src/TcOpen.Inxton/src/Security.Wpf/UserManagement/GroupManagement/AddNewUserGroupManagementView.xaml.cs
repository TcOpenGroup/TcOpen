using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    /// <summary>
    /// Interaction logic for AddNewUserDialogView.xaml
    /// </summary>
    public partial class AddNewUserGroupManagementView : UserControl
    {
        public AddNewUserGroupManagementView()
        {
            InitializeComponent();
        }

        private void Groups_DropDownOpened(object sender, EventArgs e)
        {
            List<string> groups = new List<string>();
            SecurityManager.RoleGroupManager.GetAllGroup().ForEach(a => groups.Add(a.Name));
            Groups.ItemsSource = groups;
        }
    }
}
