using PlcHammer;
using System;
using System.Linq;
using TcOpenHammer.Grafana.API.Transformation;

namespace Grafana.Backend.Queries
{
    public class Checks : IQuery<PlainStation001_ProductionData>
    {
        public ITable Query(IQueryable<PlainStation001_ProductionData> production, DateTime from, DateTime to) => production
            .AsQueryable()
            .Take(1)
            .ToTable();
     }


}
