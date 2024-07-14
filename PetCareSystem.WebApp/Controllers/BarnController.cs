using PetCareSystem.WebApp.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models.Services;
using PetCareSystem.Services.Services.Barns;
using PetCareSystem.Services.Services.Serivces;

namespace PetCareSystem.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BarnController : ControllerBase
    {
        private readonly IBarnService _barnService;

        public BarnController(IBarnService barnService)
        {
            _barnService = barnService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> GetListStaff([FromBody] string? note, string? date){
            try 
            { 
            var result = await _barnService.Create(note, date);
            return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("get-list-barn")]
        public async Task<IActionResult> GetListBarn(){
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                var response = await _barnService.GetListBarn();
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
