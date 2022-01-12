using MQTTnet;
using MQTTnet.Client.Receiving;
using System;
using System.Threading.Tasks;

namespace TcOpen.Inxton.MqttTests
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