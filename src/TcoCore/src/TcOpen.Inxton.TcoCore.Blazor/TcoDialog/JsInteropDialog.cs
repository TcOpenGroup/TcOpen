﻿using Microsoft.JSInterop;
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
        public bool firstRenderComplete;
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public JsInteropDialog(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/TcOpen.Inxton.TcoCore.Blazor/TcoDialog.js").AsTask());
        }

        public async ValueTask<string> ShowTcoDialog(string dialogId)
        {
            var module = await moduleTask.Value;
            //var id = "#TcoDialogDialogView";
            var id =  dialogId;
            return await module.InvokeAsync<string>("OpenTcoDialog", id);
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