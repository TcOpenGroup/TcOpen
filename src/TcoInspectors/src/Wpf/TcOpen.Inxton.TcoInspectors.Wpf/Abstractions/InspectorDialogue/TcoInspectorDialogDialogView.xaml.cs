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
using System.Windows.Shapes;
using TcoInspectors;

namespace TcoInspectors
{
    /// <summary>
    /// Interaction logic for InspectorDialogueWindow.xaml
    /// </summary>
    public partial class TcoInspectorDialogDialogView : Window
    {
        public TcoInspectorDialogDialogView()
        {
            InitializeComponent();
            this.DataContextChanged += TcoInspectorDialogView_DataContextChanged;

        }


        private void TcoInspectorDialogView_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            var context = this.DataContext as TcoInspectorDialogDialogViewModel;
            if (context != null)
            {
                context.CloseRequestEventHandler += (s, ev) => this.Close();
            }
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_TouchDown(object sender, TouchEventArgs e)
        {
            DragMove();
        }
    }    
}
