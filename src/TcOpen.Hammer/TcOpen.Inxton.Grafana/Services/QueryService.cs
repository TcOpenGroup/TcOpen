using System;
using System.Collections.Generic;
using System.Linq;
using Grafana.Backend.Model;
using TcOpenHammer.Grafana.API.Transformation;

namespace TcOpenHammer.Grafana.API
{
    public class QueryService
    {
        private readonly ProcessDataService _processDataService;
        private readonly StationModesService _stateObserverService;

        public QueryService(
            ProcessDataService processDataService,
            StationModesService stateObserverService
        )
        {
            _processDataService = processDataService;
            _stateObserverService = stateObserverService;
        }

        public IEnumerable<ITable> ExecuteQueries(QueryRequest request) =>
            request.Targets.Select(target =>
                ServiceForRequestedQuery(target.Target)
                    .ExecuteQuery(request.NormalizeTime(), target.Target)
                    .WithRefId(target.RefId)
            );

        public IEnumerable<string> QuerieNames =>
            Enumerable
                .Empty<string>()
                .Concat(_processDataService.QuerieNames)
                .Concat(_stateObserverService.QuerieNames);

        private IQueryableService ServiceForRequestedQuery(string nameOfRequest)
        {
            if (_processDataService.QuerieNames.Contains(nameOfRequest))
                return _processDataService;

            if (_stateObserverService.QuerieNames.Contains(nameOfRequest))
                return _stateObserverService;

            throw new Exception($"Query {nameOfRequest} was not found.");
        }
    }
}
