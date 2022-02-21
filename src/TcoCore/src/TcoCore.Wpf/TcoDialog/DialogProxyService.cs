using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TcoCore;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcOpen.Inxton.TcoCore.Wpf
{
    public class DialogProxyService : DialogProxyServiceBase
    {
        protected DialogProxyService(IVortexObject observedObject) : base(observedObject)
        {

        }

        protected async override void Queue(TcoDialogBase dialog)
        {
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

        public static DialogProxyServiceBase Create(IVortexObject tcoObject)
        {
            var dialogProxyService = new DialogProxyService(tcoObject);
            return dialogProxyService;
        }
}
}
