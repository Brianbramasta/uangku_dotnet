using Microsoft.AspNetCore.Mvc;

namespace MySimpleApi.Controllers
{
    [ApiController]
    [Route("hello")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Halo dari .NET Web API!");
        }
    }
}
