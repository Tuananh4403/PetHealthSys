using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.Services.Models.Pet;
using PetCareSystem.Services.Services.Auth;
using PetCareSystem.Services.Services.Pets;

namespace PetCareSystem.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

    }
}
