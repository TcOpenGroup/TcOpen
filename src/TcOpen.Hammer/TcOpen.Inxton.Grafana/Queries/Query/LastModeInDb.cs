using System;
using System.Linq;
using Grafana.Backend.Model;
using Grafana.Backend.Queries;
using TcOpenHammer.Grafana.API.Transformation;

namespace TcOpenHammer.Grafana.API
{
    public class LastModeInDb : IQuery<ObservedValue<string>>
    {
        public ITable Query(
            IQueryable<ObservedValue<string>> recipeHistory,
            DateTime from,
            DateTime to
        ) =>
            recipeHistory
                .OrderByDescending(x => x.Timestamp)
                .Select(x => new { RecipeName = x.Value })
                .Take(1)
                .ToTable();
    }
}
