using System;

namespace Grafana.Backend.Model
{
    /// Represents a request to a time range.  As described by the SimpleJson
    /// plugin docs, the following JSON represents this object:
    ///   {
    ///     "from": "2016-10-31T06:33:44.866Z",
    ///     "to": "2016-10-31T12:33:44.866Z",
    ///     "raw": {
    ///       "from": "now-6h",
    ///       "to": "now"
    ///     }
    ///   }
    public sealed class TimeRange
    {
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }

        public TimeSpan AsTimeSpan()
        {
            return To - From;
        }
    }
}
