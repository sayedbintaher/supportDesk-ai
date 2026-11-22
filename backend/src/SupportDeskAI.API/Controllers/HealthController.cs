using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SupportDeskAI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok(
                new
                {
                    Status = "Healthy",
                    Service = "SupportDesk AI API",
                    Timestamp = DateTime.UtcNow
                }
            );
        }
    }
}