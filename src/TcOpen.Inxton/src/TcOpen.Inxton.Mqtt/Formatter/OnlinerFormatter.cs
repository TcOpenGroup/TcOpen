using Newtonsoft.Json;
using Vortex.Connector.ValueTypes;

namespace TcOpen.Inxton.Mqtt
{
    public class OnlinerFormatter<T> : IPayloadFormatterFor<T>
    {
        public string PropertyName { get; }

        public OnlinerFormatter(OnlinerBaseType<T> onliner)
        {
            PropertyName = onliner.Symbol;
        }

        public string Format(T value)
        {
            return JsonConvert.SerializeObject(new { Property = PropertyName, Value = value }); ;
        }
    }

}
