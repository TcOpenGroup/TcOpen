using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    /// <summary>
    /// Interaction logic for AddNewUserDialogView.xaml
    /// </summary>
    public partial class AddNewUserView : UserControl
    {
        public AddNewUserView()
        {
            InitializeComponent();
            AllRolesCollectionView = CollectionViewSource.GetDefaultView(AllRoles.ItemsSource);
            AssignedRolesCollectionView = CollectionViewSource.GetDefaultView(AssignedRoles.ItemsSource);
            AllRolesCollectionView.Filter = AllRolesFilter;
            AssignedRolesCollectionView.Filter = AssignedRolesFilter;
        }

        ICollectionView AllRolesCollectionView { get; set; }
        ICollectionView AssignedRolesCollectionView { get; set; }

        private double MaxRolesListBoxHeight = int.MinValue;
        private void AvailibleRoles_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.HeightChanged)
            {
                MaxRolesListBoxHeight = Math.Max(e.NewSize.Height, MaxRolesListBoxHeight);
                (sender as Control).MinHeight = MaxRolesListBoxHeight;
            }
        }
        private void TextFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllRolesCollectionView?.Refresh();
            AssignedRolesCollectionView?.Refresh();
        }

        private void ClearAllRolesFilter_Click(object sender, RoutedEventArgs e)
        {
            AllRolesTextFilter.Text = "";
        }

        private void ClearAssignedFilter_Click(object sender, RoutedEventArgs e)
        {
            AssignedRolesTextFilter.Text = "";
        }
        private bool RoleFilter(object roleNameTocompare, string filter)
        {
            if (roleNameTocompare is string roleName)
            {
                return roleName.ToLower().Contains(filter.ToLower());
            }
            return true;
        }

        private bool AllRolesFilter(object obj) => RoleFilter(obj, AllRolesTextFilter.Text);

        private bool AssignedRolesFilter(object obj) => RoleFilter(obj, AssignedRolesTextFilter.Text);
    }
}
