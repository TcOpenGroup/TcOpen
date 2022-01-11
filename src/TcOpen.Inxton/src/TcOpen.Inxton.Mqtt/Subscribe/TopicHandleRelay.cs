using MQTTnet;
using MQTTnet.Client.Receiving;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Mqtt
{
    public class TopicHandleRelay<T> : IMqttApplicationMessageReceivedHandler
    {
        public IDictionary<string, Action<T>> TopicHandles;

        public Action<string> DefaultHandle { get; set; }

        public IPayloadDeserializer<T> Deserializer { get;}

        public TopicHandleRelay(IPayloadDeserializer<T> Deserializer)
        {
            this.Deserializer = Deserializer;
            TopicHandles = new Dictionary<string, Action<T>>();
            DefaultHandle = (msg) => Console.WriteLine(msg);
        }

        public Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            var topic = eventArgs.ApplicationMessage.Topic;
            var payload = eventArgs.ApplicationMessage.ConvertPayloadToString();
            if (TopicHandles.ContainsKey(eventArgs.ApplicationMessage.Topic))
            {
                return Task.Run(() =>
                {
                    T deserialized = Deserializer.Deserialize(payload);
                    TopicHandles[topic].Invoke(deserialized);
                });
            }
            else
            {
                return Task.Run(() => DefaultHandle($"{topic} : {payload}"));
            }
        }

        public void AddHandle(string topic, Action<T> OnMessageRecieved)
        {
            TopicHandles[topic] = OnMessageRecieved;
        }

        public void Unsubscribe(string topic)
        {
            TopicHandles.Remove(topic);
        }

    }
}