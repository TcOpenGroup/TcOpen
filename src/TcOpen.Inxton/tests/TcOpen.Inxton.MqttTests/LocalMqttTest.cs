using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading;
using TcOpen.Inxton.Mqtt;

namespace TcOpen.Inxton.Logging.Tests
{
    [TestFixture]
    public class LocalMqttTest
    {
        MQTTnet.Server.IMqttServer Broker;
        MqttFactory MqttFactory;

        int MqttPort = 1883;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {

            MqttFactory = new MqttFactory();
            Broker = MqttFactory.CreateMqttServer();
            var mqttServerOptions = MqttFactory
                    .CreateServerOptionsBuilder()
                    .WithDefaultEndpointPort(MqttPort)
                    .Build();
            Broker.StartAsync(mqttServerOptions);
        }

        private IMqttClient CreateClientAndConnect()
        {
            var c = MqttFactory.CreateMqttClient();
            var mqttClientOptions = MqttFactory.CreateClientOptionsBuilder().WithTcpServer("localhost", MqttPort).Build();
            c.ConnectAsync(mqttClientOptions, CancellationToken.None).Wait();
            return c;
        }
        private void Disconnect(IMqttClient c)
        {
            c.DisconnectAsync(new MQTTnet.Client.Disconnecting.MqttClientDisconnectOptions(), CancellationToken.None);
        }
        [Test]
        public void PublishDataTest()
        {
            //arrange

            var client = CreateClientAndConnect();
            var mqtt = new TcoMqttPublisher<object>(client);

            var objectToPublish = new { data = "hello", number = 10, doubleNumber = 10.2, nestedObject = new { hello = "i'm nested" } };
            var objectJson = JsonConvert.SerializeObject(objectToPublish);
            //act
            string deliveredMessage = "no_message";
            var messageHandler = new RelayHandler((msg) => deliveredMessage = msg.ApplicationMessage.ConvertPayloadToString());
            Broker.ApplicationMessageReceivedHandler = messageHandler;
            mqtt.PublishAsync(objectToPublish, "/topic").Wait();
            while (messageHandler.MessageNotDelivered) { Thread.Sleep(20); }
            Disconnect(client);
            //assert
            Assert.That(deliveredMessage, Is.EqualTo(objectJson));
        }



    }
}