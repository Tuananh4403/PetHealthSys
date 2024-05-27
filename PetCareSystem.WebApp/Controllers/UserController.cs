using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AutoMapper;
using PetCareSystem.WebApp.Authorization;
using PetCareSystem.WebApp.Helpers;
using PetCareSystem.Data.Services.Users;
using Microsoft.AspNetCore.Authorization;
using PetHealthSys.PetCareSystem.WebApp.Models;
using AuthorizeAttribute = PetCareSystem.WebApp.Authorization.AuthorizeAttribute;

namespace PetHealthSys.PetCareSystem.WebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSetting _appSettings;

        public UserController(IUserService userService, IMapper mapper, AppSetting appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest request)
        {
            var response = _userService.Authenticate(request);
            return Ok(response);
        }
    }
}
