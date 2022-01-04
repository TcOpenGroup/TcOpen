# SeriLog MQTT sink

This is an initial implementation of MQTT sink for serilog logger.
For details about the configuration of MQTT client see [here](https://github.com/chkr1011/MQTTnet/wiki/Client).
This sink is primarily developed to be used in TcOpen framework with TcoLogger.

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


