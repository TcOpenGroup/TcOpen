using System;
using System.Linq;
using System.Windows;

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
        }

        private void TcoDialogBaseView_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            var context = this.DataContext as TcoDialogBaseViewModel;
            if (context != null)
            {
                context.CloseRequestEventHandler += (s, ev) => this.Close();
            }
        }
    }
}
