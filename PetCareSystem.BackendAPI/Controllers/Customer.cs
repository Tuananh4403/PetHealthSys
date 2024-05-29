using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetCareSystem.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Customer : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Test ok");
        }
    }
}
