using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models;
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
                var response = await _petService.RegisterPetAsync(model, token);
                return Ok(response);
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

        [HttpGet("get-list-pet-by-user")]
        public async Task<IActionResult> GetListPetByUserId([FromQuery]bool? saveBarn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var result = await _petService.GetListPetByUserId(token, saveBarn);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the pet details", details = ex.Message });
            }
        }
        [HttpGet("get-list-pet")]
        public async Task<IActionResult> GetListPet(string? petName, string? nameOfCustomer, bool? saveBarn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var pets = await _petService.GetListPet(petName, nameOfCustomer, saveBarn);
                return Ok(pets);
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
                    return Ok(new ApiResponse<string>("Pet deleted successfully", false));
                }
                return Ok(new ApiResponse<string>("Pet not found", true));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("get-record-history")]
        public async Task<IActionResult> GetRecordHis([FromQuery]int petId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                var response = await _petService.GetPetRecordHis(petId);
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
