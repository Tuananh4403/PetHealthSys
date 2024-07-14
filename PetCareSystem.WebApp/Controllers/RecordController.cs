
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Services.Models.Recording;
using PetCareSystem.Services.Services.Records;
using PetCareSystem.Services.Services.Serivces;
using PetCareSystem.WebApp.Helpers;
using Serilog;
using System;
using System.Threading.Tasks;
using ILogger = Serilog.ILogger;

namespace PetCareSystem.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RecordController : ControllerBase
    {
        private readonly IRecordServices _recordService;
        private readonly IBarnService _barnService;
        private readonly ILogger _logger;


        public RecordController(IRecordServices recordServices, IBarnService barnService)
        {
            _recordService = recordServices;
            _barnService = barnService;
            _logger = Log.Logger;
        }

        // POST: api/record/create
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateRecordingReq model)
        {
                Console.WriteLine("test");

            if (!ModelState.IsValid)
            {
                return BadRequest("test");
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                Console.WriteLine("test");
                var response = await _recordService.CreateRecordAsync(model, token);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }
         [HttpPost("creat-by-booking/{id}")]
        public async Task<IActionResult> CreateByBooking(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                var response = await _recordService.CreateRecordByBookingAsync(id, token);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                var exStr = JsonConvert.SerializeObject(ex);
                _logger.Information(exStr);
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
        [HttpGet("get-list-record")]
        public async Task<IActionResult> GetListRecord([FromQuery]string? petName, string? nameOfCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                var response = await _recordService.GetListRecord(petName, nameOfCustomer);
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
