using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Newtonsoft.Json;
using NUnit.Framework;
using Serilog.Sinks;

namespace Serilog.Sinks.MQTTTests
{
    public class MQTTTests
    {
        private IMqttServer mqttServer = new MqttFactory().CreateMqttServer();
        private ServerMessageRecievedHandler messageHandler = new ServerMessageRecievedHandler();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            mqttServer.StartAsync(new MqttServerOptions()).Wait();
            mqttServer.ApplicationMessageReceivedHandler = messageHandler;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            mqttServer.StopAsync();
        }

        [Test]
        public void contructor_test()
        {
            var topic = "this is my topic";
            var qos = MqttQualityOfServiceLevel.AtMostOnce;
            var options = new MqttClientOptionsBuilder().WithTcpServer("localhost").Build();
            var actual = new MQTTSink(options, topic);

            Assert.AreEqual(topic, actual.Topic);
            Assert.AreEqual(qos, actual.QoS);
        }

        [Test]
        public void emit_test()
        {
            var topic = "this is my topic";
            var options = new MqttClientOptionsBuilder().WithTcpServer("localhost").Build();
            var sink = new MQTTSink(options, topic);
            var logEvent = new Events.LogEvent(
                System.DateTimeOffset.Now,
                Events.LogEventLevel.Error,
                null,
                new Events.MessageTemplate("@{payload}", new Parsing.MessageTemplateToken[] { }),
                new Events.LogEventProperty[] { }
            );

            sink.Emit(logEvent);

            System.Threading.Thread.Sleep(100);

            var actual = messageHandler.ApplicationMessages.Where(p => p.Topic == topic);

            Assert.AreEqual(1, actual.Count());
            ;

            var payload = LogMqttMessagePayload.GetPayload(
                actual.FirstOrDefault().ConvertPayloadToString()
            );

            Assert.AreEqual("@{payload}", payload.MessageTemplate);
            Assert.AreEqual("Error", payload.Level);
        }

        [Test]
        public void integrate_with_serilog_test()
        {
            var topic = "this is my fatal topic";
            var options = new MqttClientOptionsBuilder().WithTcpServer("localhost").Build();

            Log.Logger = new LoggerConfiguration().WriteTo.MQTT(options, topic).CreateLogger();

            Log.Fatal("this is fatal");

            System.Threading.Thread.Sleep(100);

            var actual = messageHandler.ApplicationMessages.Where(p => p.Topic == topic);

            Assert.AreEqual(1, actual.Count());

            var payload = LogMqttMessagePayload.GetPayload(
                actual.FirstOrDefault().ConvertPayloadToString()
            );

            Assert.AreEqual("this is fatal", payload.MessageTemplate);
            Assert.AreEqual("Fatal", payload.Level);
        }

#if DEBUG
        [Test]
        public void integrate_with_serilog_test_remote_mqtt_broker()
        {
            var topic = "hojmorhovetvomojhoroduktokradmourukousiahnenatvojuslobodu";
            var publisherOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("broker.emqx.io")
                .Build();
            var subscriberOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("broker.emqx.io")
                .Build();
            var factory = new MqttFactory();
            var subscriberClient = factory.CreateMqttClient();
            subscriberClient
                .ConnectAsync(subscriberOptions, System.Threading.CancellationToken.None)
                .Wait();

            subscriberClient
                .SubscribeAsync(
                    new MQTTnet.Client.Subscribing.MqttClientSubscribeOptions()
                    {
                        TopicFilters = new List<MqttTopicFilter>()
                        {
                            new MqttTopicFilter() { Topic = topic }
                        }
                    },
                    System.Threading.CancellationToken.None
                )
                .Wait();

            MqttApplicationMessage appMessage = null;
            subscriberClient.UseApplicationMessageReceivedHandler(
                (m) => appMessage = m.ApplicationMessage
            );

            Log.Logger = new LoggerConfiguration()
                .WriteTo.MQTT(publisherOptions, topic)
                .CreateLogger();

            Log.Fatal("this is fatal");

            while (appMessage == null)
            {
                System.Threading.Thread.Sleep(10);
            }

            var payload = LogMqttMessagePayload.GetPayload(appMessage.ConvertPayloadToString());

            Assert.AreEqual("this is fatal", payload.MessageTemplate);
            Assert.AreEqual("Fatal", payload.Level);
        }

        [Test]
        public void integrate_with_serilog_test_remote_mqtt_broker_performance_test()
        {
            var topic = "hojmorhovetvomojhoroduktokradmourukousiahnenatvojuslobodu";
            var publisherOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("broker.emqx.io")
                .Build();
            var subscriberOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("broker.emqx.io")
                .Build();
            var factory = new MqttFactory();
            var subscriberClient = factory.CreateMqttClient();
            subscriberClient
                .ConnectAsync(subscriberOptions, System.Threading.CancellationToken.None)
                .Wait();

            subscriberClient
                .SubscribeAsync(
                    new MQTTnet.Client.Subscribing.MqttClientSubscribeOptions()
                    {
                        TopicFilters = new List<MqttTopicFilter>()
                        {
                            new MqttTopicFilter() { Topic = topic }
                        }
                    },
                    System.Threading.CancellationToken.None
                )
                .Wait();

            List<MqttApplicationMessage> appMessages = new List<MqttApplicationMessage>();
            subscriberClient.UseApplicationMessageReceivedHandler(
                (m) => appMessages.Add(m.ApplicationMessage)
            );

            Log.Logger = new LoggerConfiguration()
                .WriteTo.MQTT(publisherOptions, topic)
                .CreateLogger();

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                Log.Fatal($"this is fatal {i}");
            }

            while (appMessages.Count < 1000)
            {
                System.Threading.Thread.Sleep(10);
            }

            sw.Stop();

            Assert.IsTrue(10000 > sw.ElapsedMilliseconds);
        }
#endif
    }
}
