using System;
using System.Linq;


namespace Grafana.Backend.Model
{
    internal enum enumModes
    {
        Idle = 0,
        Automat = 1000,
        Ground = 2000,
        Service = 3000
    }
    public class enumModesObservedValue : IObservedValue<ushort>
    {
        public dynamic _recordId { get; set; }
        public string _EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Name { get; set; }
        public ushort Value { get; set; }
        public string ValueDescription { get; set; }

        public enumModesObservedValue(ushort value)
        {
            Timestamp = DateTime.Now;
            Value = value;
            ValueDescription = Enum.GetValues(typeof(enumModes)).Cast<enumModes>().FirstOrDefault(x => (int)x == value).ToString();
            _recordId = Guid.NewGuid().ToString();
        }
    }

}