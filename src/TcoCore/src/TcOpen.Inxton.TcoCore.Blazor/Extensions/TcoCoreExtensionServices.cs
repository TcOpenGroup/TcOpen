using Blazm.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.TcoCore.Blazor.Extensions
{
    public static class TcoCoreExtensionServices
    {
        public static void AddTcoCoreExtensions(this IServiceCollection services)
        {
            services.AddBlazm();
        }
    }
}
