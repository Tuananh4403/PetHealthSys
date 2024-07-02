using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Bookings
{
    public interface IBookingRepository : IRepository<Booking>
    {
        public Task<(IEnumerable<Booking> Bookings, int TotalCount)> GetListBooking(DateTime? searchString = null, BookingStatus Status = BookingStatus.Review, int pageNumber = 1, int pageSize = 10);
        public Task<bool> CheckReviewBooking(Booking booking);
        public Task<Booking?> GetBookingDetail(int bookingId);
    }
}
