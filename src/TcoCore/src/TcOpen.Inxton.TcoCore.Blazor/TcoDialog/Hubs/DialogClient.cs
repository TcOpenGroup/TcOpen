using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDialog.Hubs
{
    public class DialogClient : IAsyncDisposable
    {
        public const string HUBURL = "/dialoghub";
        public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);
        private readonly string _hubUrl;
        public HubConnection _hubConnection;
        private bool _isConnected = false;

        public DialogClient(string siteUrl)
        {
            // set the hub URL
            _hubUrl = siteUrl.TrimEnd('/') + HUBURL;
        }

        public async Task SendDialogOpen(string message)
        {
            await _hubConnection.SendAsync(DialogMessages.SEND_DIALOG_OPEN, message);
        }
        public async Task SendDialogClose(string message)
        {
            await _hubConnection.SendAsync(DialogMessages.SEND_DIALOG_CLOSE, message);
        }

        public async Task StartAsync()
        {
            if (!_isConnected)
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(_hubUrl)
                    .Build();
                
                _hubConnection.On<string>(DialogMessages.RECEIVE_DIALOG_OPEN, (message) =>
                {
                    HandleReceiveMessage(message);
                });
                _hubConnection.On<string>(DialogMessages.RECEIVE_DIALOG_CLOSE, (message) =>
                {
                    HandleReceiveDialogClose(message);
                });

                // start the connection
                await _hubConnection.StartAsync();
                _isConnected = true;
            }
        }
        private void HandleReceiveMessage(string message)
        {
            // raise an event to subscribers
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message));
        }
        private void HandleReceiveDialogClose(string message)
        {
            // raise an event to subscribers
            MessageReceivedDialogClose?.Invoke(this, new MessageReceivedEventArgs(message));
        }
        public event MessageReceivedEventHandler MessageReceived;
        public event MessageReceivedEventHandler MessageReceivedDialogClose;
        public async Task StopAsync()
        {
            if (_isConnected && _hubConnection != null)
            {
                // disconnect the client
                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
                _isConnected = false;
            }
        }
        public async ValueTask DisposeAsync()
        {
            if (_isConnected)
            {
                await StopAsync();
            }
        }

    }
    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

    }
}
