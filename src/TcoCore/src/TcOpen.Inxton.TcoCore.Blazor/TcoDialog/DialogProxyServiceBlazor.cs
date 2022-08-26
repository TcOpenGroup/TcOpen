using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Dialogs;
using Vortex.Connector;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Vortex.Presentation.Controls.Blazor.RenderableContent;
using Microsoft.AspNetCore.Components.Rendering;
using TcoCore;
using TcOpen.Inxton;
using Vortex.Presentation.Blazor.Services;

namespace TcoCore
{
    public delegate void Notify(); 
    public class DialogProxyServiceBlazor : DialogProxyServiceBase
    {
        public event Notify DialogInvoked;
        public DialogProxyServiceBlazor(IEnumerable<IVortexObject> observedObjects) : base(observedObjects)
        {
            UpdateDialogs(observedObjects);
        }
    
        public IsDialog DialogVortex { get; set; }
        protected override async void Queue(IsDialog dialog) 
        {
            dialog.Read();
            await Task.Run(() =>
            {
                DialogVortex = dialog;
                DialogVortex.Read();
            });
           
            OnProcessCompleted();

        }
        protected virtual void OnProcessCompleted()
        {
            //if ProcessCompleted is not null then call delegate
            DialogInvoked?.Invoke();
        }


        void UpdateDialogs(IEnumerable<IVortexObject> observedObjects)
        {
            if (observedObjects == null || observedObjects.Count() == 0) return;
            foreach (var observedObject in observedObjects)
            {
                foreach (var dialog in observedObject.GetDescendants<IsDialog>())
                {
                    dialog.Initialize(() => Queue(dialog));
                }
            }
            
        }

        public static DialogProxyServiceBlazor Create(IEnumerable<IVortexObject> observedObjects)
        {
            var dialogProxyService = new DialogProxyServiceBlazor(observedObjects);
            return dialogProxyService;
        }

    }
}
