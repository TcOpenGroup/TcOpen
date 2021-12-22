using MQTTnet;
using MQTTnet.Client.Receiving;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using TcOpen.Inxton.Mqtt;

namespace TcOpen.Inxton.Logging.Tests
{

    class RelayHandler : IMqttApplicationMessageReceivedHandler
    {
        public Func<MqttApplicationMessageReceivedEventArgs, string> OnMessage;
        public bool MessageNotDelivered { get; private set; } = true;

        public RelayHandler(Func<MqttApplicationMessageReceivedEventArgs, string> onMessage)
        {
            OnMessage = onMessage;
        }

        public Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            if (MessageNotDelivered)
            {
                MessageNotDelivered = false;
                return Task.FromResult(OnMessage(eventArgs));
            }
            else
                throw new Exception("Only one message per handler");
        }


    }
    [TestFixture]
    public class LocalMqttTest
    {
        [Test]
        public void DummyLoggerTest()
        {
            //arrange
            var mqttFactory = new MqttFactory();
            var port = 1337;
            var mqttServer = mqttFactory.CreateMqttServer();
            var mqttServerOptions = mqttFactory
                    .CreateServerOptionsBuilder()
                    .WithClientId("clientId")
                    .WithDefaultEndpointPort(port)
                    .Build();

            var mqttClient = mqttFactory.CreateMqttClient();
            var mqttClientOptions = mqttFactory.CreateClientOptionsBuilder().WithTcpServer("localhost", port).Build();

            mqttServer.StartAsync(mqttServerOptions);
            mqttClient.ConnectAsync(mqttClientOptions, System.Threading.CancellationToken.None);
            var mqtt = new MqttClientxx(mqttClient);
            //act
            string messg = "";
            var messageHandler = new RelayHandler((msg) => messg = msg.ApplicationMessage.ConvertPayloadToString());
            mqttServer.ApplicationMessageReceivedHandler = messageHandler;
            mqtt.PublishAsync(new { a = "bitch" }, "/topic").Wait();
            while (messageHandler.MessageNotDelivered) { Thread.Sleep(10); }
            Assert.That(messg.Equals(JsonConvert.SerializeObject(new { a = "bitch" })));
        }


    }
}