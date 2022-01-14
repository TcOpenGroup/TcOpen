using Newtonsoft.Json;

namespace TcOpen.Inxton.Mqtt
{
    public class JsonStringPayloadSerializer<T> : IPayloadSerializer<T>
    {
        public string Serialize(T plain) => JsonConvert.SerializeObject(plain);
    }
}
