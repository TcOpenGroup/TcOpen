namespace Grafana.Backend.Model
{
    /// Represents a request to the /tag-values endpoint.  As described by the SimpleJson
    /// plugin docs, the following JSON represents this object:
    /// {"key": "City"}
    public sealed class TagValuesRequest
    {
        public string Key { get; set; }
    }
}
