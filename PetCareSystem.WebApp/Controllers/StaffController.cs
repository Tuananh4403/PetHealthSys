using PetCareSystem.WebApp.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models.Services;
using PetCareSystem.Services.Services.Serivces;

namespace PetCareSystem.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StaffControlelr : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffControlelr(IStaffService staffService)
        {
            _staffService = staffService;
        }
        [HttpGet]
        [Route("get-list")]
        public async Task<IActionResult> GetListStaff([FromQuery]string? searchOption, int? pageNumber, int? pageSize){
            try 
            { 
            var result = await _staffService.GetListStaff( searchOption);
            return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
