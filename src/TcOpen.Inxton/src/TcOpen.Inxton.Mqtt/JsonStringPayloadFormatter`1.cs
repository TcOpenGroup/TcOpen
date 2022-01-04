using MQTTnet.Client;
using MQTTnet.Client.Publishing;
using MQTTnet.Client.Receiving;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Mqtt
{

    public class JsonStringPayloadFormatter<T> : IPayloadFormatterFor<T>
    {
        public string Format(T plain) => JsonConvert.SerializeObject(plain);
    }


}
