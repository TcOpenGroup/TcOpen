using MQTTnet.Client;
using MQTTnet.Client.Publishing;
using MQTTnet.Client.Receiving;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Mqtt
{
    public class TcoMqtt<T> : IDisposable
    {
        public IMqttClient Client { get; }
        public IPayloadFormatterFor<T> PayloadFormatter { get; set; }
        public IMqttApplicationMessageReceivedHandler MessageHandler { get; set; }
        private IList<string> SubscribedTopics;
        public TcoMqtt(IMqttClient Client)
        {
            this.Client = Client;
            SubscribedTopics = new List<string>();
            PayloadFormatter = new JsonStringPayloadFormatter<T>();
            MessageHandler = new TopicMessageRelay();
            Client.UseApplicationMessageReceivedHandler(MessageHandler);
        }

        public Task<MqttClientPublishResult> PublishAsync(T data, string topic)
        {
            return Client.PublishAsync(topic, PayloadFormatter.Format(data));
        }

        public void Subsribe(string topic, Action<string> OnMessage)
        {
            Client.SubscribeAsync(topic);
            if (MessageHandler is TopicMessageRelay topicMessageRelay)
                topicMessageRelay.Subscribe(topic, OnMessage);
            SubscribedTopics.Add(topic);
        }

        public void Unsubsribe(string topic)
        {
            Client.UnsubscribeAsync(topic);
            if (MessageHandler is TopicMessageRelay topicMessageRelay)
                topicMessageRelay.Unsubscribe(topic);
            SubscribedTopics.Remove(topic);
        }

        public void UnsubscribeFromAllTopics()
        {
            foreach (var topic in SubscribedTopics)
                Unsubsribe(topic);
        }

        public void Dispose()
        {
            UnsubscribeFromAllTopics();
            Client.Dispose();
        }
    }


}
