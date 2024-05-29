using System.Threading.Tasks;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Users;
using PetCareSystem.Services.Auth;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PetCareSystem.Services.Helpers;

namespace PetCareSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSetting _appSetting;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResult> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !VerifyPasswordHash(password, user.Password))
            {
                return new AuthenticationResult { Success = false, Message = "Invalid credentials" };
            }

            var token = GenerateToken(user);
            return new AuthenticationResult { Success = true, Token = token };
        }

        public async Task RegisterAsync(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = HashPassword(password)
            };

            await _userRepository.AddUserAsync(user);
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // Implement password hash verification
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password)
        {
            // Implement password hashing
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}