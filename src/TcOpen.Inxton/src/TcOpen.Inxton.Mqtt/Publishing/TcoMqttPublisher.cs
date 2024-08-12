using System.Threading;
using System.Threading.Tasks;
using MQTTnet.Client;
using MQTTnet.Client.Publishing;

namespace TcOpen.Inxton.Mqtt
{
    public class TcoMqttPublisher<T>
    {
        public IMqttClient Client { get; }
        public IPayloadSerializer<T> PayloadSerializer { get; }

        public CancellationTokenSource CancellationToken { get; }

        private T LastPublished;

        public TcoMqttPublisher(IMqttClient Client, IPayloadSerializer<T> Serializer)
        {
            this.Client = Client;
            PayloadSerializer = Serializer;
            CancellationToken = new CancellationTokenSource();
        }

        public TcoMqttPublisher(IMqttClient Client)
            : this(Client, new JsonStringPayloadSerializer<T>()) { }

        public Task<MqttClientPublishResult> PublishAsync(T data, string topic)
        {
            var serialized = PayloadSerializer.Serialize(data);
            var publishResult = Client.PublishAsync(topic, serialized);
            LastPublished = data;
            return publishResult;
        }

        public Task<MqttClientPublishResult> PublishAsync(
            T data,
            string topic,
            PublishConditionDelegate<T> PublishCondition
        )
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
                return Task.FromResult(
                    new MqttClientPublishResult() { ReasonString = "PublishCondition was false" }
                );
            }
        }

        public void StopPublishing()
        {
            CancellationToken.Cancel();
        }
    }
}
