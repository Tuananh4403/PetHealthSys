using Xunit;
using Moq;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Umbraco.Core.Persistence.Repositories;

namespace PetHealthCareSystem
{
    [TestClass]
    public class AuthServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IRoleRepository> _roleRepositoryMock;
        private AuthService _authService;

        [TestInitialize]
        public void TestInitialize()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _roleRepositoryMock = new Mock<IRoleRepository>();
            _authService = new AuthService(_userRepositoryMock.Object, _customerRepositoryMock.Object, _roleRepositoryMock.Object);
        }

        [TestMethod]
        public async Task LoginAsync_WithCorrectCredentials_ShouldReturnSuccess()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User { Username = username, Password = hashedPassword };

            _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(username)).ReturnsAsync(user);

            // Act
            var result = await _authService.LoginAsync(username, password);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Token);
            Assert.AreEqual(string.Empty, result.Message);
        }

        [TestMethod]
        public async Task LoginAsync_WithIncorrectPassword_ShouldReturnFailure()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";
            var wrongPassword = "wrongpassword";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User { Username = username, Password = hashedPassword };

            _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(username)).ReturnsAsync(user);

            // Act
            var result = await _authService.LoginAsync(username, wrongPassword);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Token);
            Assert.AreEqual("Invalid credentials", result.Message);
        }

        [TestMethod]
        public async Task LoginAsync_WithNonExistentUsername_ShouldReturnFailure()
        {
            // Arrange
            var username = "nonexistentuser";
            var password = "testpassword";

            _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(username)).ReturnsAsync((User)null);

            // Act
            var result = await _authService.LoginAsync(username, password);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Token);
            Assert.AreEqual("Invalid credentials", result.Message);
        }
    }
}
