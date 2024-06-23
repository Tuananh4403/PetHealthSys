
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Services.Models.Recording;
using PetCareSystem.Services.Services.Records;
using PetCareSystem.WebApp.Helpers;
using System;
using System.Threading.Tasks;

namespace PetCareSystem.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RecordControllercs : ControllerBase
    {
        private readonly IRecordServices _service;


        public RecordControllercs(IRecordServices recordServices)
        {
            _service = recordServices;
        }

        // POST: api/record/create
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateRecordingReq model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                var result = await _service.CreateRecordAsync(model, token);
                if (result)
                {
                    return Ok("Record created successfully");
                }
                return BadRequest("Failed to create record");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
