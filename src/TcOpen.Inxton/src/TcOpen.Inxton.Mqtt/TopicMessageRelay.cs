using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Publishing;
using MQTTnet.Client.Receiving;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcOpen.Inxton.Mqtt
{

    public class TopicMessageRelay : IMqttApplicationMessageReceivedHandler
    {
        public IDictionary<string, Action<string>> TopicHandles;

        public Action<string> DefaultHandle { get; set; }
        public TopicMessageRelay()
        {
            TopicHandles = new Dictionary<string, Action<string>>();
            DefaultHandle = (msg) => Console.WriteLine(msg);
        }
        public Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            var topic = eventArgs.ApplicationMessage.Topic;
            var payload = eventArgs.ApplicationMessage.ConvertPayloadToString();
            if (TopicHandles.ContainsKey(eventArgs.ApplicationMessage.Topic))
            {
                return Task.Run(() => TopicHandles[topic].Invoke(payload));
            }
            else
            {
                return Task.Run(() => DefaultHandle($"{topic} : {payload}"));
            }
        }

        public void Subscribe(string topic, Action<string> OnMessageRecieved)
        {
            TopicHandles[topic] = OnMessageRecieved;
        }

        public void Unsubscribe(string topic)
        {
            TopicHandles.Remove(topic);
        }
    }
}
