using System.Collections.Generic;
using System.Linq;
using Grafana.Backend.Model;
using Grafana.Backend.Queries;
using MongoDB.Driver;
using PlcHammer;
using TcOpenHammer.Grafana.API.Model.Mongo;
using TcOpenHammer.Grafana.API.Transformation;

namespace TcOpenHammer.Grafana.API
{
    /// <summary>
    /// This service groups together queries that require ProcessData collection for its execution.
    /// </summary>
    public class ProcessDataService : IQueryableService
    {
        private readonly MongoService _databaseService;
        private readonly IDictionary<
            string,
            IQuery<PlainStation001_ProductionData>
        > ProcessDataQueres;

        public ProcessDataService(MongoService databaseService)
        {
            _databaseService = databaseService;
            ProcessDataQueres = new Dictionary<
                string,
                IQuery<PlainStation001_ProductionData>
            >().MergeWith(QueryCreator.QueriesFor<PlainStation001_ProductionData>());
        }

        public IEnumerable<string> QuerieNames => ProcessDataQueres.Keys;

        public ITable ExecuteQuery(QueryRequest request, string target) =>
            ProcessDataQueres[target]
                .Query(
                    _databaseService.ProcessData.AsQueryable(),
                    request.Range.From.DateTime,
                    request.Range.To.DateTime
                );
    }

    public static class DictExt
    {
        public static IDictionary<K, V> MergeWith<K, V>(
            this IDictionary<K, V> dicA,
            IDictionary<K, V> dicB
        )
        {
            return dicA.Concat(dicB).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
