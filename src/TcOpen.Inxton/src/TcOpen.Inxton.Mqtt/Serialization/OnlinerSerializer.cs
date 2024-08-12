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
            return JsonConvert.SerializeObject(
                new PropertyValue<T> { Property = PropertyName, Value = value }
            );
        }
    }
}
