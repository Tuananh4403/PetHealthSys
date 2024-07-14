using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Enums;
using PetCareSystem.Data.Repositories.Barns;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Bookings
{
    public class BarnRepository(PetHealthDBContext dbContext, ILogger<BarnRepository> logger) : BaseRepository<Barn>(dbContext, logger), IBarnRepository
    { 
    }
}