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

        public async Task<IList<Booking>> GetListBooking(string name)
        {
            var bookings = await _dbContext.Bookings
                .Include(b => b.Customer)
                .ThenInclude(c => c.User)
                .Where(b => b.Customer.User.FirstName.Contains(name))
                .ToListAsync();
            return bookings;
        }

        public async Task<bool> UpdateBooking(int bookingId, DateTime bookingTime, int[] serviceIds)
        {
            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(id=>id.Id == bookingId);
            if(booking == null)
            {
                return false;
            }
            booking.BookingTime = bookingTime;
            _dbContext.BookingService.RemoveRange(booking.BookingServices);

            foreach (var serviceId in serviceIds)
            {
                var bookingService = new BookingService
                {
                    ServiceId = serviceId,
                    BookingId = bookingId
                };
                _dbContext.BookingService.Add(bookingService);
            }
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CancelBooking(int bookingId)
        {
            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(id=> id.Id == bookingId);
            if (booking == null)
            {
                return false;
            }
            booking.Status = null;
            await _dbContext.SaveChangesAsync();
            return true;
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