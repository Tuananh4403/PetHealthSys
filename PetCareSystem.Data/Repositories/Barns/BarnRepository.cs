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

        public async Task<bool> CheckRoleAsync(int? userId)
        {
            try
            {
                bool isDoctor = await dbContext.Doctors
                    .AnyAsync(d => d.UserId == userId.Value);
                bool isStaff = await dbContext.Staffs
                    .AnyAsync(s => s.UserId == userId.Value);
                return isDoctor || isStaff;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error at CheckRoleAsync with userId {userId}: {ex.Message}");
                return false;
            }
        }
    }
}
