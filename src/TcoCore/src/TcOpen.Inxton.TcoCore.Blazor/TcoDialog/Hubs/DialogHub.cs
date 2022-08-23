using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDialog.Hubs
{
    public class DialogHub : Hub
    {
        public HubConnection hubConnection { get; set; }

        public async Task SendDialogOpen()
        {
            await Clients.All.SendAsync("ReceiveDialogOpen");
        }
        public async Task SendClose()
        {
            await Clients.All.SendAsync("ReceiveClose");
        }
    }
}
