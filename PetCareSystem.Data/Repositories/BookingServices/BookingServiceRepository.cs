using Microsoft.EntityFrameworkCore;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.BookingServices
{
    public class BookingServiceRepository : IBookingServiceRepository
    {
        private readonly PetHealthDBContext _dbContext;

        public BookingServiceRepository(PetHealthDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Boolean> UpdateServiceIdsAsync(int bookingId, HashSet<int> newServiceIds)
        {
            var bookingServices = await _dbContext.BookingService
                .Where(bs => bs.BookingId == bookingId)
                .ToListAsync();

            var servicesToRemove = bookingServices.Where(bs => !newServiceIds.Contains(bs.ServiceId)).ToList();
            _dbContext.BookingService.RemoveRange(servicesToRemove);

            var servicesToAdd = newServiceIds
                .Where(id => !bookingServices.Any(bs => bs.ServiceId == id))
                .Select(serviceId => new BookingService { BookingId = bookingId, ServiceId = serviceId });
            await _dbContext.BookingService.AddRangeAsync(servicesToAdd);

            if ( await _dbContext.SaveChangesAsync() >0)
            {
                return true;
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
    }
}
