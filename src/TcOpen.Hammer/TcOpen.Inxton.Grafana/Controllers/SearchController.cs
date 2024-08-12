using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TcOpenHammer.Grafana.API.Controllers
{
    /// <summary>
    /// Will return every query name for grafana. So when you click on the dropdown menu in grafana, the query name will appear there.
    /// </summary>
    [Produces("application/json")]
    [Route("search")]
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IEnumerable<string> QueryNames;

        public SearchController(ILogger<SearchController> logger, QueryService queryService)
        {
            _logger = logger;
            QueryNames = queryService.QuerieNames.OrderBy(name => name);
        }

        [HttpPost]
        public IActionResult Post()
        {
            _logger.LogDebug("Query names requested");
            return new JsonResult(QueryNames);
        }
    }
}
