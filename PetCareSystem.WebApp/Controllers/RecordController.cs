using Microsoft.AspNetCore.Mvc;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Services.Models.Record;
using PetCareSystem.Services.Services.Recordings;
using System;
using System.Threading.Tasks;

namespace PetCareSystem.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordController : ControllerBase
    {
        private readonly IRecordServices _recordService;


        public RecordController(IRecordServices recordServices)
        {
            _recordService = recordServices;
        }

        // POST: api/record/create
        [HttpPost("create-record")]
        public async Task<IActionResult> CreateRecord([FromBody] RecordRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            
            bool isSuccess = await _recordService.CreateRecordAsync(model, token);

            if (isSuccess)
            {
                return Ok(new { message = "Record creation successful" });
            }
            else
            {
                return BadRequest(new { message = "Record creation failed" });
            }
        }

    }
}
