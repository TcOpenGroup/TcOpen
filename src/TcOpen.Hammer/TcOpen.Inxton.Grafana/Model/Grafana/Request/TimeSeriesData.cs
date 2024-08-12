using System;

namespace Grafana.Backend.Model
{
    /// Represents a data point in a TimeSeries.
    public sealed class TimeSeriesData
    {
        public double Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
