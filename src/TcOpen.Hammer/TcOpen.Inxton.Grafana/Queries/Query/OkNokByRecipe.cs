using System;
using System.Collections.Generic;
using System.Linq;
using PlcHammer;
using TcOpenHammer.Grafana.API.Transformation;

namespace Grafana.Backend.Queries
{
    public class OkNokByRecipe : IQuery<PlainStation001_ProductionData>
    {
        public ITable Query(
            IQueryable<PlainStation001_ProductionData> production,
            DateTime from,
            DateTime to
        ) =>
            production
                .AsQueryable()
                .Select(x => new { Recipe = x.RecipeName, Result = x.Result })
                .GroupBy(x => x.Recipe)
                .Select(x => new
                {
                    Recipe = x.Key,
                    OK = x.Where(x => x.Result).Count(),
                    NOK = x.Where(x => !x.Result).Count()
                })
                .OrderBy(x => x.Recipe)
                .ToTable();
    }

    public class TotalOK : IQuery<PlainStation001_ProductionData>
    {
        public ITable Query(
            IQueryable<PlainStation001_ProductionData> production,
            DateTime from,
            DateTime to
        )
        {
            var Good = production.AsQueryable().Count(x => x.Result == true);
            var Bad = production.AsQueryable().Count(x => x.Result == false);
            return new List<Result>
            {
                new Result { Good = Good, Bad = Bad }
            }.ToTable();
        }

        class Result
        {
            public int Good { get; set; }
            public int Bad { get; set; }
        };
    }
}
