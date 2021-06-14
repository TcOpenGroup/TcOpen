using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcOpen.Inxton.Security.Wpf
{
    public static class AuthorizationChecker
    {

        public static bool HasAuthorizationWithLoginDialogue(string roles)
        {
            if(!TcOpen.Inxton.Security.AuthorizationChecker.HasAuthorization(roles))
            {
                var loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            }

            return TcOpen.Inxton.Security.AuthorizationChecker.HasAuthorization(roles);
        }
    }
}
