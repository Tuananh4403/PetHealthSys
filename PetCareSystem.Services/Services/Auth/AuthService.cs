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
using Microsoft.Extensions.Configuration;

namespace PetCareSystem.Services.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

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

        public async Task RegisterAsync(string username, string password, string firstName, string lastName, string email)
        {
            var user = new User
            {
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Birthday = DateTime.Now,
                PhoneNumber = "0900003",
                 Address = "test",
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

            IConfiguration configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .Build();

            // Access configuration settings
            var appSetting = configuration["AppSettings:Secret"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSetting);
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

        public Task<User?> GetById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }
    }
}