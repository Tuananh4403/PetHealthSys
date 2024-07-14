using PetCareSystem.WebApp.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models.Services;
using PetCareSystem.Services.Services.Serivces;
using PetCareSystem.Services.Models.Booking;
using PetCareSystem.Services.Enums;

namespace PetCareSystem.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceServices _serviceServices;

        public ServiceController(IServiceServices serviceServices)
        {
            _serviceServices = serviceServices;
        }

        // POST: ServiceController/Create
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateServiceReq model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _serviceServices.CreateServiceAsync(model);
                    return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("get-service")]
        public async Task<IActionResult> GetService([FromQuery] string? searchOption, int? typeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _serviceServices.GetListServiceAsync(searchOption, typeId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }
         [HttpPost("upate/{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] UpdateServiceReq model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _serviceServices.UpdateServcieAsync(id, model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }
         [HttpGet("get-category")]
        public async Task<IActionResult> GetCategory()
        {   
            var respone = await _serviceServices.GetAllCategory();
            return Ok(respone);
        }
    }
}
