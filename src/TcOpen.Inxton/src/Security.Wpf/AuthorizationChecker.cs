using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    /// <summary>
    /// Uses <see cref="TcOpen.Inxton.Security.AuthorizationChecker"/>, but will also display a dialog window
    /// prompting the user to enter credentials in WPF applications
    /// <code>
    ///
    ///  if (!Vortex.Framework.Security.Wpf.AuthorizationChecker.HasAuthorizationWithLoginDialogue(Roles.data_exchange_view_can_user_add_record))
    ///  {
    ///
    ///     return;
    ///  }
    ///  // the code for authorized user.
    /// </code>
    /// </summary>
    public static class AuthorizationChecker
    {
        public static bool HasAuthorizationWithLoginDialogue(string roles)
        {
            if (!TcOpen.Inxton.Local.Security.AuthorizationChecker.HasAuthorization(roles))
            {
                var loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            }

            return TcOpen.Inxton.Local.Security.AuthorizationChecker.HasAuthorization(roles);
        }
    }
}
