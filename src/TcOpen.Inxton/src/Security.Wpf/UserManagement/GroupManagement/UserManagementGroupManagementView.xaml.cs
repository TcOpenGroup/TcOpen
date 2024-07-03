using System.ComponentModel;
using System.Windows.Controls;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    /// <summary>
    /// Interaction logic for fbDataExchangeControlView.xaml
    /// </summary>
    public partial class UserManagementGroupManagementView : UserControl
    {
        public UserManagementGroupManagementView()
        {
            //if (! DesignerProperties.GetIsInDesignMode(this))
            //{
            this.DataContext = new UserManagementViewModel();
            //}

            InitializeComponent();
        }
    }
}
