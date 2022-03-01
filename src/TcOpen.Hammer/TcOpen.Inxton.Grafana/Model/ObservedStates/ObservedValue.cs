using System;
using System.Linq;

namespace Grafana.Backend.Model
{
    public class ObservedValue<T> : IObservedValue<T>
    {
        public dynamic _recordId { get; set; }
        public string _EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Name { get; set; }
        public T Value { get; set; }
        public string ValueDescription { get; set; }

        public ObservedValue(T value)
        {
            Timestamp = DateTime.Now;
            Value = value;
        }
    }

 

}