using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TcoCore;
using TcOpen.Inxton;
using Vortex.Connector;

namespace TcOpen.Inxton.TcoCore
{
    public abstract class DialogProxyServiceBase
    {
        protected DialogProxyServiceBase(IVortexObject observedObject)
        {           
            UpdateDialogs(observedObject);
        }
        void UpdateDialogs(IVortexObject rootObject)
        {
            if (rootObject == null) return;

            foreach (var dialog in rootObject.GetDescendants<TcoDialogBase>())
            {
                dialog.Initialize(() => Queue(dialog));
            }
        }
        protected abstract void Queue(TcoDialogBase dialog);        
    }
}
