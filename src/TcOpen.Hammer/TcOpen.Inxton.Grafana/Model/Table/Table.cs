using Grafana.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TcOpenHammer.Grafana.API.Transformation
{
    public class Table : ITable
    {
        public IEnumerable<Column> Columns { get; set; }
        public IEnumerable<IEnumerable<object>> Rows { get; set; }
#pragma warning disable IDE1006 // Naming Styles
        private IList<List<object>> _rows => Rows as IList<List<object>>;
        private IList<Column> _columns => Columns as IList<Column>;
#pragma warning restore IDE1006 // Naming Styles

        public string RefId { get; set; }

        public Table()
        {
            Columns = new List<Column>();
            Rows = new List<List<object>>();
        }

        public void AddRow(List<object> row) => _rows.Add(row);
        public void AddColumn(Column column) => _columns.Add(column);

    }

    public class Table<T> : ITable
    {
        public IEnumerable<Column> Columns { get; set; }
        public IEnumerable<IEnumerable<object>> Rows { get; set; }
        public string RefId { get; set; }

        public Table(IEnumerable<T> source)
        {
            if (source.Any())
            {
                Columns = HeaderFromType();
                Rows = RowsFromList(source);
            }
            else
            {
                Columns = HeaderFromType();
                Rows = DefaultValuesForType();
            }
        }

        private static IEnumerable<Column> HeaderFromType() => typeof(T)
               .GetProperties()
               .Select(propertyInfo => new Column(propertyInfo));

        private IEnumerable<IEnumerable<object>> DefaultValuesForType()
        {
            return new List<IEnumerable<object>> { Columns.Select(x => DefaultValuesForType(x.UnderlyingType)) };
        }

        private static object DefaultValuesForType(Type underlyingType)
        {
            if (underlyingType == typeof(string))
                return "No data";
            if (underlyingType == typeof(int))
                return 0;
            if (underlyingType == typeof(double))
                return 0.0;
            if (underlyingType == typeof(float))
                return 0.0f;
            if (underlyingType == typeof(DateTime))
                return DateTime.Now;
            if (underlyingType == typeof(TimeSpan))
                return TimeSpan.Zero;
            return "";
        }

        private static IEnumerable<IEnumerable<object>> RowsFromList(IEnumerable<T> source)
        {
            var properties = typeof(T).GetProperties();
            return source.Select(item => properties.Select(prop => prop.GetValue(item)));
        }

    }
}
