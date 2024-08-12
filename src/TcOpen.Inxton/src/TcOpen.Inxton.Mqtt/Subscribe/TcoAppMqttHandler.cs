using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client.Receiving;

namespace TcOpen.Inxton.Mqtt
{
    public class TcoAppMqttHandler : IMqttApplicationMessageReceivedHandler
    {
        private readonly ConcurrentDictionary<
            string,
            IMqttApplicationMessageReceivedHandler
        > _subHandlers;
        public IEnumerable<string> TopicNames => _subHandlers.Keys;

        public TcoAppMqttHandler()
        {
            _subHandlers =
                new ConcurrentDictionary<string, IMqttApplicationMessageReceivedHandler>();
        }

        public async Task HandleApplicationMessageReceivedAsync(
            MqttApplicationMessageReceivedEventArgs eventArgs
        )
        {
            var topic = eventArgs.ApplicationMessage.Topic;
            if (_subHandlers.ContainsKey(topic))
                await _subHandlers[eventArgs.ApplicationMessage.Topic]
                    .HandleApplicationMessageReceivedAsync(eventArgs);
        }

        public bool TryAdd(string topic, IMqttApplicationMessageReceivedHandler handler)
        {
            return _subHandlers.TryAdd(topic, handler);
        }

        public bool TryRemove(string topic)
        {
            return _subHandlers.TryRemove(topic, out _);
        }
    }
}
