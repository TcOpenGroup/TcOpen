using MQTTnet.Client;
using Vortex.Connector.ValueTypes;

namespace TcOpen.Inxton.Mqtt
{
    public static class OnlinerBaseTypePublishExtensions
    {
        /// <summary>
        /// Used to publish primitive values from PLC to a MQTT topic.
        /// </summary>
        /// <example>
        /// Publishing to topic "fun_with_TcOpen_Hammer" when the newValue is bigger than 100.
        /// <code>
        ///     IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._startCycleCount.PublishChanges(client, "fun_with_TcOpen_Hammer",
        ///         publishCondition: (lastPublished, toPublish) => (toPublish - lastPublished) >= 100);
        ///     </code>
        /// </example>    
        /// <typeparam name="T">Type of the primitive, usually inferred by the compiler</typeparam>
        /// <param name="onliner">PLC Twin of a primitive which will be published</param>
        /// <param name="client">MQTT Client</param>
        /// <param name="topic">Topic</param>
        /// <param name="publishCondition">Delegate which accepts two parameters - last published and latest value and returns a boolean</param>
        /// <returns>The delegate used for subscribing, which can be used to unsubscribe later</returns>
        public static TcoMqttPrimitivePublisher<T> PublishChanges<T>(this OnlinerBaseType<T> onliner, IMqttClient client, string topic, PublishConditionDelegate<T> publishCondition = null)
        {
            var mqttWrapper = new TcoMqttPrimitivePublisher<T>(client, new OnlinerSerializer<T>(onliner));
            mqttWrapper.PrimitiveSubscribeDelegate = (valueTag, valueChangedArgs) =>
                {
                    if (mqttWrapper.CancellationToken.IsCancellationRequested)
                    {
                        onliner.UnSubscribe(mqttWrapper.PrimitiveSubscribeDelegate);
                        return;
                    }

                    if (publishCondition == null)
                        mqttWrapper.PublishAsync(onliner.Cyclic, topic);
                    else
                        mqttWrapper.PublishAsync(onliner.Cyclic, topic, publishCondition);

                };
            onliner.Subscribe(mqttWrapper.PrimitiveSubscribeDelegate);
            return mqttWrapper;
        }
    }
}