using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TcOpen.Inxton.Security.Wpf.UserManagement
{
    /// <summary>
    /// Interaction logic for AddNewUserDialogView.xaml
    /// </summary>
    public partial class AddNewUserView : UserControl
    {
        public AddNewUserView()
        {
            InitializeComponent();
        }

        private double MaxRolesListBoxHeight = int.MinValue;
        private void AvailibleRoles_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.HeightChanged)
            {
                MaxRolesListBoxHeight = Math.Max(e.NewSize.Height, MaxRolesListBoxHeight);
                (sender as Control).MinHeight = MaxRolesListBoxHeight;
            }
        }
    }
}
