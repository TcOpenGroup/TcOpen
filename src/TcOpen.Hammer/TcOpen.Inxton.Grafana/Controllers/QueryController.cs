using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Grafana.Backend.Model;
using Grafana.Backend.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TcOpenHammer.Grafana.API.Transformation;

namespace TcOpenHammer.Grafana.API.Controllers
{
    /// <summary>
    /// This is the controller which is called when a query is requested by Grafana.
    /// In GrafanaRequest there may be multiple "targets" (read query names) which can be requested at once.
    /// </summary>
    [Produces("application/json")]
    [Route("query")]
    [ApiController]
    public class QueryController : Controller
    {
        private readonly ILogger<QueryController> _logger;
        private readonly QueryService queryService;

        public QueryController(ILogger<QueryController> logger, QueryService queryService)
        {
            _logger = logger;
            this.queryService = queryService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] QueryRequest request)
        {
            var queryName = request.Targets.Select(x => x.Target).JoinToString();

            if (string.IsNullOrEmpty(queryName))
            {
                _logger.LogWarning("Query name is empty");
                return BadRequest(
                    "You have to specify the name of the query - don't leave at \"select metric\" "
                );
            }

            try
            {
                var sw = new Stopwatch();
                _logger.LogInformation($"Executed {queryName}");
                sw.Start();
                var result = GrafanResult(
                    await Task.Run(() => queryService.ExecuteQueries(request))
                );
                sw.Stop();
                _logger.LogInformation($"OK [{sw.ElapsedMilliseconds} ms] \t {queryName}");
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"NOK - {queryName}. {request}");
                return BadRequest();
            }
        }

        private static IActionResult GrafanResult(IEnumerable<ITable> table) =>
            new JsonResult(new List<GrafanaResponse>(ResponseFromTables(table)));

        private static IEnumerable<GrafanaResponse> ResponseFromTables(IEnumerable<ITable> table) =>
            table.Select(t => new GrafanaResponse(t));
    }
}
