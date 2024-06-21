using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PetCareSystem.Services.Services.Auth;
using PetCareSystem.Services.Helpers;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PetCareSystem.WebApp.Helpers
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext, IAuthService authService)
        {
            if (httpContext.Request.Path.StartsWithSegments("/api/auth/authenticate") || httpContext.Request.Path.StartsWithSegments("/api/auth/register"))
            {
                // If the request path matches, short-circuit the middleware pipeline
                await _next(httpContext);
                return;
            }

            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await AttachUserToContext(httpContext, authService, token);
            }

            await _next(httpContext);
        }
        public async Task AttachUserToContext(HttpContext httpContext, IAuthService authService, string token)
        {

            // Access configuration settings
            var appSetting = _configuration["AppSettings:Secret"];
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSetting);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clock skew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                //Attach user to context on successful JWT validation
                httpContext.Items["User"] = await authService.GetById(userId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred in JwtMiddleware.");
            }
        }
    }
}
