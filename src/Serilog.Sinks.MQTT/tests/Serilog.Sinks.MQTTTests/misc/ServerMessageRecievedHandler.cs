using System.Collections.Generic;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client.Receiving;

namespace Serilog.Sinks.MQTTTests
{
    internal class ServerMessageRecievedHandler : IMqttApplicationMessageReceivedHandler
    {
        public List<MqttApplicationMessage> ApplicationMessages { get; } =
            new List<MqttApplicationMessage>();

        public Task HandleApplicationMessageReceivedAsync(
            MqttApplicationMessageReceivedEventArgs eventArgs
        )
        {
            return Task.Run(() => ApplicationMessages.Add(eventArgs.ApplicationMessage));
        }
    }
}
