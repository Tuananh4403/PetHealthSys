using Microsoft.AspNetCore.Identity;
using PetCareSystem.Data.Entites;
using PetCareSystem.ViewModels.System.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace PetCareSystem.Application.System.Users
{
    public class UserService : IUserService
    {
        
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly IConfiguration _config;
        public UserService(UserManager<Customer> userManager, SignInManager<Customer> signInManager, IConfiguration config) 
        { 
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }
        async Task<string> IUserService.Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return null;

            var result = _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (result.IsCompletedSuccessfully)
            {
                return null;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.GivenName, user.LastName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
            _config["Tokens:Issuer"],
            claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var user = new Customer()
            {
                UserName = request.UserName,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Birthday = request.Birthday,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if(result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Register(Microsoft.AspNetCore.Identity.Data.RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
