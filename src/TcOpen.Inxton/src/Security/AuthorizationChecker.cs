using System;
using System.Linq;

namespace TcOpen.Inxton.Security
{
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
