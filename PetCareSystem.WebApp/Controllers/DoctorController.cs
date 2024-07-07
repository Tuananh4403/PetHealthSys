using Microsoft.AspNetCore.Mvc;
using PetCareSystem.Services.Services.Doctors;
using PetCareSystem.WebApp.Helpers;
using Microsoft.AspNetCore.Http;

namespace PetCareSystem.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorServices _doctorServices;
        public DoctorController(IDoctorServices doctorServices)
        {
            _doctorServices = doctorServices;
        }
        [HttpGet("get-doctor")]
        public async Task<IActionResult> GetDoctor([FromQuery] string? searchOption)
        {
            try
            {
                var response = await _doctorServices.GetListDoctorAsync(searchOption);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }
    }
        
    }

