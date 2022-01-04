using MQTTnet.Client;
using MQTTnet.Client.Publishing;
using MQTTnet.Client.Receiving;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Mqtt
{
    public interface IPayloadFormatterFor<T>
    {
        string Format(T plain);
    }


}
