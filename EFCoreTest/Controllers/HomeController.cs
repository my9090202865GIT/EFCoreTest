using Microsoft.AspNetCore.Mvc;

namespace EFCoreTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]  // Map root URL
        public IActionResult Get()
        {
            var sampleData = new { message = "This is a sample JSON response!.Server is up and running." };
            return Ok(sampleData); // Return JSON by default
        }
    }
}
