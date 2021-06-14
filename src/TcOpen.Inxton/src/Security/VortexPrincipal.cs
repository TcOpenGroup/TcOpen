using System.Linq;
using System.Security.Principal;

namespace TcOpen.Inxton.Security
{
    public partial class VortexIdentity
    {
        public class VortexPrincipal : IPrincipal
        {
            private VortexIdentity _identity;

            public VortexIdentity Identity
            {
                get { return _identity ?? new AnonymousIdentity(); }
                set { _identity = value; }
            }

            #region IPrincipal Members
            IIdentity IPrincipal.Identity
            {
                get { return this.Identity; }
            }

            public bool IsInRole(string role)
            {
                return _identity.Roles.Contains(role);
            }
            #endregion
        }
    }
}
