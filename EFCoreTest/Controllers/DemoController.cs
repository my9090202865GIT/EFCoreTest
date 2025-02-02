using DotNetTry.serviceInject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCoreTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IDemoService _demoService;

        public DemoController(IDemoService demoService)
        {
            this._demoService = demoService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            Console.WriteLine("ggeheh");
            _demoService.GetXyz("Sample text from controller");
            return _demoService.Description();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAsync(int id)
        {
            _demoService.GetXyz("GetAllAsync method.");
            var result = _demoService.Description();
            return Ok(result);
        }

        [HttpGet]
        public void ErrorEndpoint(int param)
        {
            if (param == 0)
            {
                throw new ArgumentException();
            }
        }
    }
}