using MQTTnet.Client;
using MQTTnet.Client.Receiving;
using MQTTnet.Client.Subscribing;
using MQTTnet.Client.Unsubscribing;
using System;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Mqtt
{
    public class TcoMqttSubscriber<T> 
    {
        public IMqttClient Client { get; }

        public IMqttApplicationMessageReceivedHandler MessageReceivedHandler { get; }

        public TcoMqttSubscriber(IMqttClient Client, IPayloadDeserializer<T> deserializer)
        {
            this.Client = Client;
            MessageReceivedHandler = new TopicHandleRelay<T>(deserializer);
            Client.UseApplicationMessageReceivedHandler(MessageReceivedHandler);
        }

        public Task<MqttClientSubscribeResult> SubscribeAsync(string topic, Action<T> OnMessageRecieved)
        {
            var mqttClientSubscribeResult = Client.SubscribeAsync(topic);
            if (MessageReceivedHandler is TopicHandleRelay<T> topicMessageRelay)
                topicMessageRelay.Subscribe(topic, OnMessageRecieved);
            return mqttClientSubscribeResult;
        }

        public Task<MqttClientUnsubscribeResult> UnsubscribeAsync(string topic)
        {
            var mqttClientUnsubscribeResult = Client.UnsubscribeAsync(topic);
            if (MessageReceivedHandler is TopicHandleRelay<T> topicMessageRelay)
                topicMessageRelay.Unsubscribe(topic);
            return mqttClientUnsubscribeResult;
        }
    }
}