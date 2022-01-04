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
            var mqtt = new TcoMqtt<object>(client);

            var objectToPublish = new { data = "hello", number = 10, doubleNumber = 10.2, nestedObject = new { hello = "i'm nested" } };
            var objectJson = JsonConvert.SerializeObject(objectToPublish);
            //act
            string deliveredMessage = "no_message";
            var messageHandler = new RelayHandler((msg) => deliveredMessage = msg.ApplicationMessage.ConvertPayloadToString());
            Broker.ApplicationMessageReceivedHandler = messageHandler;
            mqtt.PublishAsync(objectToPublish, "/topic").Wait();
            Disconnect(client);
            while (messageHandler.MessageNotDelivered) { Thread.Sleep(10); }

            //assert
            Assert.That(deliveredMessage.Equals(objectJson));

        }

        [Test]
        public void PublishAndRecieveTest()
        {
            var publisherMqtt = new TcoMqtt<object>(CreateClientAndConnect());
            var observeMqtt = new TcoMqtt<object>(CreateClientAndConnect());
            var msg = new { Lorem = "Ipsum" };
            var msgJson = JsonConvert.SerializeObject(msg);
            string recievedMsg = "no_msg";
            observeMqtt.Subsribe("topic", (message) => recievedMsg = message);
            publisherMqtt.PublishAsync(msg, "topic").Wait();
            Assert.That(() => recievedMsg, Is.EqualTo(msgJson).After(200).PollEvery(10).MilliSeconds);
        }

    }
}