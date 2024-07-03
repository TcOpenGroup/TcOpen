using System;

namespace Grafana.Backend.Model
{
    public interface IObservedValue<T>
    {
        dynamic _recordId { get; set; }
        string _EntityId { get; set; }
        DateTime Timestamp { get; set; }
        string Name { get; set; }
        T Value { get; set; }
        string ValueDescription { get; set; }
    }
}
