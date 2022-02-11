using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TcOpenHammer.Grafana.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;

        public StatusController(ILogger<StatusController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Status requested");
            return Ok();
        }
    }
}
