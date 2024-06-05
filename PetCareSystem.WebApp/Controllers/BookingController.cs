using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PetCareSystem.Services.Models.Booking;
using PetCareSystem.Services.Services.Bookings;
using System;
using PetCareSystem.Data.Entites;


namespace PetCareSystem.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingServices _services;
        public BookingController(IBookingServices bookingServices)
        {
            _services = bookingServices;
        }
        // POST: Booking/Create
        [HttpPost("create-booking")]
        public async Task<IActionResult> Create(CreateBookingReq model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var checkUpdate = await _services.CreateBookingAsync(model);
                if (checkUpdate)
                {
                    return Ok("Booking created successfully");
                }
                else
                {
                    return BadRequest("Failed to create booking");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
