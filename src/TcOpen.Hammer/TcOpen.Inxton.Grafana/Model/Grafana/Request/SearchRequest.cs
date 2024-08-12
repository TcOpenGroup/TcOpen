namespace Grafana.Backend.Model
{
    /// Represents a request to the /search endpoint.  As described by the SimpleJson
    /// plugin docs, the following JSON represents this object:
    /// { target: 'upper_50' }
    public sealed class SearchRequest
    {
        public string Target { get; set; }
    }
}
