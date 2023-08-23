using Microsoft.AspNetCore.Mvc;

namespace ProbabilityService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProbabilityController:ControllerBase
    {
        [HttpGet]
        public ActionResult GetActionResult()
        {
            return Ok("this is what you needed");
        }
    }
}
