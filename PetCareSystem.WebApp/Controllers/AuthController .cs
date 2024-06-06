using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PetCareSystem.WebApp.Models.Auth;
using PetCareSystem.Services.Services.Models.Auth;
using PetRequest = PetCareSystem.WebApp.Models.Auth.PetRequest;

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
        public async Task<IActionResult> Login(AuthenticateRequest model)
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
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authService.RegisterAsync(model.Username, model.Password);
            return Ok();
        }

        [HttpPost("register-pet")]
        public async Task<IActionResult> RegisterPet(PetRequest model)
        {
            // Check if the model state is NOT valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool isRegistered = await _authService.RegisterPetAsync(model);
                if (isRegistered)
                {
                    return Ok(new { message = "Pet registration successful" });
                }
                else
                {
                    return BadRequest(new { message = "Pet registration failed" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while registering the pet", details = ex.Message });
            }
        }


        [HttpGet("protected")]
        [Authorize]
        public IActionResult Protected()
        {
            return Ok("This is a protected endpoint");
        }
    }
}
