using System.Threading.Tasks;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Users;
using PetCareSystem.Data.Repositories.Customers;
using PetCareSystem.Services.Models.Auth;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using PetCareSystem.Services.Helpers;

namespace PetCareSystem.Services.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;

        public AuthService(IUserRepository userRepository, ICustomerRepository customerRepository)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
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

        public async Task RegisterAsync(RegisterRequest model)
        {
            if(_userRepository.GetUserByEmail(model.Email) == null || _userRepository.GetUserByPhone(model.Phone) == null)
            {
                throw new AppException("Email  or Phone is already taken");
            }
            var user = new User
            {
                Username = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Birthday = DateTime.Now,
                PhoneNumber = model.Phone,
                Address = "test",
                Password = HashPassword(model.Password)
            };
           
            await _userRepository.AddUserAsync(user);
            if (model.IsCustomer)
            {
                var customer = new Customer
                {
                    UserId = user.Id, // Assuming there's a UserId property in the Customer model
                    // Assign other properties for the Customer model
                };

                // Add the customer
                await _customerRepository.AddCustomerAsync(customer);
            }
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

        public Task RegisterAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}