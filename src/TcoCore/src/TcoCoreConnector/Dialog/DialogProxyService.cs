using System;
using System.Collections.Generic;
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
        protected DialogProxyServiceBase(IEnumerable<IVortexObject> observedObjects)
        {           
            UpdateDialogs(observedObjects);
        }
        void UpdateDialogs(IEnumerable<IVortexObject> observedObjects)
        {
            if (observedObjects == null || observedObjects.Count() ==  0) return;
            foreach (var observedObject in observedObjects)
            {
                foreach (var dialog in observedObject.GetDescendants<TcoDialogBase>())
                {
                    dialog.Initialize(() => Queue(dialog));
                }
            }
            
        }
        protected abstract void Queue(TcoDialogBase dialog);        
    }
}
