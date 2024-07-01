
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
    public class RecordController : ControllerBase
    {
        private readonly IRecordServices _recordService;


        public RecordController(IRecordServices recordServices)
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
                var response = await _recordService.GetRecordHis(petId);
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
