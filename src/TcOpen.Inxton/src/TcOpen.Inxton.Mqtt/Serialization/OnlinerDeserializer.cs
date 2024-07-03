using Newtonsoft.Json;

namespace TcOpen.Inxton.Mqtt
{
    public class OnlinerDeserializer<T> : IPayloadDeserializer<T>
    {
        public T Deserialize(string value)
        {
            var deserialize = JsonConvert.DeserializeObject<PropertyValue<T>>(value);
            return deserialize.Value;
        }
    }
}
