using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PetCareSystem.Services.Models.Auth;
using PetCareSystem.Services;
using System.Threading.Tasks;
using PetCareSystem.Services.Services.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using PetCareSystem.Services.Models.Auth;

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
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(AuthenticateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _authService.LoginAsync(model.Username, model.Password);
                if (result.Success)
                {
                    return Ok(new { message = "Login successful", token = result.Token });
                }

                return Unauthorized(new { message = "Username or password is incorrect" });
            }
            catch (Exception ex)
            {
                // Log the exception (you might need to inject a logger)
                Console.WriteLine($"Login error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred during login" });
            }
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authService.RegisterAsync(model);
            return Ok("User have been created");
        }



        [HttpGet("protected")]
        [Authorize]
        public IActionResult Protected()
        {
            return Ok("This is a protected endpoint");
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result?.Principal != null)
            {
                // Extract the user's information from the result
                var accessToken = result?.Properties?.GetTokenValue("access_token");
                /*var claims = result?.Principal?.Identities?.FirstOrDefault()?.Claims;
                var token = claims.FirstOrDefault(c => c.Type == "access_token")?.Value;*/

                // Use the token to get the user's profile information
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.GetAsync("https://openidconnect.googleapis.com/v1/userinfo");
                response.EnsureSuccessStatusCode();
                var userInfo = await response.Content.ReadAsStringAsync();

                return Ok(JsonConvert.DeserializeObject<GoogleResponse>(userInfo));
            }

            return BadRequest("Unable to complete Google login.");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Logged out successfully.");
        }
        [AllowAnonymous]
        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole(CreateRoleReq model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authService.CreateRole(model);
            return Ok("Role have been created");
        }

    }
}
