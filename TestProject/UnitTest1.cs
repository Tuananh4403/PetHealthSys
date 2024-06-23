using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using PetCareSystem.Services.Helpers;
using PetCareSystem.Services.Services.Auth;
using PetCareSystem.Data.Repositories.Users;
using PetCareSystem.Data.Repositories.Customers;
using PetCareSystem.Data.Repositories.Roles;
using PetCareSystem.Data.Repositories.Doctors;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Services.Auth;
using PetCareSystem.Services.Models.Auth;

public class AuthServiceTests
{
    private readonly AuthService _authService;
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<ICustomerRepository> _mockCustomerRepository;
    private readonly Mock<IRoleRepository> _mockRoleRepository;
    private readonly Mock<IDoctorRepository> _mockDoctorRepository;

    public AuthServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockCustomerRepository = new Mock<ICustomerRepository>();
        _mockRoleRepository = new Mock<IRoleRepository>();
        _mockDoctorRepository = new Mock<IDoctorRepository>();
        _authService = new AuthService(_mockUserRepository.Object, _mockCustomerRepository.Object, _mockRoleRepository.Object, _mockDoctorRepository.Object);
    }
    [Fact]
    public async Task RegisterAsync_EmailAlreadyTaken_ThrowsAppException()
    {
        // Arrange
        var model = new RegisterRequest
        {
            Username = "testuser",
            FirstName = "Test",
            LastName = "User",
            Email = "test@example.com",
            Phone = "1234567890",
            Password = "password",
            RoleId = 1,
            IsCustomer = true
        };

        _mockUserRepository.Setup(r => r.GetUserByEmail(model.Email))
                            .ReturnsAsync(new User())
                            .Callback(() => Console.WriteLine($"GetUserByEmail called with {model.Email}"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _authService.RegisterAsync(model));
        Assert.Equal("Email is already taken", exception.Message);
    }
    [Fact]
    public async Task RegisterAsync_PhoneAlreadyTaken_ThrowsAppException()
    {
        // Arrange
        var model = new RegisterRequest
        {
            Username = "testuser",
            FirstName = "Test",
            LastName = "User",
            Email = "test@example.com",
            Phone = "1234567890",
            Password = "password",
            RoleId = 1,
            IsCustomer = true
        };

        _mockUserRepository.Setup(r => r.GetUserByPhone(model.Phone))
                            .ReturnsAsync(new User())
                            .Callback(() => Console.WriteLine($"GetUserByPhone called with {model.Phone}"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _authService.RegisterAsync(model));
        Assert.Equal("Phone number is already taken", exception.Message);
    }

    [Fact]
    public async Task RegisterAsync_ValidCustomerRegistration_AddsUserAndCustomer()
    {
        // Arrange
        var model = new RegisterRequest
        {
            Username = "testuser",
            FirstName = "Test",
            LastName = "User",
            Email = "test@example.com",
            Phone = "1234567890",
            Password = "password",
            RoleId = 1,
            IsCustomer = true
        };

        _mockUserRepository.Setup(r => r.GetUserByEmail(model.Email)).ReturnsAsync((User)null);
        _mockUserRepository.Setup(r => r.GetUserByPhone(model.Phone)).ReturnsAsync((User)null);
        _mockUserRepository.Setup(r => r.AddUserAsync(It.IsAny<User>(), model.RoleId)).Returns(Task.CompletedTask);
        _mockCustomerRepository.Setup(r => r.AddAsync(It.IsAny<Customer>())).Returns((Task<bool>)Task.CompletedTask);

        // Act
        await _authService.RegisterAsync(model);

        // Assert
        _mockUserRepository.Verify(r => r.AddUserAsync(It.IsAny<User>(), model.RoleId), Times.Once);
        _mockCustomerRepository.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once);
    }

    [Fact]
    public async Task RegisterAsync_ValidDoctorRegistration_AddsUserAndDoctor()
    {
        // Arrange
        var model = new RegisterRequest
        {
            Username = "testuser",
            FirstName = "Test",
            LastName = "User",
            Email = "test@example.com",
            Phone = "1234567890",
            Password = "password",
            RoleId = 2, // Assuming 2 corresponds to a Doctor role
            IsCustomer = false
        };

        var role = new Role { Id = 2, Title = "DT" };

        _mockUserRepository.Setup(r => r.GetUserByEmail(model.Email)).ReturnsAsync((User)null);
        _mockUserRepository.Setup(r => r.GetUserByPhone(model.Phone)).ReturnsAsync((User)null);
        _mockUserRepository.Setup(r => r.AddUserAsync(It.IsAny<User>(), model.RoleId)).Returns(Task.CompletedTask);
        _mockRoleRepository.Setup(r => r.GetRoleByIdAsync(model.RoleId ?? 2)).ReturnsAsync(role);
        _mockDoctorRepository.Setup(r => r.AddAsync(It.IsAny<Doctor>())).Returns((Task<bool>)Task.CompletedTask);

        // Act
        await _authService.RegisterAsync(model);

        // Assert
        _mockUserRepository.Verify(r => r.AddUserAsync(It.IsAny<User>(), model.RoleId), Times.Once);
        _mockDoctorRepository.Verify(r => r.AddAsync(It.IsAny<Doctor>()), Times.Once);
    }

}
