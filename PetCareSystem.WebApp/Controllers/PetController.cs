using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models.Booking;
using PetCareSystem.Services.Models.Pet;
using PetCareSystem.Services.Services.Auth;
using PetCareSystem.Services.Services.Pets;
using PetCareSystem.WebApp.Helpers;

namespace PetCareSystem.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpPost("register-pet")]
        public async Task<IActionResult> RegisterPet([FromBody] PetRequest model)
        {
            // Check if the model state is NOT valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                bool isRegistered = await _petService.RegisterPetAsync(model, token);
                if (isRegistered)
                {
                    return Ok(new { message = "Pet registration successful" });
                }
                else
                {
                    return BadRequest(new { message = "Pet registration failed" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while registering the pet", details = ex.Message });
            }
        }

        [HttpGet("pet-detail/{petId}")]
        public async Task<IActionResult> GetPetDetails(int petId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var petDetails = await _petService.GetPetDetailsAsync(petId);
                if (petDetails != null)
                {
                    return Ok(petDetails);
                }
                else
                {
                    return NotFound(new { message = "Pet not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the pet details", details = ex.Message });
            }
        }

        [HttpGet("getListPet")]
        public async Task<IActionResult> GetListPet(string petName, string nameOfCustomer, string kindOfPet, string speciesOfPet, bool? genderOfPet, DateTime? birthdayOfPet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var pets = await _petService.GetListPet(petName, nameOfCustomer, kindOfPet, speciesOfPet, genderOfPet, birthdayOfPet);
                if (pets.Any())
                {
                    return Ok(pets);
                }
                return NotFound("No pets found matching the search criteria.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the pet details", details = ex.Message });
            }
        }

        [HttpPut("update-pet/{id}")]
        public async Task<IActionResult> UpdatePet(int id, PetRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _petService.UpdatePetAsync(id, model);
                if (result)
                {
                    return Ok("Pet updated successfully");
                }
                return NotFound("Pet not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("delete-pet/{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            try
            {
                var result = await _petService.DeletePetAsync(id);
                if (result)
                {
                    return Ok("Pet deleted successfully");
                }
                return NotFound("Pet not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
