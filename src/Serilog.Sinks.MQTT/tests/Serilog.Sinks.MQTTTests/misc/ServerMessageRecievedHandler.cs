using MQTTnet;
using MQTTnet.Client.Receiving;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serilog.Sinks.MQTTTests
{
    internal class ServerMessageRecievedHandler : IMqttApplicationMessageReceivedHandler
    {
        public List<MqttApplicationMessage> ApplicationMessages { get; } = new List<MqttApplicationMessage>();

        public Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            return Task.Run(() => ApplicationMessages.Add(eventArgs.ApplicationMessage));
        }
    }
}