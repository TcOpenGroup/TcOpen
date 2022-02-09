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
        public StationModesService(MongoService databaseService)
        {
            _databaseService = databaseService;
            Queries = QueryCreator.QueriesFor<enumModesObservedValue>();
        }

        public IEnumerable<string> QuerieNames => Queries.Keys.ToList();
        public ITable ExecuteQuery(QueryRequest request, string target) => Queries[target]
            .Query(
                _databaseService.StationModes.AsQueryable(),
                request.Range.From.DateTime,
                request.Range.To.DateTime
            );
    }
}
