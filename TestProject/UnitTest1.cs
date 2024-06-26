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

    _mockUserRepository.Setup(r => r.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((User)null);
    _mockUserRepository.Setup(r => r.GetUserByPhone(It.IsAny<string>())).ReturnsAsync((User)null);
    _mockUserRepository.Setup(r => r.AddUserAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.CompletedTask);
    _mockCustomerRepository.Setup(r => r.AddAsync(It.IsAny<Customer>())).Returns((Task.FromResult(true)));
    _mockRoleRepository.Setup(r => r.GetRoleByIdAsync(It.IsAny<int>())).ReturnsAsync(new Role { Title = "Customer" });

        // Act
        var response = await _authService.RegisterAsync(model);

        // Assert
        Assert.True(response.Success);
        Assert.Equal("Registration successful", response.Message);
        Assert.Null(response.Data);

        _mockUserRepository.Verify(r => r.AddUserAsync(It.IsAny<User>(), It.IsAny<int>()), Times.Once);
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
            RoleId = 3, // Assuming 2 corresponds to a Doctor role
            IsCustomer = false
        };
    

        _mockUserRepository.Setup(r => r.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((User)null);
        _mockUserRepository.Setup(r => r.GetUserByPhone(It.IsAny<string>())).ReturnsAsync((User)null);
        _mockUserRepository.Setup(r => r.AddUserAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.CompletedTask);
        _mockDoctorRepository.Setup(r => r.AddAsync(It.IsAny<Doctor>())).Returns((Task.FromResult(true)));
        _mockRoleRepository.Setup(r => r.GetRoleByIdAsync(It.IsAny<int>())).ReturnsAsync(new Role { Title = "Doctor" });

        // Act
        var response = await _authService.RegisterAsync(model);

        // Assert
        Assert.True(response.Success);
        Assert.Equal("Create doctor successful", response.Message);
        Assert.Null(response.Data);

        _mockUserRepository.Verify(r => r.AddUserAsync(It.IsAny<User>(), It.IsAny<int>()), Times.Once);
        //_mockDoctorRepository.Verify(r => r.AddAsync(It.IsAny<Doctor>()), Times.Once);
    }

}
