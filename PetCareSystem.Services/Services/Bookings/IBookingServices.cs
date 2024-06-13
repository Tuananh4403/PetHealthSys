using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Bookings
{
    public interface IBookingServices
    {
        Task<bool> CreateBookingAsync(CreateBookingReq bookingReq);

        Task<bool> UpdateBookingAsync(int BookingId, CreateBookingReq updateReq);
        Task<Booking> GetBookingById(int BookingId);
        Task<bool> DeleteBooking(int BookingId);

    }
}
