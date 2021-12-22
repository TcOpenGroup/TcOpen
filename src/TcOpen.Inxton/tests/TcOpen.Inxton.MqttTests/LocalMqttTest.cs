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
        MQTTnet.Server.IMqttServer MqttServer;
        MqttFactory MqttFactory;

        int MqttPort = 1883;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {

            MqttFactory = new MqttFactory();
            MqttServer = MqttFactory.CreateMqttServer();
            var mqttServerOptions = MqttFactory
                    .CreateServerOptionsBuilder()
                    .WithDefaultEndpointPort(MqttPort)
                    .Build();
            MqttServer.StartAsync(mqttServerOptions);

            //var adapter = new Vortex.Connector.ConnectorAdapter(typeof(Vortex.Connector.DummyConnectorFactory));
            //Plc = new PlcHammer.PlcHammerTwinController(adapter);
            //Plc.Connector.BuildAndStart();
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
            var mqtt = new TcoMqtt(client);

            var objectToPublish = new { data = "hello", number = 10, doubleNumber = 10.2, nestedObject = new { hello = "i'm nested" } };
            var objectJson = JsonConvert.SerializeObject(objectToPublish);
            //act
            string deliveredMessage = "no_message";
            var messageHandler = new RelayHandler((msg) => deliveredMessage = msg.ApplicationMessage.ConvertPayloadToString());
            MqttServer.ApplicationMessageReceivedHandler = messageHandler;
            mqtt.PublishAsync(objectToPublish, "/topic").Wait();
            Disconnect(client);
            while (messageHandler.MessageNotDelivered) { Thread.Sleep(10); }

            //assert
            Assert.That(deliveredMessage.Equals(objectJson));

        }

        [Test]
        public void RecieveDataTest()
        {

            var publisherMqtt = new TcoMqtt(CreateClientAndConnect());
            var observeMqtt = new TcoMqtt(CreateClientAndConnect());

            observeMqtt.Subsribe("topic", (message) => Console.WriteLine(message)); ;
            publisherMqtt.PublishAsync(new { some = "data" },"topic").Wait();
    
            Assert.Pass();
        }


    }
}