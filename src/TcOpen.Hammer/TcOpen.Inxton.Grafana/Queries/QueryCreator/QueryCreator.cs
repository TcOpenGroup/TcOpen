using Grafana.Backend.Queries.Extensions;
using PlcHammer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grafana.Backend.Queries
{
    internal static class QueryCreator
    {
        internal static IDictionary<string, IQuery<T>> QueriesFor<T>() => typeof(QueryCreator)
            .Assembly
            .GetTypes()
            .Where(p => typeof(IQuery<T>).IsAssignableFrom(p)
                && !p.IsInterface
                && !p.IsAbstract
                && !p.GetCustomAttributes(typeof(QueryTemplateAttribute), true).Any()
            )
            .Select(x => Activator.CreateInstance(x) as IQuery<T>)
            .ToDictionary(x => x.GetType().Name);
    }
}
