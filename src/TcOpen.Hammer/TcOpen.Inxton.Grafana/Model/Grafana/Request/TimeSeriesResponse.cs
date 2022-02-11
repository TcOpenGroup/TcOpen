using System.Collections.Generic;

namespace Grafana.Backend.Model
{

    /// Represents a individual metric's values in the response to /query.
    /// From the SimpleJson plugin docs:
    ///   {
    ///     "target":"upper_75", // The field being queried for
    ///     "datapoints":[
    ///       [622,1450754160000],  // Metric value as a float , unixtimestamp in milliseconds
    ///       [365,1450754220000]
    ///     ]
    ///   }
    public sealed class TimeSeriesResponse : IQueryResponse {
    public string Target { get; set; }
    public IEnumerable<IEnumerable<double>> DataPoints { get; set; } = new List<IEnumerable<double>>();
  }
}
