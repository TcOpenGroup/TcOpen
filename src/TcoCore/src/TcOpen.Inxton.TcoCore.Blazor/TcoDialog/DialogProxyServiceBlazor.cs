using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using TcoCore;
using TcOpen.Inxton;
using TcOpen.Inxton.Dialogs;
using Vortex.Connector;
using Vortex.Presentation.Blazor.Services;
using Vortex.Presentation.Controls.Blazor.RenderableContent;

namespace TcoCore
{
    public delegate void Notify();

    public class DialogProxyServiceBlazor : DialogProxyServiceBase
    {
        public event Notify DialogInvoked;

        public DialogProxyServiceBlazor(IEnumerable<IVortexObject> observedObjects)
            : base(observedObjects)
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
            if (observedObjects == null || observedObjects.Count() == 0)
                return;
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
