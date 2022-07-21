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
    public delegate void Notify();  // delegate
    public class DialogProxyServiceBlazor : DialogProxyServiceBase
    {
        private ComponentService componentService = new ComponentService();
        public event Notify DialogInitializationCompleted; // event
        
        public DialogProxyServiceBlazor(IEnumerable<IVortexObject> observedObjects) : base(observedObjects)
        {
            UpdateDialogs(observedObjects);
        }

        public TcoDialog Dialog { get; set; } = new TcoDialog();
        protected override async void Queue(IsDialog dialog) 
        {

            //dialog.GetType().FullName();
            //var buildedComponentName = $"{name}{presentationName}View";
            //componentService.GetComponent(buildedComponentName);
            //consider other types of dialog
            dialog.Read();
            try
            {
                Dialog = (TcoDialog)dialog;
            }
            catch 
            {
                //pass
            }
           
            OnProcessCompleted();

        }
        protected virtual void OnProcessCompleted() //protected virtual method
        {
            //if ProcessCompleted is not null then call delegate
            DialogInitializationCompleted?.Invoke();
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

    }
}
