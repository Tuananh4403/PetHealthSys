using PetCareSystem.Data.Entites;
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
        public Task<IList<Booking>> GetListBooking(int BookingId);
    }
}
