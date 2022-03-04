using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Grafana.Backend.Model
{

    /// Represents a Filter.  As described by the SimpleJson
    /// plugin docs, the following JSON represents this object:
    ///   {
    ///     "key": "City",
    ///     "operator": "=",
    ///     "value": "Berlin"
    ///   }
    public sealed class Filter {
    public string Key { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public EOperator Operator { get; set; }

    public string Value { get; set; }
  }
}
