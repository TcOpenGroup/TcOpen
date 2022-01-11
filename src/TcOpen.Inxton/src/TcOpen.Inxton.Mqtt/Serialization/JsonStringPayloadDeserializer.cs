using Newtonsoft.Json;

namespace TcOpen.Inxton.Mqtt
{
    public class JsonStringPayloadDeserializer<T> : IPayloadDeserializer<T>
    {
        public T Deserialize(string message) => JsonConvert.DeserializeObject<T>(message);
    }
}
