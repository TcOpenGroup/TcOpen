namespace Grafana.Backend.Model
{
    /// Represents an individual element sent back from the
    /// /search endpoint.  For example, one element of the following array:
    ///
    /// [ { "text" :"upper_25", "value": 1}, { "text" :"upper_75", "value": 2} ]
    public sealed class SearchMetric
    {
        public string Text { get; set; }
        public object Value { get; set; }
    }
}
