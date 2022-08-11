using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlcHammer.Hmi.Blazor.Hubs
{
    public static class UserHandler
    {
        //public static HashSet<string> ConnectedIds = new HashSet<string>();
        public static ConcurrentDictionary<string, string> ConnectedIds = new ConcurrentDictionary<string, string>();
        public static int ConnectedCount { get; set; } = 0;
    }
    public class DialogHub : Hub
    {
        public HubConnection hubConnection { get; set; }
       
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendClose()
        {
            await Clients.All.SendAsync("ReceiveClose");
        }
        public override Task OnConnectedAsync()
        {
            UserHandler.ConnectedIds.TryAdd(Context.ConnectionId, "some value");
            UserHandler.ConnectedCount = UserHandler.ConnectedIds.Count();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string removedItem;
            UserHandler.ConnectedIds.TryRemove(Context.ConnectionId, out removedItem);
            UserHandler.ConnectedCount = UserHandler.ConnectedIds.Count();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
