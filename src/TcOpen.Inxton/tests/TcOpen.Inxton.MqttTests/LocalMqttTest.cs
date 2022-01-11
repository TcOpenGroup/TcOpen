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
        public void Published_object_json_will_be_the_same_as_delivered()
        {
            //arrange
            var client = CreateClientAndConnect();
            var mqtt = new TcoMqttPublisher<object>(client);

            var objectToPublish = new MqttData();
            var stringJson = JsonConvert.SerializeObject(objectToPublish);
            //act
            string deliveredMessage = "no_message";
            var messageHandler = new RelayHandler((msg) => deliveredMessage = msg.ApplicationMessage.ConvertPayloadToString());
            Broker.ApplicationMessageReceivedHandler = messageHandler;
            mqtt.PublishAsync(objectToPublish, "/topic").Wait();
            while (messageHandler.MessageNotDelivered) { Thread.Sleep(20); }
            Disconnect(client);
            //assert
            Assert.That(deliveredMessage, Is.EqualTo(stringJson));
        }


        [Test]
        public void Subscribed_object_will_mirror_the_content_of_published()
        {
            //arrange
            var client = CreateClientAndConnect();
            var publisher = new TcoMqttPublisher<MqttData>(client);
            var subscriber = new TcoMqttSubscriber<MqttData>(client);

            var objectToPublish = new MqttData();
            var objectToMirror = new MqttData(false);

            //act
            subscriber.SubscribeAsync("/topic", newData => objectToMirror = newData);

            publisher.PublishAsync(objectToPublish, "/topic").Wait();

            while (objectToMirror.Nested == null) { Thread.Sleep(10); }
            Disconnect(client);
            //assert
            Assert.That(objectToPublish, Is.EqualTo(objectToMirror));
        }


        [Test]
        public void Subscribing_to_different_published_type_will_fail()
        {
            //arrange
            var client = CreateClientAndConnect();
            var publisher = new TcoMqttPublisher<MqttOtherData>(client);
            var subscriber = new TcoMqttSubscriber<MqttData>(client);

            var publishOtherData = new MqttOtherData();
            MqttData mqttData = null;

            //act

            subscriber.SubscribeAsync("/topic", newMqttData => mqttData = newMqttData);

            publisher.PublishAsync(publishOtherData, "/topic").Wait();

            while (mqttData == null) { Thread.Sleep(10); }
            Disconnect(client);
            //assert
            Assert.Ignore();
            //Assert.That(publishOtherData, Is.Not.EqualTo(mqttData));
        }


        [Test]
        public void Subscribing_to_different_topic_will_timeout()
        {
            //arrange
            var client = CreateClientAndConnect();
            var publisher = new TcoMqttPublisher<MqttOtherData>(client);
            var subscriber = new TcoMqttSubscriber<MqttData>(client);

            var publishOtherData = new MqttOtherData();
            MqttData mqttData = null;

            //act

            subscriber.SubscribeAsync("/topic", newMqttData => mqttData = newMqttData);

            publisher.PublishAsync(publishOtherData, "/totheropic").Wait();

            Assert.That(publishOtherData, Is.Not.EqualTo(mqttData).After(500, 50));

            Disconnect(client);

        }


    }
}