using System;
using Newtonsoft.Json;

namespace Serilog.Sinks.MQTTTests
{
    public class LogMqttMessagePayload
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string MessageTemplate { get; set; }

        public static LogMqttMessagePayload GetPayload(string content)
        {
            return JsonConvert.DeserializeObject<LogMqttMessagePayload>(content);
        }
    }
}
