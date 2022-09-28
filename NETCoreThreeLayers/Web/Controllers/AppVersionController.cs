using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/app")]
    [ApiController]
    public class AppVersionController : ControllerBase
    {
        public AppVersionController()
        {
        }

        [HttpGet("version")]
        public IActionResult Get()
        {
            return Ok(new
            {
                Id = 1,
                Name = "Sample Application",
                Version = "1.0.0"
            });
        }
    }
}