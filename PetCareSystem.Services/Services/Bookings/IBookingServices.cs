using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models.Booking;
namespace PetCareSystem.Services.Services.Bookings
{
    public interface IBookingServices
    {
        Task<bool> CreateBookingAsync(CreateBookingReq bookingReq);

        Task<bool> UpdateBookingAsync(int BookingId, CreateBookingReq updateReq);
        Task<List<Booking>> GetBookingById(int BookingId);
        Task<bool> DeleteBooking(int BookingId);

    }
}
