using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDialog
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class JsInteropDialog
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public JsInteropDialog(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/TcOpen.Inxton.TcoCore.Blazor/TcoDialog.js").AsTask());
        }

        public async ValueTask<string> ShowTcoDialog()
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("OpenTcoDialog");
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
