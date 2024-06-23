using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.BookingServices
{
    public class BookingServiceRepository(PetHealthDBContext dbContext, ILogger<BookingServiceRepository> logger) : BaseRepository<BookingService>(dbContext, logger), IBookingServiceRepository
    {
    }
}
