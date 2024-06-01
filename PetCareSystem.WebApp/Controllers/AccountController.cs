using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetCareSystem.WebApp.Models.Auth;
using System.Net.Http.Headers;

namespace PetCareSystem.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
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
    }
}
