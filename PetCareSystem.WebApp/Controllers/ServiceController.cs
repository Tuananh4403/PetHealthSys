using PetCareSystem.WebApp.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models.Services;
using PetCareSystem.Services.Services.Serivces;

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
                var result = await _serviceServices.CreateServiceAsync(model);
                if (result)
                {
                    return Ok("Booking created successfully");
                }
                return BadRequest("Failed to create booking");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("get-service")]
        public async Task<IActionResult> GetService([FromQuery] string searchOption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var (services, totalCount) = await _serviceServices.GetListServiceAsync(searchOption, 1,1, 20);

                var servicesWithCategory = services.Select(service => new
                {
                    service.Id,
                    service.Name,
                    service.TypeId,
                    CategoryName = _serviceServices.GetServiceByCategory(service.TypeId)
                    // Map other properties if needed
                }).ToList();

                return Ok(new { Services = servicesWithCategory, TotalCount = totalCount });
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
