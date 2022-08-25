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
        private static readonly Dictionary<string, string> userLookup = new Dictionary<string, string>();
        public HubConnection hubConnection { get; set; }

        public async Task SendDialogOpen(string username, string message)
        {
            await Clients.All.SendAsync(DialogMessages.RECEIVE_DIALOG_OPEN, username, message);
        }
        public async Task SendDialogClose(string username, string message)
        {
            await Clients.All.SendAsync(DialogMessages.RECEIVE_DIALOG_CLOSE, username, message);
        }
        //public async Task SendDialogOpen(string groupName)
        //{
        //    await Clients.All.SendAsync("ReceiveDialogOpen");
        //}
        //public async Task SendClose(string groupName)
        //{
        //    await Clients.All.SendAsync("ReceiveClose");
        //}

        public Task JoinGroup(string groupName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        public Task LeaveGroup(string groupName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Connected");
            return base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine("disconnected");
            await base.OnDisconnectedAsync(e);
        }
    }
}
