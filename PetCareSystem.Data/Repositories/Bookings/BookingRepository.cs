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

        public async Task<bool> CreateBookingAsync(Booking booking)        {
            _dbContext.Bookings.Add(booking);
            return await SaveChangesAsync();
        }

        public async Task<IList<Booking>> GetListBooking(int BookingId)
        {
            var bookings = await _dbContext.Bookings.Where(b => b.Id == BookingId).ToListAsync();
            return bookings.ToList<Booking>();
        }

        public async Task<bool> DeleteBookingAsync(int BookingId)
        {
            var bookings = await GetListBooking(BookingId);
            if (bookings != null && bookings.Any())
            {
                foreach (var booking in bookings)
        {
                    _dbContext.Bookings.Remove(booking);
                }
                return await SaveChangesAsync();
            }
            return false; 
        }


        public async Task<List<Booking>> GetBookingsAsync(string searchTerm, Dictionary<string, object> searchParameters, int pageNumber, int pageSize)
        {
            var query = _dbContext.Bookings.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(b => b.CustomerName.Contains(searchTerm) || b.ServiceType.Contains(searchTerm));
            }

            foreach (var param in searchParameters)
            {
                switch (param.Key.ToLower())
                {
                    case "startdate":
                        if (param.Value is DateTime startDate)
                        {
                            query = query.Where(b => b.BookingDate >= startDate);
                        }
                        break;
                    case "enddate":
                        if (param.Value is DateTime endDate)
                        {
                            query = query.Where(b => b.BookingDate <= endDate);
                        }
                        break;
                    case "servicetype":
                        if (param.Value is string serviceType)
                        {
                            query = query.Where(b => b.ServiceType == serviceType);
                        }
                        break;
                        // Add more cases for other parameters as needed
                }
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
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
