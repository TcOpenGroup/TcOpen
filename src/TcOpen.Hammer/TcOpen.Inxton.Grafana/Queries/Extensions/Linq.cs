using System;
using System.Collections.Generic;
using System.Linq;

namespace Grafana.Backend.Queries
{
    internal static class LinqExt
    {
        internal static T As<T>(this object @object) where T : class => (@object as T);

        internal static T2 Let<T, T2>(this T @object, Func<T, T2> action) => action(@object);

        internal static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (T item in sequence) action(item);
        }

        internal static double Median(this IEnumerable<double> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var data = source.OrderBy(n => n).ToArray();
            if (data.Length == 0)
                throw new InvalidOperationException();
            if (data.Length % 2 == 0)
                return (data[data.Length / 2 - 1] + data[data.Length / 2]) / 2.0;
            return data[data.Length / 2];
        }

        internal static string JoinToString<T>(this IEnumerable<T> sequence, string separator = ",")
            => string.Join(separator, sequence);

        internal static object GetPropertyValue(this object src, string propName)
        {
            if (src == null) throw new ArgumentException("Value cannot be null.", nameof(src));
            if (propName == null) throw new ArgumentException("Value cannot be null.", nameof(propName));

            if (propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                return prop?.GetValue(src, null);
            }
        }

    }
}
