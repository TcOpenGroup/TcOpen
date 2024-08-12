using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    /// <summary>
    /// Interaction logic for fbDataExchangeControlView.xaml
    /// </summary>
    public partial class AllGroupsGroupManagementView : IDisposable
    {
        public AllGroupsGroupManagementView()
        {
            InitializeComponent();
            GroupsList.SelectionChanged += SelectionChanged;
        }

        ICollectionView AvailibleRolesCollectionView { get; set; }
        ICollectionView GroupRolesCollectionView { get; set; }

        private void SelectionChanged(object sender, RoutedEventArgs e)
        {
            AvailibleRolesCollectionView = CollectionViewSource.GetDefaultView(
                AvailibleRoles.ItemsSource
            );
            GroupRolesCollectionView = CollectionViewSource.GetDefaultView(
                CurrentRoles.ItemsSource
            );
            AvailibleRolesCollectionView.Filter = AvailibleRolesFilter;
            if (GroupRolesCollectionView != null)
                GroupRolesCollectionView.Filter = UserRolesFilter;
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

        private bool AvailibleRolesFilter(object obj) =>
            RoleFilter(obj, AvailableRolesTextFilter.Text);

        private void AvailableRolesTextFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            AvailibleRolesCollectionView?.Refresh();
            GroupRolesCollectionView?.Refresh();
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
            GroupsList.SelectionChanged -= SelectionChanged;
        }
    }
}
