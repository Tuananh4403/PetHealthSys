using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Bookings
{
    public class BookingRepository(PetHealthDBContext dbContext, ILogger<BookingRepository> logger) : BaseRepository<Booking>(dbContext, logger), IBookingRepository
    {
        public async Task<bool> CheckReviewBooking(Booking booking)
        {
            try
            {
                int result;
                DateTime checkTime = booking.BookingTime;
                var query = dbContext.Bookings.AsQueryable();
                query = query.Where(b => b.DoctorId == booking.DoctorId)
                             .Where(b => b.BookingTime >= checkTime.AddMinutes(30) || b.BookingTime <= checkTime.AddMinutes(30));
                result = await query.CountAsync();
                if (result > 0)
                {
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                logger.LogInformation($"Error at BookingRepository with ID {booking.Id} error {ex}");
                return false;
            }
        }

        public async Task<IList<Booking>> GetListBooking(int BookingId)
        {
            var bookings = await dbContext.Bookings.Where(b => b.Id == BookingId).ToListAsync();
            return bookings.ToList<Booking>();
        }
    }
}
