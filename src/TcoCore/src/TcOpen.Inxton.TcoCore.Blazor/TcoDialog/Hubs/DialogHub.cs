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
        public async Task SendDialogOpen(string message)
        {
            await Clients.All.SendAsync(DialogMessages.RECEIVE_DIALOG_OPEN, message);
        }

        public async Task SendDialogClose(string message)
        {
            await Clients.All.SendAsync(DialogMessages.RECEIVE_DIALOG_CLOSE, message);
        }
    }
}
