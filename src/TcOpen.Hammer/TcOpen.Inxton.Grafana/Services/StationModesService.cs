using TcOpenHammer.Grafana.API.Model.Mongo;
using TcOpenHammer.Grafana.API.Transformation;
using Grafana.Backend.Model;
using Grafana.Backend.Queries;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace TcOpenHammer.Grafana.API
{
    public class StationModesService : IQueryableService
    {
        private readonly MongoService _databaseService;
        private readonly IDictionary<string, IQuery<enumModesObservedValue>> Queries;
        private readonly IDictionary<string, IQuery<ObservedValue<string>>> ManualyDefinedQueries;
        public StationModesService(MongoService databaseService)
        {
            _databaseService = databaseService;
            Queries = QueryCreator.QueriesFor<enumModesObservedValue>();
            ManualyDefinedQueries = new Dictionary<string, IQuery<ObservedValue<string>>>();
            ManualyDefinedQueries.Add("LastMode", new LastModeInDb());
        }


        public IEnumerable<string> QuerieNames => Queries.Keys.Concat(ManualyDefinedQueries.Keys);

        public ITable ExecuteQuery(QueryRequest request, string target)
        {
            if (ManualyDefinedQueries.ContainsKey(target))
                return ManualyDefinedQueries[target]
                    .Query(_databaseService.RecipeHistory.AsQueryable(), request.Range.From.DateTime, request.Range.To.DateTime);
            else
                return Queries[target]
                    .Query(_databaseService.StationModes.AsQueryable(), request.Range.From.DateTime, request.Range.To.DateTime);
        }
    }
}
