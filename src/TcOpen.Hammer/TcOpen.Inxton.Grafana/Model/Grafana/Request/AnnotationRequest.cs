namespace Grafana.Backend.Model
{

    /// Represents a request to the /annotations endpoint.  As described by the SimpleJson
    /// plugin docs, the following JSON represents this object:
    /// {
    ///   "range": {
    ///     "from": "2016-04-15T13:44:39.070Z",
    ///     "to": "2016-04-15T14:44:39.070Z"
    ///   },
    ///   "rangeRaw": {
    ///     "from": "now-1h",
    ///     "to": "now"
    ///   },
    ///   "annotation": {
    ///     "name": "deploy",
    ///     "datasource": "Simple JSON Datasource",
    ///     "iconColor": "rgba(255, 96, 96, 1)",
    ///     "enable": true,
    ///     "query": "#deploy"
    ///   }
    /// }
    public sealed class AnnotationRequest {
    public TimeRange Range { get; set; }
    public AnnotationDescriptor Annotation { get; set; }
  }
}
