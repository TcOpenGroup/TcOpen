namespace Grafana.Backend.Model
{

    /// Represents a Filter.  As described by the SimpleJson
    /// plugin docs, the following JSON represents this object:
    ///     {
    ///       "name": "deploy",
    ///       "datasource": "Simple JSON Datasource",
    ///       "iconColor": "rgba(255, 96, 96, 1)",
    ///       "enable": true,
    ///       "query": "#deploy"
    ///     }
    public sealed class AnnotationDescriptor {
    public string Name { get; set; }
    public string Datasource { get; set; }
    public string IconColor { get; set; }
    public bool Enable { get; set; }
    public string Query { get; set; }
  }
}
