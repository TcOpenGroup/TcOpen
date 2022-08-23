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

namespace TcOpen.Inxton.TcoIo.Wpf.Diagnostics.EtherCAT
{
    /// <summary>
    /// Interaction logic for Generating.xaml
    /// </summary>
    public partial class Generating : Window
    {
        public Generating()
        {
            InitializeComponent();
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
