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
using TcOpen.Inxton.Local.Security.Wpf;

namespace TcoInspectors
{
    /// <summary>
    /// Interaction logic for InspectorDialogueWindow.xaml
    /// </summary>
    public partial class TcoInspectorDialogDialogView : Window, IDisposable
    {
        private TcoInspectorDialogDialogViewModel context;

        public TcoInspectorDialogDialogView()
        {
            InitializeComponent();
        
            this.DataContextChanged += TcoInspectorDialogView_DataContextChanged;

        }
    
        private void TcoInspectorDialogView_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            context = this.DataContext as TcoInspectorDialogDialogViewModel;
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
            this.CaptureTouch(e.TouchDevice);
        }

        public void Dispose()
        {
            this.DataContextChanged -= TcoInspectorDialogView_DataContextChanged;
            if (context != null)
            {
                PermissionBox.RemovePermissionBox(this.PermissionBoxOverrideCommand);
                context.CloseRequestEventHandler -= (s, ev) => this.Close();
            }
        }

        private void HostWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (webView != null)
            {
                webView.Dispose();
                webView = null;
            }
        }
    }
}