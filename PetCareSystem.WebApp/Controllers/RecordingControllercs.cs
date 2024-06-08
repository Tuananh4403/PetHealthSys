using Microsoft.AspNetCore.Mvc;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Services.Models.Recording;
using PetCareSystem.Services.Services.Recordings;
using System;
using System.Threading.Tasks;

namespace PetCareSystem.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordingControllercs : ControllerBase
    {
        private readonly IRecordingServices _service;


        public RecordingControllercs(IRecordingServices recordServices)
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

            try
            {
                // Check if DoctorId is valid
                var isDoctorValid = await _service.IsDoctorId(model.DoctorId);
                if (!isDoctorValid)
                {
                    return BadRequest("You are not doctor");
                }

                var result = await _service.CreateRecordAsync(model);
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
