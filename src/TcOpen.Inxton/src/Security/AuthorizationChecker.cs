using System;
using System.Linq;

namespace TcOpen.Inxton.Security
{
    /// <summary>
    ///     User AuthorizationChecker to check wheter the current use has privilege to execute the code.
    ///     <code>
    ///         //Roles is a static class with defined roles as strings.
    ///         if (TcOpen.Inxton.Security.AuthorizationChecker.HasAuthorization.(Roles.data_exchange_view_can_update_record))
    ///         {
    ///             //execute the code if the user has the rights.
    ///         }
    ///         else 
    ///         {
    ///             // show a message that user doesn't have a privilage to do so.
    ///         }
    ///     </code>
    /// </summary>
    public static class AuthorizationChecker
    {

        private static bool IsAuthorized(string roles)
        {
            return roles.Split('|')
                        .Where(p => p != string.Empty)
                        .Select(p => p.ToLower())
                        .Intersect((SecurityManager.Manager.Principal.Identity as VortexIdentity).Roles.Select(role => role.ToLower()))
                        .Any() ? true : false;
        }

        public static bool HasAuthorization(string roles, Action notAuthorizedAction = null)
        {            
            if (!IsAuthorized(roles) && notAuthorizedAction != null)
            {
                try
                {
                    notAuthorizedAction.Invoke();
                }
                catch (Exception)
                {
                    //++ Ignore
                }
                
            }

            return IsAuthorized(roles);
        }
    }
}
