using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Publishing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vortex.Connector;

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
    public class MqttClientxx : IDisposable
    {
        public IMqttClient Client { get; }
        public IStringPayloadFormatter PayloadFormatter { get; set; } = new JsonStringPayloadFormatter();

        private IDictionary<string, Action<string>> TopicHooks;
        public MqttClientxx(IMqttClient Client)
        {
            this.Client = Client;
            Client.UseApplicationMessageReceivedHandler((msg) => TopicHooks[msg.ApplicationMessage.Topic](msg.ApplicationMessage.ConvertPayloadToString()));
        }
        public Task<MqttClientPublishResult> PublishAsync(object plain, string topic)
        {
            return Client.PublishAsync(topic, PayloadFormatter.Format(plain));
        }

        public void Subsribe(string topic, Action<string> OnMessage)
        {
            Client.SubscribeAsync(topic);
            TopicHooks[topic] = OnMessage;
        }
        public void Dispose() => Client.Dispose();
    }
}
