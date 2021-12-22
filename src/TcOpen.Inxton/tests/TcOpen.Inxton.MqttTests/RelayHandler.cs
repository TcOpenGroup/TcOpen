using MQTTnet;
using MQTTnet.Client.Receiving;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using TcOpen.Inxton.Mqtt;

namespace TcOpen.Inxton.Logging.Tests
{

    class RelayHandler : IMqttApplicationMessageReceivedHandler
    {
        public Func<MqttApplicationMessageReceivedEventArgs, string> OnMessage;
        public bool MessageNotDelivered { get; private set; } = true;

        public RelayHandler(Func<MqttApplicationMessageReceivedEventArgs, string> onMessage)
        {
            OnMessage = onMessage;
        }

        public Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            if (MessageNotDelivered)
            {
                MessageNotDelivered = false;
                return Task.FromResult(OnMessage(eventArgs));
            }
            else
                throw new Exception("Only one message per handler");
        }


    }
}