using System;
using System.Linq;
using TcOpenHammer.Grafana.API.Transformation;

namespace Grafana.Backend.Queries
{
    internal interface IQuery<T>
    {
        ITable Query(IQueryable<T> production, DateTime from, DateTime to);
        string QueryName() => GetType().Name;
    }
}
