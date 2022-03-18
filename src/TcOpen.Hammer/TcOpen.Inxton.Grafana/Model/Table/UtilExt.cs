using Grafana.Backend.Model;
using System.Collections.Generic;
using System.Linq;

namespace TcOpenHammer.Grafana.API.Transformation
{
    internal static class UtilExt
    {
        public static ITable ToTable<T>(this IEnumerable<T> enumerable) => new Table<T>(enumerable);
        public static ITable ToTableWithTimeColumn<T>(this IEnumerable<T> enumerable) => new TableWithTimeColumn<T>(enumerable);

        public static ITable Transpose(this ITable source)
        {
            var pivoted = new Table<object>(new List<object>());
            var firstColumn = source.Rows.Select(x => x.First().ToString());
            var secondColumn = source.Rows.Select(x => x.Last());
            pivoted.Columns = firstColumn.Select(x => new Column { Text = x, Type = "number" });
            if (secondColumn is IEnumerable<object>)
            {
                var test = secondColumn as IEnumerable<IEnumerable<object>>;
                var tes2 = secondColumn.GetType();
                ;
            }

            pivoted.Rows = new List<IEnumerable<object>> { secondColumn };
            return pivoted;
        }

    }
}
