using Newtonsoft.Json;

namespace TcOpen.Inxton.Mqtt
{
    public class JsonStringPayloadFormatter<T> : IPayloadFormatterFor<T>
    {
        public string Format(T plain) => JsonConvert.SerializeObject(plain);
    }
}
