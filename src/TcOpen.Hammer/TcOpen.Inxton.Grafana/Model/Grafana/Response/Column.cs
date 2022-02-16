using Newtonsoft.Json;
using System;
using System.Reflection;

namespace Grafana.Backend.Model
{
    public class Column
    {
        public string Text { get; set; }
        public string Type { get; set; }
        
        [JsonIgnore]
        public Type UnderlyingType { get; }

        public Column()
        {

        }
        public Column(PropertyInfo info)
        {
            Text = info.Name;
            Type = (Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType).ToString();
            UnderlyingType = Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType;
        }
    }
}
