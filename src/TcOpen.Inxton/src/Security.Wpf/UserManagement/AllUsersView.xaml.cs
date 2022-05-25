using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    /// <summary>
    /// Interaction logic for AllUsersView.xaml
    /// </summary>
    public partial class AllUsersView : UserControl, IDisposable
    {
        public AllUsersView()
        {
            InitializeComponent();
            UsersList.SelectionChanged += SelectionChanged;
        }

        ICollectionView AvailibleRolesCollectionView { get; set; }
        ICollectionView UserRolesCollectionView { get; set; }
        private void SelectionChanged(object sender, RoutedEventArgs e)
        {
            AvailibleRolesCollectionView = CollectionViewSource.GetDefaultView(AvailibleRoles.ItemsSource);
            UserRolesCollectionView = CollectionViewSource.GetDefaultView(CurrentRoles.ItemsSource);
            AvailibleRolesCollectionView.Filter = AvailibleRolesFilter;
            if(UserRolesCollectionView != null)
                UserRolesCollectionView.Filter = UserRolesFilter;
        }

        private bool RoleFilter(object roleNameTocompare, string filter)
        {
            if (roleNameTocompare is string roleName)
            {
                return roleName.ToLower().Contains(filter.ToLower());
            }
            return true;
        }

        private bool UserRolesFilter(object obj) => RoleFilter(obj, CurrentRolesTextFilter.Text);

        private bool AvailibleRolesFilter(object obj) => RoleFilter(obj, AvailableRolesTextFilter.Text);

        private void AvailableRolesTextFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            AvailibleRolesCollectionView?.Refresh();
            UserRolesCollectionView?.Refresh();
        }

        private void ClearAvailableFilter_Click(object sender, RoutedEventArgs e)
        {
            AvailableRolesTextFilter.Text = "";
        }

        private void ClearAssignedFilter_Click(object sender, RoutedEventArgs e)
        {
            CurrentRolesTextFilter.Text = "";
        }

        public void Dispose()
        {
            UsersList.SelectionChanged -= SelectionChanged;
        }
    }
}
