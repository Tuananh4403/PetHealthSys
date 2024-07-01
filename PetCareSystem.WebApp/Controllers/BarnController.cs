using Microsoft.AspNetCore.Mvc;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Barns;
using PetCareSystem.Services.Models.Barn;
using PetCareSystem.Services.Services.Barns;
using PetCareSystem.WebApp.Helpers;
using System;
using System.Threading.Tasks;


namespace PetCareSystem.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BarnController : ControllerBase
    {
        private readonly IBarnService _barnservice;

        public BarnController(IBarnService barnservice)
        {
            _barnservice = barnservice;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBarn([FromBody] BarnRequest model)
        {
            // Check if the model state is NOT valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                bool isCreated = await _barnservice.CreateBarnsAsync(model, token);
                if (isCreated)
                {
                    return Ok(new { message = "Barn create successful" });
                }
                else
                {
                    return BadRequest(new { message = "Barn create failed" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the barn", details = ex.Message });
            }
        }
    }
}
