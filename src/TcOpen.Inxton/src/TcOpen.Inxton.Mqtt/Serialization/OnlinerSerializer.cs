using Newtonsoft.Json;
using Vortex.Connector.ValueTypes;

namespace TcOpen.Inxton.Mqtt
{
    public class OnlinerSerializer<T> : IPayloadSerializer<T>
    {
        public string PropertyName { get; }

        public OnlinerSerializer(OnlinerBaseType<T> onliner)
        {
            PropertyName = onliner.Symbol;
        }

        public string Serialize(T value)
        {
            return JsonConvert.SerializeObject(new PropertyValue<T> { Property = PropertyName, Value = value });
        }
    }

    public class OnlinerDeserializer<T> : IPayloadDeserializer<T>
    {
        public T Deserialize(string value)
        {
            var deserialize = JsonConvert.DeserializeObject<PropertyValue<T>>(value);
            return deserialize.Value;
        }
    }

    internal class PropertyValue<T>
    {
        public string Property { get; set; }
        public T Value { get; set; }

    }

}
