using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    public class AuthorizationHandler
    {                 
        public static void SecurityExceptionHandler(FirstChanceExceptionEventArgs e)
        {
            ShowLoginWindow(e);
        }

        public static void ShowLoginWindow(FirstChanceExceptionEventArgs e)
        {
            try
            {
                Dispatcher.CurrentDispatcher.Invoke(() => {
                    if (e.Exception is SecurityException)
                    {
                        var secException = e.Exception as SecurityException;

                        var loginWindow = new LoginWindow($"We are sorry you need to authenticate to go ahead.");
                        loginWindow.ShowDialog();
                    }

                    if (e.Exception is UnauthorizedAccessException)
                    {
                        var secException = e.Exception as SecurityException;
                        System.Windows.MessageBox.Show("You are not authorized.");
                    }
                });
            }
            catch (TargetInvocationException)
            {

                // Ignore
            }
            catch
            {
                throw;
            }
            
        }
    }
}
