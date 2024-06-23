using Microsoft.AspNetCore.Mvc;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models.Booking;
using PetCareSystem.Services.Models.Recording;
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
        public async Task<IActionResult> Create(RecordingReq createRecordReq, int recordId, int serviceId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.CreateRecordAsync(createRecordReq, recordId ,serviceId);
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

        [HttpGet("{recordId}")]
        public async Task<IActionResult> GetRecordById(int createId)
        {
            try
            {
                var booking = await _service.GetRecordByIdAsync(createId);
                if (booking == null)
                {
                    return NotFound("Record not found");
                }
                return Ok(booking);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("record-detail")]
        public async Task<IActionResult> GetRecordDetail(int recordDetailId)
        {
            try
            {
                var booking = await _service.GetRecordDetailByIdAsync(recordDetailId);
                if (booking == null)
                {
                    return NotFound("Record detail not found");
                }
                return Ok(booking);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/booking/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, RecordingReq model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.UpdateRecordAsync(id, model);
                if (result)
                {
                    return Ok("Record updated successfully");
                }
                return NotFound("Booking not found");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }
    }
}