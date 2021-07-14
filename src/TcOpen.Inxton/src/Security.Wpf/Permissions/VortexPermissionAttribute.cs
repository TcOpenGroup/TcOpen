using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    [ComVisible(true)]
    [Serializable]
    internal sealed class VortexPermissionAttribute : CodeAccessSecurityAttribute
    {
        public string Name { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public bool Authenticated { get; set; } = true;

        public VortexPermissionAttribute(SecurityAction action)
            : base(action)
        {
            Console.WriteLine();
        }

        public override IPermission CreatePermission()
        {
            if (this.Unrestricted)
                return new PrincipalPermission(PermissionState.Unrestricted);

            return new PrincipalPermission(this.Name, this.Role, this.Authenticated);
        }
    }
}
