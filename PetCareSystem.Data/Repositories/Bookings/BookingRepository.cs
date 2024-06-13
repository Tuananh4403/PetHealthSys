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

        public async Task<Booking> GetListBooking(int bookingId)
        {
            return await _dbContext.Bookings.Include(b => b.Customer).FirstOrDefaultAsync(b => b.Id == bookingId);
        }


        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            var bookings = await GetListBooking(bookingId);
            if (bookings != null)
            {
                _dbContext.Remove(bookings);
                return await SaveChangesAsync();
            }
            return false;
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

        public async Task<IList<Booking>> GetListBooking(string name)
        {
            var bookings = await _dbContext.Bookings
                .Include(b => b.Customer)
                .ThenInclude(c => c.User)
                .Where(b => b.Customer.User.FirstName.Contains(name))
                .ToListAsync();
            return bookings;
        }

        public async Task<bool> UpdateBookingTimeAsync(int bookingId, DateTime time)
        {
            var bookingToUpdate = await GetListBooking(bookingId);

            if (bookingToUpdate != null)
            {
                bookingToUpdate.BookingTime = time;

                try
                {
                    
                    return await SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
