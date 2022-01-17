using MQTTnet.Client;
using MQTTnet.Client.Subscribing;
using MQTTnet.Client.Unsubscribing;
using System;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Mqtt
{
    public class TcoMqttSubscriber<T>
    {
        public string Topic { get; }
        public IMqttClient Client { get; }
        public TopicHandleRelay<T> MessageReceivedHandler { get; }
        private TcoAppMqttHandler TcoAppHandler => Client.ApplicationMessageReceivedHandler as TcoAppMqttHandler;

        public TcoMqttSubscriber(IMqttClient Client, string topic, IPayloadDeserializer<T> deserializer)
        {
            this.Topic = topic;
            this.Client = Client;
            if (TcoAppHandler is null)
                throw new ArgumentException($"The {nameof(IMqttClient)} needs to call UseApplicationMessageReceivedHandler(new {nameof(TcoAppMqttHandler)}())" +
                    $"before using {nameof(TcoMqttSubscriber<T>)}");
            MessageReceivedHandler = new TopicHandleRelay<T>(topic, deserializer, TcoAppHandler);
        }

        public TcoMqttSubscriber(IMqttClient Client, string topic)
            : this(Client, topic, new JsonStringPayloadDeserializer<T>()) { }

        public Task<MqttClientSubscribeResult> SubscribeAsync(Action<T> OnMessageRecieved)
        {
            var mqttClientSubscribeResult = Client.SubscribeAsync(Topic);
            MessageReceivedHandler.AddHandle(OnMessageRecieved);
            return mqttClientSubscribeResult;
        }

        public Task<MqttClientUnsubscribeResult> UnsubscribeAsync()
        {
            var mqttClientUnsubscribeResult = Client.UnsubscribeAsync(Topic);
            MessageReceivedHandler.Unsubscribe();
            return mqttClientUnsubscribeResult;
        }

    }
}