using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PetCareSystem.Services;
using PetCareSystem.WebApp.Models.Auth;
using System.Threading.Tasks;
using PetCareSystem.Services.Auth;

namespace PetCareSystem.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(model.Username, model.Password);
            if (result.Success)
            {
                return Ok(result.Token);
            }

            return Unauthorized(result.Message);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authService.RegisterAsync(model.Username, model.Password);
            return Ok();
        }

        [HttpGet("protected")]
        [Authorize]
        public IActionResult Protected()
        {
            return Ok("This is a protected endpoint");
        }
    }
}
