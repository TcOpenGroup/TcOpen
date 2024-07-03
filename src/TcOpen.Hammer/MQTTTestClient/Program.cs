using System;
using System.Collections.Generic;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace MQTTTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
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
                            new MqttTopicFilter() { Topic = "fun_with_TcOpen_Hammer" }
                        }
                    },
                    System.Threading.CancellationToken.None
                )
                .Wait();

            List<MqttApplicationMessage> appMessages = new List<MqttApplicationMessage>();
            subscriberClient.UseApplicationMessageReceivedHandler(
                (m) => Console.WriteLine(m.ApplicationMessage.ConvertPayloadToString())
            );

            Console.ReadLine();
        }
    }
}
