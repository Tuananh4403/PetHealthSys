
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
        private readonly IRecordServices _recordService;


        public RecordControllercs(IRecordServices recordServices)
        {
            _recordService = recordServices;
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
                var response = await _recordService.CreateRecordAsync(model, token);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("getRecord")]
        public async Task<IActionResult> GetRecord(int recordId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                var response = await _recordService.GetRecordById(recordId,token);
                if (response == null)
                {
                    return NotFound("Record not found");
                }
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
