using MQTTnet.Client.Options;
using NUnit.Framework;
using Serilog.Sinks;

namespace Serilog.Sinks.MQTTTests
{
    public class MQTTTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void contructor_test()
        {
            var topic = "this is my topic";
            var options = new MqttClientOptionsBuilder().WithTcpServer("").Build();
            var actual = new MQTT(options, topic);

            Assert.AreEqual(topic, actual.Topic);            
        }

       [Test]
       public void emit_test()
       {
            var topic = "this is my topic";
            var options = new MqttClientOptionsBuilder().WithTcpServer("").Build();
            var sink = new MQTT(options, topic);

           sink.Emit(new Events.LogEvent() { Level = Events.LogEventLevel.Debug, })
        }
    }
}