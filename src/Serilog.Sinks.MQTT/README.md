# SeriLog MQTT sink

This sink was primarily developed to be used in TcOpen framework.

## Sink configuration

~~~C#
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Server;
using Serilog.Sinks;
using System;
using MQTTnet.Client;

   var topic = "my_funny_topic";
   var publisherOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.emqx.io").Build();
   Log.Logger = new LoggerConfiguration()
                    .WriteTo.MQTT(publisherOptions, topic)
                    .CreateLogger();                    
~~~


