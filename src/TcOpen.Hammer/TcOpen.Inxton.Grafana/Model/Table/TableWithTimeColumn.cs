using System;
using System.Collections.Generic;
using System.Linq;
using Grafana.Backend.Model;

namespace TcOpenHammer.Grafana.API.Transformation
{
    public class TableWithTimeColumn<T> : Table<T>
    {
        public TableWithTimeColumn(IEnumerable<T> source)
            : base(source)
        {
            var dateString = DateTime.Now.ToString();
            Columns = Columns.Prepend(TimeColumn);
            Rows = Rows.Select(x => x.Prepend(dateString));
        }

        private static Column TimeColumn => new Column { Text = "Time", Type = "time" };
    }
}
