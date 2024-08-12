using System.Collections.Generic;
using Grafana.Backend.Model;

namespace TcOpenHammer.Grafana.API.Transformation
{
    public interface ITable
    {
        IEnumerable<Column> Columns { get; }
        IEnumerable<IEnumerable<object>> Rows { get; }

        string RefId { get; set; }
    }

    public static class ITableExtension
    {
        public static ITable WithRefId(this ITable table, string refId)
        {
            table.RefId = refId;
            return table;
        }
    }
}
