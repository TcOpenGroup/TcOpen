using System;
using System.Collections.Generic;
using System.Linq;

namespace Grafana.Backend.Model
{
    /// Represents a request to the /query endpoint.  As described by the SimpleJson
    /// plugin docs, the following JSON represents this object:
    /// {
    ///   "panelId": 1,
    ///   "range": {
    ///     "from": "2016-10-31T06:33:44.866Z",
    ///     "to": "2016-10-31T12:33:44.866Z",
    ///     "raw": {                   // ignored
    ///       "from": "now-6h",        // ignored
    ///       "to": "now"              // ignored
    ///     }
    ///   },
    ///   "rangeRaw": {                // ignored
    ///     "from": "now-6h",          // ignored
    ///     "to": "now"                // ignored
    ///   },
    ///   "interval": "30s",
    ///   "intervalMs": 30000,
    ///   "targets": [
    ///      { "target": "upper_50", "refId": "A", "type": "timeserie" },
    ///      { "target": "upper_75", "refId": "B", "type": "timeserie" }
    ///   ],
    ///   "adhocFilters": [{
    ///     "key": "City",
    ///     "operator": "=",
    ///     "value": "Berlin"
    ///   }],
    ///   "format": "json",
    ///   "maxDataPoints": 550
    /// }
    public sealed class QueryRequest
    {
        public long PanelId { get; set; }
        public TimeRange Range { get; set; }
        public int IntervalMs { get; set; }
        public string Timezone { get; set; }
        public IEnumerable<Filter> AdhocFilters { get; set; }
        public IEnumerable<TargetMetric> Targets { get; set; }
        public int? MaxDataPoints { get; set; }

        public QueryRequest NormalizeTime()
        {
            var hourDifference = DateTime.Now.Hour - Range.To.Hour;
            Range.To = Range.To.AddHours(hourDifference);
            Range.From = Range.From.AddHours(hourDifference);
            return this;
        }
    }
}
