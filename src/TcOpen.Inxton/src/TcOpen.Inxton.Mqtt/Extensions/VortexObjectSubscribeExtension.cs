using MQTTnet.Client;
using Vortex.Connector;

namespace TcOpen.Inxton.Mqtt
{
    public static class VortexObjectSubscribeExtension
    {
        /// <summary>
        /// Write changes from a topic to an Onliner object.
        /// <example>
        /// Select the Plc-Twin variable in and subscribe to it. 
        /// If you subscribe to an object of type i.e.  "ST001_ComponentsHandle" provide a "Plain" type in the generic subscribe method for parsing  purposes "PlainST001_ComponentsHandle"
        /// 
        /// _mqttMirrorComponents is of type ST001_ComponentsHandle but the data that is in the Mqtt topic is of type PlainST001_ComponentsHandle, therefore use the plain type for subscribe.
        /// 
        ///     <code>
        ///         IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._ST001
        ///             ._mqttMirrorComponents
        ///             .Subscribe<PlainST001_ComponentsHandler>(client, "fun_with_TcOpen_Hammer");
        ///     </code>
        /// </example>
        /// 

        /// </summary>
        /// <typeparam name="T">The plain type of a VortexObject</typeparam>
        /// <param name="vortexObject">Online PLC twin</param>
        /// <param name="client">MQTT Client</param>
        /// <param name="topic">Topic name</param>
        /// <returns>TcoMqttSubscriber for unbsubscribing</returns>
        public static TcoMqttSubscriber<T> Subscribe<T>(this IVortexObject vortexObject, IMqttClient client, string topic) where T : IPlain
        {
            var mqttWrapper = new TcoMqttSubscriber<T>(client, new JsonStringPayloadDeserializer<T>());
            mqttWrapper.SubscribeAsync(topic, newData => vortexObject.FlushPlainToOnline(newData));
            return mqttWrapper;
        }

        private static void FlushPlainToOnline(this IVortexObject obj, dynamic plainer)
        {
            dynamic o = obj;
            o.FlushPlainToOnline(plainer);
        }
    }

}
