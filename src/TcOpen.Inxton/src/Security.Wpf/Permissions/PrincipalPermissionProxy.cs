using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    [ComVisible(true)]
    [Serializable]
    public sealed class PrincipalPermissionProxy : IPermission, IUnrestrictedPermission
    {
        private readonly PrincipalPermission _inner;
        public PrincipalPermissionProxy(PrincipalPermission inner)
        {
            _inner = inner;
        }

        public IPermission Copy()
        {
            return _inner.Copy();
        }

        public void Demand()
        {
            // NOTE here we check if we are running under designer and if so - ignore demand
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            _inner.Demand();
        }

        public void FromXml(SecurityElement e)
        {
            _inner.FromXml(e);
        }

        public IPermission Intersect(IPermission target)
        {
            return _inner.Intersect(target);
        }

        public bool IsSubsetOf(IPermission target)
        {
            return _inner.IsSubsetOf(target);
        }

        public bool IsUnrestricted()
        {
            return _inner.IsUnrestricted();
        }

        public SecurityElement ToXml()
        {
            return _inner.ToXml();
        }

        public IPermission Union(IPermission target)
        {
            return _inner.Union(target);
        }
    }
}

