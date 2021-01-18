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

namespace TcOpen
{
    /// <summary>
    /// Interaction logic for fbTaskView.xaml
    /// </summary>
    public partial class fbTaskView : UserControl
    {
        public fbTaskView()
        {
            InitializeComponent();
        }
    }

    public partial class fbTaskControlView : fbTaskView
    {
        public fbTaskControlView()
        {
            InitializeComponent();
        }
    }

    public partial class fbTaskDisplayView : fbTaskView
    {
        public fbTaskDisplayView()
        {
            InitializeComponent();
        }
    }
}
