using Microsoft.AspNetCore.Mvc;

namespace Lurch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeartbeatController : ControllerBase
    {
        // GET api/heartbeat
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}