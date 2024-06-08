using System.Threading.Tasks;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using PetCareSystem.Services.Services.Models.Auth;

namespace PetCareSystem.Services
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

        public async Task RegisterAsync(string username, string password )
        {
            var user = new User
            {
                Username = username,
                Password = HashPassword(password)
            };

            await _userRepository.AddUserAsync(user);
        }

        public async Task<bool> RegisterPetAsync(PetRequest model)
        {
            // Validate the provided information
            if (model.PetId <= 0 || string.IsNullOrEmpty(model.PetName) || string.IsNullOrEmpty(model.KindOfPet) ||
                model.Birthday == DateTime.MinValue || string.IsNullOrEmpty(model.Species) || model.CustomerId <= 0 || model.Birthday > DateTime.Now)
            {
                // Return false or throw an exception if the validation fails
                return false;
            }

            // Create a new pet object
            var pet = new Pet
            {
                PetId = model.PetId,
                PetName = model.PetName,
                KindOfPet = model.KindOfPet,
                Gender = model.Gender,
                Birthday = model.Birthday,
                Species = model.Species,
                CustomerId = model.CustomerId
            };

            // Add the new pet to the repository
            await _userRepository.AddPetAsync(pet);

            // Return true if the pet was registered successfully
            return true;
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
    }
}