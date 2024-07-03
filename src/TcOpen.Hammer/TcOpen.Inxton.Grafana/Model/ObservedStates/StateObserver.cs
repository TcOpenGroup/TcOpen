using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Grafana.Backend.Model
{
    //legacy state observer
    [BsonIgnoreExtraElements]
    public class StateObserver
    {
        public DateTime _Created { get; set; }
        public DateTime _Modified { get; set; }
        public string Obserever { get; set; }
        public string ObsereverCu { get; set; }
        public string State { get; set; }
        public string StateDesc { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
