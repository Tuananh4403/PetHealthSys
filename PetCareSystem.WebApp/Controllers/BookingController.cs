using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Authorization;
using PetCareSystem.WebApp.Helpers;
using PetCareSystem.Services.Models.Booking;
using PetCareSystem.Services.Services.Bookings;
using System;
using System.Threading.Tasks;
using PetCareSystem.Data.Entites;
using Sprache;
using PetCareSystem.Data.Enums;
using PetCareSystem.Services.Models;
using Newtonsoft.Json;
using ILogger = Serilog.ILogger;
using Serilog;


namespace PetCareSystem.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingServices _bookingServices;
        private readonly ILogger _logger;


        public BookingController(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
            _logger = Log.Logger;

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
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var result = await _bookingServices.CreateBookingAsync(model, token);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("review-booking")]
        public async Task<IActionResult> GetReviewBooking(string? status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var bookingStatus = (BookingStatus)Enum.Parse(typeof(BookingStatus), status) != null ? (BookingStatus)Enum.Parse(typeof(BookingStatus), status) : BookingStatus.Review;
                var response = await _bookingServices.GetListBookingAsync(status: bookingStatus, token: token);

                return Ok(response);
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
                var booking = await _bookingServices.GetBookingById(bookingId);
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
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                var result = await _bookingServices.UpdateBookingAsync(id, model, token);
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
                var result = await _bookingServices.DeleteBooking(id);
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
        [HttpPost("confirm/{id}")]
        public async Task<IActionResult> Confirm(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var response = await _bookingServices.ConfirmBooking(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("get-shift")]
        public IActionResult GetShift()
        {
            var shifts = BookingShiftExtensions.GetAllBookingShifts();

            return Ok(new ApiResponse<IList<BookingShiftDto>>(data: shifts));
        }
    }
}
