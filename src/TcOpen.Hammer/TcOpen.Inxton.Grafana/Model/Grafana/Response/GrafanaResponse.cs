using System.Collections.Generic;
using System.Linq;
using TcOpenHammer.Grafana.API.Transformation;

namespace Grafana.Backend.Model
{
    public class GrafanaResponse
    {
        public List<Column> Columns { get; set; }
        public List<List<object>> Rows { get; set; }
        public string Type { get; set; } = "table";

        public string Refid { get; set; }

        public GrafanaResponse() { }

        public GrafanaResponse(ITable table)
        {
            Columns = table.Columns.ToList();
            Rows = table.Rows.Select(x => x.ToList()).ToList();
            Refid = table.RefId;
        }
    }
}
