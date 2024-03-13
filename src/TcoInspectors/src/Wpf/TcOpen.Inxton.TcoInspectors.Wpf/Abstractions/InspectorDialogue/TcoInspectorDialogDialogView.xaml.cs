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
    public partial class TcoInspectorDialogDialogView : Window, IDisposable
    {
        private TcoInspectorDialogDialogViewModel context;

        public TcoInspectorDialogDialogView()
        {
            InitializeComponent();
        
            this.DataContextChanged += TcoInspectorDialogView_DataContextChanged;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        //    // Calculate the desired window size (80% of the screen resolution)
        //    double screenWidth = SystemParameters.PrimaryScreenWidth;
        //    double screenHeight = SystemParameters.PrimaryScreenHeight;

        //    double desiredWidth = screenWidth * 0.9;
        //    double desiredHeight = screenHeight * 0.9;

        //    // Set the window size
        //    MaxWidth = desiredWidth;
        //    MaxHeight = desiredHeight;

        //    // Center the window on the screen
        //    Left = (screenWidth - Width) / 2;
        //    Top = (screenHeight - Height) / 2;
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
                context.CloseRequestEventHandler -= (s, ev) => this.Close();
            }
        }
    }
}