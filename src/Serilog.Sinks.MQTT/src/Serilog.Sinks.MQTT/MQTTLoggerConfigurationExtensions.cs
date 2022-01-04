using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using Serilog.Configuration;
using Serilog.Events;

namespace Serilog.Sinks
{
    public static class MQTTLoggerConfigurationExtensions
    {
        /// <summary>
        /// Creates logger configuration for MQTT.
        /// </summary>
        /// <param name="sinkConfiguration">Sink configuration</param>
        /// <param name="clientOptions">MQTT client options</param>
        /// <param name="topic">MQTT topic under which the logs are to be published.</param>
        /// <param name="qoS">Quality of service level. <seealso cref="MqttQualityOfServiceLevel"/></param>
        /// <param name="restrictedToMinimumLevel">Restricted min level.</param>
        /// <param name="formatter">Custom formatter.</param>
        /// <returns></returns>
        public static LoggerConfiguration MQTT(
           this LoggerSinkConfiguration sinkConfiguration,
            IMqttClientOptions clientOptions,
                    string topic,
                    MqttQualityOfServiceLevel qoS = MqttQualityOfServiceLevel.AtMostOnce,
                    LogEventLevel restrictedToMinimumLevel = LogEventLevel.Information,
                    Formatting.ITextFormatter formatter = null)
        {
            return sinkConfiguration.Sink(new MQTTSink(clientOptions, topic, qoS, formatter), restrictedToMinimumLevel);
        }
    }
}