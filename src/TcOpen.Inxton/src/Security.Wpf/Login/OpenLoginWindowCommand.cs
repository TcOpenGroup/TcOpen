using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    public class OpenLoginWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                var lw = new LoginWindow();
                lw.ShowDialog();
            });
        }
    }
}
