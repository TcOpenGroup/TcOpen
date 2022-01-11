using MQTTnet.Client;
using Vortex.Connector.ValueTypes;

namespace TcOpen.Inxton.Mqtt
{
    public static partial class OnlinerBaseTypeSubscribeExtensions
    {
        /// <summary>
        /// Will write new values from a topic to an Onliner.
        /// </summary>
        /// <typeparam name="T">The type of an onliner</typeparam>
        /// <param name="onliner">PLC Twin</param>
        /// <param name="client">MQTT Client</param>
        /// <param name="topic">Topic which will be observed</param>
        /// <returns></returns>
        public static TcoMqttSubscriber<T> Subscribe<T>(this OnlinerBaseType<T> onliner, IMqttClient client, string topic)
        {
            var mqttWrapper = new TcoMqttSubscriber<T>(client, new OnlinerDeserializer<T>());
            mqttWrapper.SubscribeAsync(topic, newValue => onliner.Cyclic = newValue);
            return mqttWrapper;
        }
    }
}