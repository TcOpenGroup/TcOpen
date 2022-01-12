using MQTTnet.Client;
using Vortex.Connector.ValueTypes;

namespace TcOpen.Inxton.Mqtt
{
    public class TcoMqttPrimitivePublisher<T> : TcoMqttPublisher<T>
    {
        public ValueChangedEventHandlerDelegate PrimitiveSubscribeDelegate { get; set; }

        public TcoMqttPrimitivePublisher(IMqttClient Client) : base(Client)
        {
        }

        public TcoMqttPrimitivePublisher(IMqttClient Client, IPayloadSerializer<T> Serializer) : base(Client, Serializer)
        {
        }
    }

}