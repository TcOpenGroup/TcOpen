using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using TcOpen.Inxton.Local.Security;
using TcOpen.Inxton.Local.Security.Blazor.Users;

namespace TcOpen.Inxton.Local.Security.Blazor
{
    public class BlazorAlertManager
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public BlazorAlertManager(IJSRuntime jsRuntime)
        {
            moduleTask = new(
                () =>
                    jsRuntime
                        .InvokeAsync<IJSObjectReference>(
                            "import",
                            "./_content/TcOpen.Inxton.Local.Security.Blazor/BlazorAlert.js"
                        )
                        .AsTask()
            );
        }

        public async void addAlert(string type, string message)
        {
            var module = await moduleTask.Value;

            await module.InvokeAsync<string>("alert", new string[] { message, type });
        }
    }
}
