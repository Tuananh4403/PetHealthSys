using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.BookingServices
{
    public interface IBookingServiceRepository
    {
        Task<Boolean> UpdateServiceIdsAsync(int bookingId, HashSet<int> newServiceIds);
        Task<Boolean> SaveChangesAsync();
    }
}

