using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TcoCore;
using TcOpen.Inxton.App.Dialogs;
using TcOpen.Inxton.Dialogs;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcOpen.Inxton.TcoCore.Wpf
{
    /// <summary>
    /// Provides management for the PLC dialogs.
    /// </summary>
    public class DialogProxyServiceWpf : DialogProxyServiceBase
    {
        protected DialogProxyServiceWpf(IEnumerable<IVortexObject> observedObjects) : base(observedObjects)
        {

        }

        protected async override void Queue(IsDialog dialog)
        {
            dialog.Read();

            await TcoAppDomain.Current.Dispatcher.InvokeAsync(
                () =>
                {
                    var view = Renderer.Get.GetView("Dialog", dialog.GetType());
                    var viewInstance = Activator.CreateInstance(view);
                    var viewModel = Renderer.Get.GetViewModel("Dialog", dialog.GetType(), dialog);
                    var win = viewInstance as Window;
                    if (win != null)
                    {
                        win.DataContext = viewModel;
                        win.Show();
                    }                                        
                }
                );
        }

        /// <summary>
        /// Creates observer for plc dialogs for given objects.
        /// </summary>
        /// <param name="observedObjects">Object for which the dialogs must be handled in this application.</param>
        /// <returns></returns>
        public static DialogProxyServiceBase Create(IEnumerable<IVortexObject> observedObjects)
        {
            var dialogProxyService = new DialogProxyServiceWpf(observedObjects);
            return dialogProxyService;
        }
}
}
