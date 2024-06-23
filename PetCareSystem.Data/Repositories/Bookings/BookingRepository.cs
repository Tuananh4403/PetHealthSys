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

        public async Task<IList<Booking>> GetListBooking(int BookingId)
        {
            var bookings = await dbContext.Bookings.Where(b => b.Id == BookingId).ToListAsync();
            return bookings.ToList<Booking>();
        }
    }
}
