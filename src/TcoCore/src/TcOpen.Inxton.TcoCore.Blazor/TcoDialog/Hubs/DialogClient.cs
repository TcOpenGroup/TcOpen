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
        private HubConnection _hubConnection;
        private readonly string _username;
        private bool _isConnected = false;

        public DialogClient(string username, string siteUrl)
        {
            // check inputs
            //if (string.IsNullOrWhiteSpace(username))
            //    throw new ArgumentNullException(nameof(username));
            //if (string.IsNullOrWhiteSpace(siteUrl))
            //    throw new ArgumentNullException(nameof(siteUrl));
            // save username
            _username = username;
            // set the hub URL
            _hubUrl = siteUrl.TrimEnd('/') + HUBURL;
        }

        public async Task SendDialogOpen(string message)
        {
            // check we are connected
            //if (!_isConnected)
            //    throw new InvalidOperationException("Client not started");
            // send the message
            await _hubConnection.SendAsync(DialogMessages.SEND_DIALOG_OPEN, _username, message);
        }
        public async Task SendDialogClose(string message)
        {
            // check we are connected
            //if (!_isConnected)
            //    throw new InvalidOperationException("Client not started");
            // send the message
            await _hubConnection.SendAsync(DialogMessages.SEND_DIALOG_CLOSE, _username, message);
        }

        public async Task StartAsync()
        {
            if (!_isConnected)
            {
                // create the connection using the .NET SignalR client
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(_hubUrl)
                    .Build();
                Console.WriteLine("ChatClient: calling Start()");

                // add handler for receiving messages
                _hubConnection.On<string, string>(DialogMessages.RECEIVE_DIALOG_OPEN, (user, message) =>
                {
                    HandleReceiveMessage(user, message);
                });
                _hubConnection.On<string, string>(DialogMessages.RECEIVE_DIALOG_CLOSE, (user, message) =>
                {
                    HandleReceiveDialogClose(user, message);
                });

                // start the connection
                await _hubConnection.StartAsync();

                Console.WriteLine("ChatClient: Start returned");
                _isConnected = true;

                // register user on hub to let other clients know they've joined
                //await _hubConnection.SendAsync(Messages.REGISTER, _username);
            }
        }
        private void HandleReceiveMessage(string username, string message)
        {
            // raise an event to subscribers
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(username, message));
        }
        private void HandleReceiveDialogClose(string username, string message)
        {
            // raise an event to subscribers
            MessageReceivedDialogClose?.Invoke(this, new MessageReceivedEventArgs(username, message));
        }
        public event MessageReceivedEventHandler MessageReceived;
        public event MessageReceivedEventHandler MessageReceivedDialogClose;
        public async Task StopAsync()
        {
            if (_isConnected)
            {
                // disconnect the client
                await _hubConnection.StopAsync();
                // There is a bug in the mono/SignalR client that does not
                // close connections even after stop/dispose
                // see https://github.com/mono/mono/issues/18628
                // this means the demo won't show "xxx left the chat" since 
                // the connections are left open
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
                _isConnected = false;
                Console.WriteLine("hub connection disposed");
            }
        }
        public async ValueTask DisposeAsync()
        {
            Console.WriteLine("ChatClient: Disposing client");
            await StopAsync();
        }

    }
    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(string username, string message)
        {
            Username = username;
            Message = message;
        }

        /// <summary>
        /// Name of the message/event
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Message data items
        /// </summary>
        public string Message { get; set; }

    }
}
