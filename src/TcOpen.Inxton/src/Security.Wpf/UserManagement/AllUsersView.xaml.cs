using System;
using System.Windows;
using System.Windows.Controls;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    /// <summary>
    /// Interaction logic for AllUsersView.xaml
    /// </summary>
    public partial class AllUsersView : UserControl
    {
        public AllUsersView()
        {
            InitializeComponent();
        }

        private double MaxRolesListBoxHeight = int.MinValue;
        private void AvailibleRoles_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.HeightChanged)
            {
                MaxRolesListBoxHeight = Math.Max( e.NewSize.Height, MaxRolesListBoxHeight);
                (sender as Control).MinHeight = MaxRolesListBoxHeight;
            }
        }
    }
}
