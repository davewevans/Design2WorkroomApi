using Design2WorkroomApi.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Design2WorkroomApi.Controllers
{
    [Route("api/logger")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private readonly ILogger<SeedController> _logger;

        public LoggerController(ILogger<SeedController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("This was logged from the D2W API");
            _logger.LogError("And error has occurred");
            _logger.LogCritical("This is a critical log");
            _logger.LogDebug("This is a debug log");
            return Ok();
        }
    }
}
