using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TcoCore;
using TcOpen.Inxton;
using TcOpen.Inxton.Dialogs;
using Vortex.Connector;

namespace TcOpen.Inxton
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
                foreach (var dialog in observedObject.GetDescendants<IsDialog>())
                {
                    dialog.Initialize(() => Queue(dialog));
                }
            }
            
        }
        protected abstract void Queue(IsDialog dialog);        
    }
}
