using System.Runtime.Serialization;

namespace Grafana.Backend.Model
{
    /// Represents an operator in a filter.
    public enum EMetricType
    {
        [EnumMember(Value = "timeserie")]
        Timeseries,

        [EnumMember(Value = "table")]
        Table,
    }
}
