using System;
using System.Linq;
using System.Threading;
using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;
using NUnit.Framework;
using TcOpen.Inxton.Mqtt;
using Vortex.Connector.ValueTypes;

namespace TcOpen.Inxton.MqttTests
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
            var mqttClientOptions = MqttFactory
                .CreateClientOptionsBuilder()
                .WithTcpServer("localhost", MqttPort)
                .Build();
            c.ConnectAsync(mqttClientOptions, CancellationToken.None).Wait();
            c.UseApplicationMessageReceivedHandler(new TcoAppMqttHandler());
            return c;
        }

        private void Disconnect(IMqttClient c)
        {
            c.DisconnectAsync(
                new MQTTnet.Client.Disconnecting.MqttClientDisconnectOptions(),
                CancellationToken.None
            );
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
            var messageHandler = new RelayHandler(
                (msg) => deliveredMessage = msg.ApplicationMessage.ConvertPayloadToString()
            );
            Broker.ApplicationMessageReceivedHandler = messageHandler;
            mqtt.PublishAsync(objectToPublish, "/topic").Wait();
            while (messageHandler.MessageNotDelivered)
            {
                Thread.Sleep(20);
            }
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
            var subscriber = new TcoMqttSubscriber<MqttData>(client, "/topic");

            var objectToPublish = new MqttData();
            var objectToMirror = new MqttData(false);

            //act
            subscriber.SubscribeAsync(newData => objectToMirror = newData);

            publisher.PublishAsync(objectToPublish, "/topic").Wait();

            while (objectToMirror.Nested == null)
            {
                Thread.Sleep(10);
            }
            Disconnect(client);
            //assert
            Assert.That(objectToPublish, Is.EqualTo(objectToMirror));
        }

        [Test]
        public void Subscribing_to_different_topic_will_timeout()
        {
            //arrange
            var client = CreateClientAndConnect();
            var publisher = new TcoMqttPublisher<MqttOtherData>(client);
            var subscriber = new TcoMqttSubscriber<MqttData>(client, "/topic");

            var publishOtherData = new MqttOtherData();
            MqttData mqttData = null;

            //act
            subscriber.SubscribeAsync(newMqttData => mqttData = newMqttData);

            publisher.PublishAsync(publishOtherData, "/totheropic").Wait();

            Assert.That(publishOtherData, Is.Not.EqualTo(mqttData).After(500, 50));

            Disconnect(client);
        }

        [Test]
        public void Using_tcomqqt_subscriber_without_AppMqttHandler_will_throw_exception()
        {
            //arrange
            var client = CreateClientAndConnect();
            client.UseApplicationMessageReceivedHandler(
                new RelayHandler((message) => message.ApplicationMessage.ConvertPayloadToString())
            );
            var publisher = new TcoMqttPublisher<MqttData>(client);

            Assert.Throws<ArgumentException>(() =>
            {
                var subscriber = new TcoMqttSubscriber<MqttData>(client, "/topic");
            });
        }

        [Test]
        public void Unsubscribe_from_onliner_base_will_remove_the_handle()
        {
            //arrange
            var client = CreateClientAndConnect();
            var handler = client.ApplicationMessageReceivedHandler as TcoAppMqttHandler;
            var adapter = new Vortex.Connector.ConnectorAdapter(
                typeof(Vortex.Connector.DummyConnectorFactory)
            );
            var parent = new PlcParent(adapter.GetConnector(null), "", "");
            var plcInt = adapter.Adapter.CreateINT(parent, "int", "int");

            var handle = plcInt.Subscribe(client, "/topic");
            Assert.That(handler.TopicNames.ToList().Count, Is.EqualTo(1));
            handle.UnsubscribeAsync();
            Assert.That(handler.TopicNames.ToList().Count, Is.EqualTo(0));
        }
    }
}
