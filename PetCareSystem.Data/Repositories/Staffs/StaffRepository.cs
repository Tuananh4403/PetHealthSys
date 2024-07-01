using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Staffs
{
    public class StaffRepository(PetHealthDBContext dbContext, ILogger<StaffRepository> logger) : BaseRepository<Staff>(dbContext, logger), IStaffRepository
    {
        public async Task<bool> CheckRoleAsync(int? userId)
        {
            try
            {
                bool isStaff = await dbContext.Staffs
                    .AnyAsync(s => s.UserId == userId.Value);
                return  isStaff;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error at CheckRoleAsync with userId {userId}: {ex.Message}");
                return false;
            }
        }
    }
}
