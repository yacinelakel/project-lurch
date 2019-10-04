using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
