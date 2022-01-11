using MQTTnet.Client;
using MQTTnet.Client.Publishing;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Mqtt
{
    public class TcoMqttPublisher<T>
    {
        public IMqttClient Client { get; }
        public IPayloadSerializer<T> PayloadSerializer { get; }

        private T LastPublished;

        public TcoMqttPublisher(IMqttClient Client, IPayloadSerializer<T> Serializer)
        {
            this.Client = Client;
            PayloadSerializer = Serializer;
        }

        public TcoMqttPublisher(IMqttClient Client) : this(Client, new JsonStringPayloadSerializer<T>())
        {

        }

        public Task<MqttClientPublishResult> PublishAsync(T data, string topic)
        {
            var publishResult = Client.PublishAsync(topic, PayloadSerializer.Serialize(data));
            LastPublished = data;
            return publishResult;
        }

        public Task<MqttClientPublishResult> PublishAsync(T data, string topic, PublishConditionDelegate<T> PublishCondition)
        {
            if (LastPublished == null)
                return PublishAsync(data, topic);
            if (PublishCondition(LastPublished, data))
            {
                var publishResult = Client.PublishAsync(topic, PayloadSerializer.Serialize(data));
                LastPublished = data;
                return publishResult;
            }
            else
            {
                return Task.FromResult(new MqttClientPublishResult() { ReasonString = "PublishCondition was false" });
            }
        }

    }

}