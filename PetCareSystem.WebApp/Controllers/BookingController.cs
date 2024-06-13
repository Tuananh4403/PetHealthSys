using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PetCareSystem.Services.Models.Booking;
using PetCareSystem.Services.Services.Bookings;
using System;
using System.Threading.Tasks;
using PetCareSystem.Data.Entites;


namespace PetCareSystem.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingServices _services;

        public BookingController(IBookingServices bookingServices)
        {
            _services = bookingServices;
        }

        // POST: api/booking/create
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateBookingReq model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _services.CreateBookingAsync(model);
                if (result)
                {
                    return Ok("Booking created successfully");
                }
                return BadRequest("Failed to create booking");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/booking/{bookingId}
        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetDetailBooking(int bookingId)
        {
            try
            {
                var booking = await _services.GetBookingById(bookingId);
                if (booking == null)
                {
                    return NotFound("Booking not found");
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
        public async Task<IActionResult> Update(int id, CreateBookingReq model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _services.UpdateBookingAsync(id, model);
                if (result)
                {
                    return Ok("Booking updated successfully");
                }
                return NotFound("Booking not found");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/booking/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _services.DeleteBooking(id);
                if (result)
                {
                    return Ok("Booking deleted successfully");
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
