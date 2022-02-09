using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TcOpenHammer.Grafana.API.Controllers
{
    /// <summary>
    /// Endpoint which is required by Grafana, but we don't use it.
    /// https://grafana.com/docs/grafana/latest/dashboards/annotations/
    /// </summary>
    [Produces("application/json")]
    [Route("annotations")]
    public class AnnotationsController : Controller
    {
        private readonly ILogger<AnnotationsController> _logger;

        public AnnotationsController(ILogger<AnnotationsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] string value) => new JsonResult(
            new
            {
                Controller = nameof(AnnotationsController),
                Method = nameof(Post),
                RecievedData = value
            }
        );
    }
}
