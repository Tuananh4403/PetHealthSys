using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Customers;
using PetCareSystem.Data.Repositories.Pets;
using PetCareSystem.Services.Models.Pet;
using PetCareSystem.Services.Services.Pets;

namespace PetCareSystem.Services.Tests;

[TestFixture]
public class UnitTest
{
    private Mock<IPetRepository> _mockRepository;
    private Mock<ICustomerRepository> _mockCusRepository;
    private PetService _petService;
    private Mock<DbSet<Pet>> _mockSet;
    private Mock<PetHealthDBContext> _mockContext;

    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<IPetRepository>();
        _mockCusRepository = new Mock<ICustomerRepository>();
        _petService = new PetService(_mockRepository.Object, _mockCusRepository.Object);
    }

    [Test]
    public async Task TestValidPetRegistration()
    {
        // Arrange
        var petRequest = new PetRequest
        {
            Birthday = Convert.ToDateTime("01-02-2024"),
            PetName = "test",
            Gender = true,
            KindOfPet = "Dog",
            Species = "test"
        };

        var pet = new Pet
        {
            // Map properties from PetRequest to Pet entity
            Birthday = petRequest.Birthday,
            PetName = petRequest.PetName,
            Gender = petRequest.Gender,
            KindOfPet = petRequest.KindOfPet,
            Species = petRequest.Species
        };

        _mockRepository.Setup(repo => repo.AddPetAsync(It.IsAny<Pet>()))
            .ReturnsAsync(pet)
            .Verifiable("AddAsync was not called with the expected pet.");

        // Act
        var result = await _petService.RegisterPetAsync(petRequest, "token");

        // Assert
        Xunit.Assert.Equal(pet.PetName, result.PetName);
        Console.WriteLine($"Data after test execution: {pet.Id}");
        _mockRepository.Verify();
    }
}
