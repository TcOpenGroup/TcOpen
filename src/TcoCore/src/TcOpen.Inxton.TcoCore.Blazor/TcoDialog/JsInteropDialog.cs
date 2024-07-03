using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace TcoCore
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class JsInteropDialog
    {
        public bool firstRenderComplete;
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public JsInteropDialog(IJSRuntime jsRuntime)
        {
            moduleTask = new(
                () =>
                    jsRuntime
                        .InvokeAsync<IJSObjectReference>(
                            "import",
                            "./_content/TcOpen.Inxton.TcoCore.Blazor/TcoDialog.js"
                        )
                        .AsTask()
            );
        }

        public async ValueTask<bool> ShowTcoDialog(string dialogId)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<bool>("OpenTcoDialog", dialogId);
        }

        public async ValueTask<bool> HideTcoDialog(string dialogId)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<bool>("HideTcoDialog", dialogId);
        }

        public async ValueTask<bool> SendOpenAllDialogs()
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<bool>("ClickSend");
        }

        public async ValueTask<bool> SendCloseAllDialogs()
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<bool>("ClickSendClose");
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                if (firstRenderComplete == true)
                {
                    var module = await moduleTask.Value;
                    await module.DisposeAsync();
                }
            }
        }
    }
}
