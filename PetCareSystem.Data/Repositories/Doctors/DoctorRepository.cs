using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Doctors
{
    public class DoctorRepository(PetHealthDBContext dbContext, ILogger<DoctorRepository> logger) : BaseRepository<Doctor>(dbContext, logger), IDoctorRepository
    {
        public async Task<Doctor> GetDoctorByUserId(int? id)
        {
            return await dbContext.Doctors.SingleOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<bool> CheckRoleAsync(int? userId)
        {
            try
            {
                bool isDoctor = await dbContext.Doctors
                    .AnyAsync(d => d.UserId == userId.Value);
                return isDoctor;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error at CheckRoleAsync with userId {userId}: {ex.Message}");
                return false;
            }
        }
    }
}
