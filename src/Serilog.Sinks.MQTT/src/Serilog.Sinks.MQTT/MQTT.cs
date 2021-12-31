using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace Serilog.Sinks
{
    public class MQTT : Serilog.Core.ILogEventSink, IDisposable
    {
        private IMqttClient MqttClient { get; } = new MqttFactory().CreateMqttClient();
        private Formatting.ITextFormatter Formatter { get; } = new Serilog.Formatting.Json.JsonFormatter();

        private IMqttClientOptions MqttClientOptions { get; }

        public MQTT(IMqttClientOptions clientOptions,
                    string topic,
                    Formatting.ITextFormatter formatter = null)
        {
            Topic = topic;
            Formatter = formatter ?? Formatter;
            MqttClientOptions = clientOptions;
        }

        public string Topic { get; }
        
        public async void Emit(LogEvent logEvent)
        {            
            if(!MqttClient.IsConnected)
            {
                await MqttClient.ConnectAsync(MqttClientOptions);
            }

            var stringWriter = new StringWriter();
            Formatter.Format(logEvent, stringWriter);
            await MqttClient.PublishAsync(Topic, stringWriter.ToString());
        }

        public void Dispose()
        {
            MqttClient.Dispose();
        }
    }
}