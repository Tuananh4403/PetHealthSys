using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.BookingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Barns
{
    public class BarnRepository(PetHealthDBContext dbContext, ILogger<BarnRepository> logger) : BaseRepository<Barn>(dbContext, logger), IBarnRepository
    {

        
    }
}
