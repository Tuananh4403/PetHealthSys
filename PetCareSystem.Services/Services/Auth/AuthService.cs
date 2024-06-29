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
using PetCareSystem.Data.Repositories.Roles;
using PetCareSystem.Data.Repositories.Doctors;
using PetCareSystem.Services.Models;
using PetCareSystem.Data.Repositories.UserRoles;
using PetCareSystem.Data.Repositories.Staffs;

namespace PetCareSystem.Services.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUserRolesRepository _userRolesRepository;
        private readonly IStaffRepository _staffRepository;

        public AuthService(IUserRepository userRepository, ICustomerRepository customerRepository, IRoleRepository roleRepository, 
            IDoctorRepository doctorRepository, IUserRolesRepository userRolesRepository, IStaffRepository staffRepository)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _roleRepository = roleRepository;
            _doctorRepository = doctorRepository;
            _staffRepository = staffRepository;
        }

        public async Task<ApiResponse<AuthenticateResponse>> LoginAsync(string username, string password)
        {
            User user = await _userRepository.GetUserByEmail(username);
            List<Role> listRoles = [];
            var userRoles = user.UserRoles;

            foreach (var userRole in userRoles)
            {
                Role role = userRole.Role;
                Console.WriteLine($"Role id: {role.Id}, Title: {role.Title}");

                if (role != null)
                {
                    listRoles.Add(role); // Add role to the list
                }
            }

            if (user == null || !VerifyPasswordHash(password, user.Password))
            {
                return null;
            }

            var token = GenerateToken(user);
            return new ApiResponse<AuthenticateResponse>(new AuthenticateResponse( user, token, listRoles), "Login success");
        }

        public async Task<ApiResponse<String>> RegisterAsync(RegisterRequest model)
        {
            var existingUserByEmail = await _userRepository.GetUserByEmail(model.Email);
            if (existingUserByEmail != null)
            {
                return new ApiResponse<string>("Email is already taken", true);
            }

            var existingUserByPhone = await _userRepository.GetUserByPhone(model.Phone);
            if (existingUserByPhone != null)
            {
                return new ApiResponse<string>("Phone number is already taken", true);
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
           
            await _userRepository.AddUserAsync(user, model.RoleId);
            if (model.IsCustomer)
            {
                var customer = new Customer
                {
                    UserId = user.Id, // Assuming there's a UserId property in the Customer model
                    // Assign other properties for the Customer model
                };

                // Add the customer
                var check = await _customerRepository.AddAsync(customer);
                if (check)
                {
                    return new ApiResponse<string>("Registration successful", false);

                }
            }
            else
            {
                Role role = await _roleRepository.GetRoleByIdAsync(model.RoleId ?? 2);
                if(role.Title == "DT")
                {
                    Doctor doc =new Doctor { UserId = user.Id };
                    var check = await _doctorRepository.AddAsync(doc);
                    if (check)
                    {
                        return new ApiResponse<string>("Create doctor successful",false);

                    }
                }
                if (role.Title == "ST")
                {
                    Staff staff = new Staff { UserId = user.Id };
                    var check = await _staffRepository.AddAsync(staff);
                    if (check)
                    {
                        return new ApiResponse<string>("Create staff successful", false);
                    }
                }
            }
            return new ApiResponse<string>("Create doctor successful", false);
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
            return _userRepository.GetByIdAsync(userId);
        }

        public async Task CreateRole(CreateRoleReq model)
        {
            if(await _roleRepository.GetRoleByTitleAsync(model.Title) != null){
                throw new AppException("Role is already created");
            }
            var role = new Role{
                Title = model.Title,
                Name = model.Name
            };
            await _roleRepository.AddAsync(role);
        }

        public async Task<IEnumerable<Role>> GetListRole()
        {
            return await _roleRepository.GetAllAsync(); 
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}