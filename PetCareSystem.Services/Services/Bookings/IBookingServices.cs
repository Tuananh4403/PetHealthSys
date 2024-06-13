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

        Task<bool> UpdateBookingAsync(int BookingId, UpdateBookingReq updateReq);
        Task<Booking> GetBookingById(int BookingId);

        Task<IList<Booking>> GetBookingbyName(string Name);
        Task<bool> DeleteBooking(int BookingId);

    }
}
