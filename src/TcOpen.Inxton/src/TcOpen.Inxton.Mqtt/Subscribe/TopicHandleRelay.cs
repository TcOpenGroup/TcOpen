using System;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client.Receiving;

namespace TcOpen.Inxton.Mqtt
{
    public class TopicHandleRelay<T> : IMqttApplicationMessageReceivedHandler
    {
        public Action<T> TopicHandle;

        public string Topic { get; }
        public IPayloadDeserializer<T> Deserializer { get; }

        public TcoAppMqttHandler TcoAppHandler { get; }

        public TopicHandleRelay(
            string Topic,
            IPayloadDeserializer<T> Deserializer,
            TcoAppMqttHandler TcoAppHandler
        )
        {
            this.Topic = Topic;
            this.Deserializer = Deserializer;
            this.TcoAppHandler = TcoAppHandler;
            this.TcoAppHandler.TryAdd(Topic, this);
        }

        public Task HandleApplicationMessageReceivedAsync(
            MqttApplicationMessageReceivedEventArgs eventArgs
        )
        {
            var topic = eventArgs.ApplicationMessage.Topic;
            var payload = eventArgs.ApplicationMessage.ConvertPayloadToString();
            return Task.Run(() =>
            {
                T deserialized = Deserializer.Deserialize(payload);
                TopicHandle.Invoke(deserialized);
            });
        }

        public void AddHandle(Action<T> OnMessageRecieved)
        {
            TopicHandle = OnMessageRecieved;
        }

        public void Unsubscribe()
        {
            TopicHandle = null;
            TcoAppHandler.TryRemove(Topic);
        }
    }
}
