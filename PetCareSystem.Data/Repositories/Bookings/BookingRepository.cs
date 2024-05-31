using Microsoft.EntityFrameworkCore;
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
    public class BookingRepository : IBookingRepository
    {
        private readonly PetHealthDBContext _dbContext;
        public BookingRepository(PetHealthDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddBookingServiceAsync(BookingService bookingService)
        {
            _dbContext.BookingService.Add(bookingService);
            return await SaveChangesAsync();
        }

        public async Task<bool> CreateBookingAsync(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            return await SaveChangesAsync();
        }

        public Task<IList> GetListBooking()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                // Log or handle the exception as needed
                return false;
            }
        }
    }
}
