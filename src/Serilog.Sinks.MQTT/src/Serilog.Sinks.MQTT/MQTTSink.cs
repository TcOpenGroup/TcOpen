using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using Serilog.Events;
using System;
using System.IO;

namespace Serilog.Sinks
{
    /// <summary>
    /// Provides serilog sink for publishing logs to MQTT broker.
    /// </summary>
    public class MQTTSink : Core.ILogEventSink, IDisposable
    {
        private IMqttClient MqttClient { get; } = new MqttFactory().CreateMqttClient();
        private Formatting.ITextFormatter Formatter { get; } = new Serilog.Formatting.Json.JsonFormatter();
        private IMqttClientOptions MqttClientOptions { get; }

        /// <summary>
        /// Creates new instance of <see cref="MQTTSink"/>
        /// </summary>
        /// <param name="clientOptions">MQTT client options <seealso cref="https://github.com/chkr1011/MQTTnet/wiki/Client"/></param>
        /// <param name="topic">Topic under which the logs are to be published.</param>
        /// <param name="qoS">Quality of service level. <see cref="MqttQualityOfServiceLevel"/></param>
        /// <param name="formatter">Custom log formatter.</param>
        public MQTTSink(IMqttClientOptions clientOptions,
                    string topic,
                    MqttQualityOfServiceLevel qoS = MqttQualityOfServiceLevel.AtMostOnce,
                    Formatting.ITextFormatter formatter = null)
        {
            Topic = topic;
            QoS = qoS;
            Formatter = formatter ?? Formatter;
            MqttClientOptions = clientOptions;
            MqttClient.ConnectAsync(MqttClientOptions).Wait();
        }

        /// <summary>
        /// Gets `Topic` name under which the logs are published in this MQTT sink.
        /// </summary>
        public string Topic { get; }

        public MqttQualityOfServiceLevel QoS { get; }

        /// <summary>
        /// Emits (publishes) the event to the configured MQTT broker.
        /// </summary>
        /// <param name="logEvent"></param>
        public async void Emit(LogEvent logEvent)
        {            
            var stringWriter = new StringWriter();
            Formatter.Format(logEvent, stringWriter);

            var applicationMessage = new MqttApplicationMessageBuilder()
                            .WithPayload(stringWriter.ToString())
                            .WithPayloadFormatIndicator(MQTTnet.Protocol.MqttPayloadFormatIndicator.CharacterData)
                            .WithTopic(Topic)                            
                            .Build();
            
            applicationMessage.QualityOfServiceLevel = QoS;
            
            await MqttClient.PublishAsync(applicationMessage);
        }

        public void Dispose()
        {
            MqttClient.DisconnectAsync().Wait();
            MqttClient.Dispose();
        }
    }
}