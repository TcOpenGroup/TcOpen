using System.ComponentModel;
using System.Windows.Controls;

namespace TcOpen.Inxton.Security.Wpf
{
    /// <summary>
    /// Interaction logic for fbDataExchangeControlView.xaml
    /// </summary>
    public partial class UserManagementView : UserControl
    {
        public UserManagementView()
        {
            //if (! DesignerProperties.GetIsInDesignMode(this))
            //{
                this.DataContext = new UserManagementViewModel();
            //}

            InitializeComponent();
        }
 
    }
}
