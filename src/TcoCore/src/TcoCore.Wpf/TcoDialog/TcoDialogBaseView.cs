using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TcoCore
{
    /// <summary>
    /// Interaction logic for TcoDialogBaseView_.xaml
    /// </summary>
    public partial class TcoDialogBaseView : Window
    {
        public TcoDialogBaseView()
        {
            this.DataContextChanged += TcoDialogBaseView_DataContextChanged;
            this.MouseLeftButtonDown += OnMouseLeftButtonDown;
            this.PreviewTouchDown += Window_TouchDown;
        }

        private void TcoDialogBaseView_DataContextChanged(
            object sender,
            System.Windows.DependencyPropertyChangedEventArgs e
        )
        {
            var context = this.DataContext as TcoDialogBaseViewModel;
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
    }
}
