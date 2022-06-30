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

namespace TcOpen.Inxton.TcoIo.Wpf.Diagnostics.EtherCAT.Display
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class StringDisplay
    {
        public StringDisplay()
        {
            InitializeComponent();
            DataContextChanged += DataContextChange;
        }
        private void DataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            Binding binding = tbStringDisplay.GetBindingExpression(TextBox.TextProperty).ParentBinding;

            var formatString = DataContext as string;
            if (string.IsNullOrEmpty(formatString)) return;
            var b = new Binding
            {
                Source = DataContext,
                Mode = binding.Mode,
                StringFormat = formatString
            };
            tbStringDisplay.SetBinding(TextBox.TextProperty, b);
        }

    }
}
