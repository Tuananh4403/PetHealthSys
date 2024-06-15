using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetCareSystem.Services.Models.Auth;
using PetCareSystem.Services.Services.Auth;
using PetCareSystem.WebApp.Controllers;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class AuthControllerTests
    {
        private Mock<IAuthService> _authServiceMock;
        private AuthController _controller;

        [TestInitialize]
        public void Setup()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object);
        }

        [TestMethod]
        public async Task Register_ReturnsOk_WhenModelIsValid()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                Username = "testuser",
                Password = "Test@123",
                Email = "test@example.com"
            };

            // Act
            var result = await _controller.Register(registerRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual("User have been created", okResult.Value);
        }

        [TestMethod]
        public async Task Register_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var registerRequest = new RegisterRequest(); // Invalid model

            _controller.ModelState.AddModelError("Username", "Required");

            // Act
            var result = await _controller.Register(registerRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}