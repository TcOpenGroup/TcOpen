using MQTTnet.Client;
using System;
using System.Threading.Tasks;
using TcoCore;
using Vortex.Connector;

namespace TcOpen.Inxton.Mqtt
{
    public static class VortexObjectPublishExtension
    {
        /// <summary>
        /// Extension for publishing IVortexObjects via MQTT.
        /// <example>
        /// Example for publishing components data, when the power state of a drive changes.
        /// <code>
        ///    IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._ST001._components.PublishChanges(client, "fun_with_TcOpen_Hammer", publishCondition: ComponentsCondition);                
        /// </code>
        /// PublishCondition delegate defined as a method. If you're publishing a certain PLC twin, the tyope of objects inside the will always be a prefixed with "Plain".
        /// <code>
        ///     private bool ComponentsCondition(object LastPublished, object ToPublish)
        ///     {
        ///         var lastPublihed = (PlainST001_ComponentsHandler)LastPublished;
        ///         var toPublihed = (PlainST001_ComponentsHandler)ToPublish;
        ///         return lastPublihed._drive._power != toPublihed._drive._power;
        ///     }
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="vortexObject">PLC Twin object to be published</param>
        /// <param name="client">MQTT Client</param>
        /// <param name="topic">Topic to publish to</param>
        /// <param name="sampleRate">Frequency at which the changes will be published. Defaults to 100ms</param>
        /// <param name="publishCondition">Delegate which accepts two parameters - last published and latest value and returns a boolean</param>
        /// <returns></returns>
        public static IVortexObject PublishChanges(this IVortexObject vortexObject,
                IMqttClient client,
                string topic,
                TimeSpan? sampleRate = null,
                PublishConditionDelegate<object> publishCondition = null)
        {
            var sampleRateValue = sampleRate ?? TimeSpan.FromMilliseconds(1000);
            var mqttWrapper = new TcoMqttPublisher<object>(client);
            Task.Run(async () =>
            {
                while (true)
                {
                    var plainer = vortexObject.CreatePlain();
                    if (publishCondition == null)
                        _ = mqttWrapper.PublishAsync(plainer, topic);
                    else
                        _ = mqttWrapper.PublishAsync(plainer, topic, publishCondition);
                    await Task.Delay(sampleRateValue);
                }
            });
            return vortexObject;
        }
    }

}
