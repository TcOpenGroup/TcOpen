using Newtonsoft.Json;

namespace TcOpen.Inxton.Mqtt
{
    public class JsonStringPayloadSerializer<T> : IPayloadSerializer<T>
    {
        public string Serialize(T plain) => JsonConvert.SerializeObject(plain);
    }

    public class JsonStringPayloadDeserializer<T> : IPayloadDeserializer<T>
    {
        public T Deserialize(string message) => JsonConvert.DeserializeObject<T>(message);
    }
}
