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
    public interface IStringPayloadFormatter
    {
        string Format(object plain);
    }

    public class JsonStringPayloadFormatter : IStringPayloadFormatter
    {
        public string Format(object plain) => JsonConvert.SerializeObject(plain);
    }
    public class TcoMqtt : IDisposable
    {
        public IMqttClient Client { get; }
        public IStringPayloadFormatter PayloadFormatter { get; set; } = new JsonStringPayloadFormatter();
        public IMqttApplicationMessageReceivedHandler MessageHandler { get; set; }

        private IDictionary<string, Action<string>> TopicHooks;
        public TcoMqtt(IMqttClient Client)
        {
            this.Client = Client;
            MessageHandler = new TopicMessageRelay();
            Client.UseApplicationMessageReceivedHandler(MessageHandler);
        }

        public Task<MqttClientPublishResult> PublishAsync(object plain, string topic)
        {
            return Client.PublishAsync(topic, PayloadFormatter.Format(plain));
        }

        public void Subsribe(string topic, Action<string> OnMessage)
        {
            Client.SubscribeAsync(topic);
            if (MessageHandler is TopicMessageRelay topicMessageRelay)
                topicMessageRelay.Subscribe(topic, OnMessage);
        }
        public void Dispose() => Client.Dispose();
    }
}
