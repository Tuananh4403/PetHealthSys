using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models.Barn;
using PetCareSystem.Services.Services.Barn;
using System.Threading.Tasks;
using System;

namespace PetCareSystem.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarnController : ControllerBase
    {
        private readonly IBarnServices _barnServices;

        public BarnController(IBarnServices barnServices)
        {
            _barnServices = barnServices;
        }

        // POST: BarnController/Create
        [HttpPost("create")]
        public async Task<IActionResult> Create(BarnReq model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _barnServices.CreateBarnAsync(model);
                if (result)
                {
                    return Ok("Barn created successfully");
                }
                return BadRequest("Failed to create barn");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("get-barn")]
        public async Task<IActionResult> GetListBarn()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var barns = await _barnServices.GetBarn();
                return Ok(barns);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("get-barn-status")]
        public async Task<IActionResult> GetListBarnStatus(bool status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var barns = await _barnServices.GetBarnStatusFalse(status);
                return Ok(barns);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, BarnReq model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _barnServices.UpdateBarnAsync(id, model);
                if (result)
                {
                    return Ok("Barn updated successfully");
                }
                return BadRequest("Failed to update barn");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _barnServices.DeleteBarnAsync(id);
                if (result)
                {
                    return Ok("Barn deleted successfully");
                }
                return BadRequest("Failed to delete barn");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
