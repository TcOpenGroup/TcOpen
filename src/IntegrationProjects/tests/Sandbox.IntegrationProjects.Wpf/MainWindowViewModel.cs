using IntegrationProjects;
using MQTTnet;
using MQTTnet.Client;
using System.Threading;
using TcOpen.Inxton.Mqtt;
using TcOpen.Inxton.Swift.Wpf;
using Vortex.Presentation.Wpf;

namespace Sandbox.IntegrationProjects.Wpf
{

    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            SwiftRecordVm = new SwiftRecorderViewModel(IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._ST001);

            IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._ST001._serviceModeTask.Execute(null);
            System.Threading.Thread.Sleep(1000);
            IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._ST001._groundSequenceTask.Execute(null);
            var client = CreateClientAndConnect();

            IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._startCycleCount.PublishChanges(client, "fun_with_TcOpen_Hammer",
                publishCondition: (lastPublished, toPublish) => true);

            IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._mirrorStartCycle.Subscribe(client, "fun_with_TcOpen_Hammer");

           // IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._ST001._components.PublishChanges(client, "fun_with_TcOpen_Hammer");
           //  IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._ST001._mqttMirrorComponents.Subscribe<PlainST001_ComponentsHandler>(client, "fun_with_TcOpen_Hammer");

        }


        private static IMqttClient CreateClientAndConnect()
        {
            int MqttPort = 1883;
            var MqttFactory = new MqttFactory();
            var Broker = MqttFactory.CreateMqttServer();
            var mqttServerOptions = MqttFactory
                    .CreateServerOptionsBuilder()
                    .WithDefaultEndpointPort(MqttPort)
                    .Build();
            Broker.StartAsync(mqttServerOptions);

            var c = MqttFactory.CreateMqttClient().UseApplicationMessageReceivedHandler(new TcoAppMqttHandler());
            var mqttClientOptions = MqttFactory.CreateClientOptionsBuilder().WithTcpServer("broker.emqx.io").Build();
            c.ConnectAsync(mqttClientOptions, CancellationToken.None).Wait();
            return c;
        }

        public SwiftRecorderViewModel SwiftRecordVm { get; }

        public IntegrationProjectsTwinController IntegrationProjectsPlc { get; } = Entry.IntegrationProjectsPlc;
    }


}
